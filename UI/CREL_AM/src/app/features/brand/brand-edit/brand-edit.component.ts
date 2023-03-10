import {
  Component,
  OnInit,
  OnDestroy,
  ViewChildren,
  ElementRef,
  AfterViewInit,
  ChangeDetectorRef,
} from '@angular/core';
import { fromEvent, merge, Observable, of, Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { Brand } from '../brand.model';
import { BrandService } from '../brand.service';
import { debounceTime, finalize, switchMap, takeUntil } from 'rxjs/operators';
import swal from 'sweetalert2';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormControlName,
} from '@angular/forms';
import { GenericValidator } from 'src/app/shared/validator/generic-validator';
import 'devextreme/ui/html_editor/converters/markdown';
import { IndustryService } from '../../industry/industry.service';
import { Industry } from '../../industry/industry.model';
import { EMAIL_PATTERN } from 'src/app/shared/constants/common.const';
import { Broker } from '../../broker/broker.model';
import CustomStore from 'devextreme/data/custom_store';
import { TeamService } from '../../team/team.service';
import { BrokerService } from '../../broker/broker.service';
import { Team } from '../../team/team.model';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';

@Component({
  selector: 'app-brand-edit',
  templateUrl: './brand-edit.component.html',
  styleUrls: ['./brand-edit.component.scss'],
})
export class BrandEditComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  /* 
  fields for object
  */
  //brand
  brand: Brand;
  reasons: string[];
  statuses: { label: string; value: number }[];
  selectedStatus: { label: string; value: number };
  //industry
  industry: Industry;
  industries: Industry[] = [];
  selectedIndustry: Industry;
  // Broker
  brokers: Broker[] = [];
  selectedBroker: Broker;
  brokerGridDataSource: any;
  brokerGridBoxValue: number[] = [];
  isBrokerGridBoxOpened: boolean = false;
  brokerGridColumns: any = [
    {
      caption: 'T??n',
      dataField: 'name',
      dataType: 'string',
    },
    {
      caption: 'S??? ??i???n tho???i',
      dataField: 'phoneNumber',
      dataType: 'string',
    },
  ];
  //team
  team: Team;
  teams: Team[] = [];

  //image
  avatarFile: File;

  changedImg: boolean;

  //editor
  editorValueType: string;

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = '';

  brandForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  constructor(
    private brandServices: BrandService,
    private industryServices: IndustryService,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private ref: ChangeDetectorRef,
    private brokerServices: BrokerService,
    private teamServices: TeamService,
    private reloadService: ReloadRouteService
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;
    this.statuses = [
      { label: '???? xo??', value: 0 },
      { label: '?????i x??t duy???t', value: 1 },
      { label: '???????c duy???t', value: 2 },
      { label: 'Kh??ng ???????c duy???t', value: 3 },
    ];

    this.reasons = [
      'L???i n???i dung sai s??? th???t',
      'L???i h??nh ???nh kh??ng li??n quan',
      'L???i d??ng t??? ng??? th?? t???c',
      'L???i n???i dung ch???a th??ng tin ph??n bi???t ch???ng t???c, v??ng mi???n',
    ];

    //editor
    this.editorValueType = 'html';

    //image
    this.changedImg = false;

    //validate
    this.validationMessages = {
      name: {
        required: 'T??n kh??ng ???????c ????? tr???ng.',
        minlength: 'T??n ph???i c?? ??t nh???t 3 k?? t???.',
        maxlength: 'T??n c?? nhi???u nh???t 100 k?? t???.',
      },
      phoneNumber: {
        required: 'S??? ??i???n tho???i kh??ng ???????c ????? tr???ng.',
      },
      email: {
        required: '?????a ch??? email kh??ng ???????c ????? tr???ng.',
        pattern: '?????a ch??? email kh??ng h???p l???',
      },
      status: {
        required: 'Tr???ng th??i kh??ng ???????c ????? tr???ng.',
      },
      selectedIndustry: {
        required: 'Ng??nh kinh doanh kh??ng ???????c ????? tr???ng.',
      },
      broker: {
        required: 'Nh??n vi??n m??i gi???i kh??ng ???????c ????? tr???ng.',
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.brandForm = this.fb.group({
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ],
      ],
      userName: [{ value: '', disabled: true }],
      email: ['', [Validators.required, Validators.pattern(EMAIL_PATTERN)]],
      phoneNumber: ['', [Validators.required]],
      status: ['', [Validators.required]],
      selectedIndustry: ['', [Validators.required]],
      registrationNumber: [''],
      broker: ['', [Validators.required]],
    });

    // Load Data
    this.route.queryParams
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((params) => {
          return this.brandServices.getBrandById(params['id']);
        }),
        switchMap((brand) => {
          this.brand = brand;
          return this.teamServices.getTeams();
        }),
        switchMap((teams) => {
          this.teams = teams;

          return this.brokerServices.getBrokers();
        }),
        switchMap((brokers) => {
          this.brokers = brokers;
          this.brokers.forEach((broker) => {
            let team: Team = this.getTeam(broker);
            if (team) {
              broker.team = team;
              broker.teamName = team.name;
            }
          });
          // gridbox devex
          this.brokerGridDataSource = this.makeAsyncDataSource(this.brokers);
          return this.industryServices.getIndustries();
        })
      )
      .subscribe((industries) => {
        this.industries = industries;
        this.brand = this.updateIndustry(this.brand);
        this.selectedIndustry = this.brand.industry;
        this.statuses.forEach((status) => {
          status.value === this.brand.status
            ? (this.selectedStatus = status)
            : '';
        });
        this.selectedBroker = this.brokers.filter(
          (res) => res.id === this.brand.brokerId
        )[0];
        this.brokerGridBoxValue = [
          ...this.brokerGridBoxValue,
          this.brand.brokerId,
        ];

        // set value for fields form
        this.brandForm.controls.userName.setValue(this.brand.userName);
        this.brandForm.controls.broker.setValue(this.brokerGridBoxValue);

        this.isShowSkeleton = false;
      });
  }

  ngAfterViewInit(): void {
    // Watch for the blur event from any input element on the form.
    // This is required because the valueChanges does not provide notification on blur
    const controlBlurs: Observable<any>[] = this.formInputElements.map(
      (formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur')
    );

    // Merge the blur event observable with the valueChanges observable
    // so we only need to subscribe once.
    merge(this.brandForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.brandForm
        );
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingReload('/thuong-hieu/danh-sach', null);
  }

  cancelDialog() {
    this.reloadService.routingNotReload('/thuong-hieu/danh-sach', null);
  }

  //data functions
  getIndustry(brand: Brand): Industry {
    for (let i = 0; i < this.industries.length; i++) {
      if (brand.industryId === this.industries[i].id) {
        return this.industries[i];
      }
    }
    return;
  }
  updateIndustry(brand: Brand): Brand {
    let industry = this.getIndustry(brand);
    if (industry) {
      brand.industry = industry;
      brand.industryName = industry.name;
    }
    return brand;
  }
  getIndustries() {
    this.industryServices.getIndustries().subscribe((res) => {
      this.industries = res;
    });
  }
  getTeam(broker: Broker): Team {
    for (let i = 0; i < this.teams.length; i++) {
      if (broker.teamId === this.teams[i].id) {
        return this.teams[i];
      }
    }
    return;
  }

  // Dev extreme
  makeAsyncDataSource(items) {
    return new CustomStore({
      loadMode: 'raw',
      key: 'id',
      load() {
        return items;
      },
    });
  }

  brokerGridBox_displayExpr(item) {
    return item && `${item.name} <${item.phoneNumber}>`;
  }

  onBrokerGridBoxOptionChanged(e) {
    if (e.name === 'value' && e.value) {
      this.isBrokerGridBoxOpened = false;
      this.selectedBroker = this.brokers.filter(
        (res) => res.id === e.value[0]
      )[0];
      if (!e.previousValue) {
        this.ref.detectChanges();
      }
    }
  }

  // Image Function
  getImgFromChild(imgFile) {
    this.avatarFile = imgFile;
    this.changedImg = true;
  }

  //action function
  saveBrand() {
    if (this.brandForm.valid) {
      this.loading = true;
      this.brand.industryId = this.selectedIndustry.id;
      this.brand.status = this.selectedStatus.value;
      if (this.brand.status === 1 && this.brand.brokerId !== null) {
        this.brand.brokerId = null;
      }
      if (this.brand.phoneNumber.includes(' ')) {
        let phoneArr = this.brand.phoneNumber.split(' ');
        this.brand.phoneNumber = phoneArr[0] + phoneArr[1] + phoneArr[2];
      }
      this.brand.brokerId = this.selectedBroker.id;
      this.brand.email = this.brand.email.toLowerCase();
      this.brand = new Brand(this.brand, false);
      this.brandServices
        .updateBrand(this.brand)
        .pipe(
          takeUntil(this.destroySubs$),
          switchMap((brand) => {
            swal.fire({
              title: 'Th??nh c??ng!',
              text: 'C???p nh???t th????ng hi???u th??nh c??ng.',
              icon: 'success',
              customClass: {
                confirmButton: 'btn btn-success animation-on-hover',
              },
              buttonsStyling: false,
              timer: 2000,
            });
            this.brandForm.reset();
            this.hideDialog();

            //update image
            if (this.changedImg && this.avatarFile) {
              let formData: FormData = new FormData();
              formData.append('file', this.avatarFile);
              return this.brandServices.updateBrandAvatar(brand.id, formData);
            }
            return of(brand);
          }),
          finalize(() => (this.loading = false))
        )
        .subscribe(
          (brandAfterUpdateImage: Brand) => {
            //get Industry
            this.brand = this.updateIndustry(brandAfterUpdateImage);
          },
          (error) => {
            swal.fire({
              title: 'Th???t b???i!',
              text:
                'C???p nh???t th????ng hi???u th???t b???i v???i l???i ' + error.error.title,
              icon: 'error',
              customClass: {
                confirmButton: 'btn btn-info animation-on-hover',
              },
              buttonsStyling: false,
              timer: 2000,
            });
          }
        );
    }
  }

  //extend
  changeStatus(ev) {
    this.selectedStatus = ev.value;
    if (this.selectedStatus.value !== 1) {
      this.brandForm.controls.broker.markAsPristine();
    }
  }
}
