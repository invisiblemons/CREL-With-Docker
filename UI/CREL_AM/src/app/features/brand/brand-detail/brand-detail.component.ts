import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChildren,
} from '@angular/core';
import {
  FormBuilder,
  FormControlName,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import CustomStore from 'devextreme/data/custom_store';
import { fromEvent, merge, Observable, Subject } from 'rxjs';
import { debounceTime, finalize, switchMap, takeUntil } from 'rxjs/operators';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';
import { GenericValidator } from 'src/app/shared/validator/generic-validator';
import { Broker } from '../../broker/broker.model';
import { Industry } from '../../industry/industry.model';
import { IndustryService } from '../../industry/industry.service';
import { Brand } from '../brand.model';
import { BrandService } from '../brand.service';
import swal from 'sweetalert2';
import { TeamService } from '../../team/team.service';
import { BrokerService } from '../../broker/broker.service';
import { Team } from '../../team/team.model';
import { BrandVerificationService } from 'src/app/shared/services/brand-verification.service';

@Component({
  selector: 'app-brand-detail',
  templateUrl: './brand-detail.component.html',
  styleUrls: ['./brand-detail.component.scss'],
})
export class BrandDetailComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  fromSuggestion: boolean;

  verifyModal: boolean;

  unverifyModal: boolean;

  reasons: string[];

  /* 
  fields for object
  */
  // Brand
  statuses: { label: string; value: number }[];
  brandId: string;
  brand: Brand;
  industry: Industry;
  industries: Industry[] = [];
  rejectMessage: string = '';
  // Broker
  brokers: Broker[] = [];
  selectedBroker: Broker;
  brokerGridDataSource: any;
  brokerGridBoxValue: number[] = [];
  isBrokerGridBoxOpened: boolean = false;
  brokerGridColumns: any = [
    {
      caption: 'Tên',
      dataField: 'name',
      dataType: 'string',
    },
    {
      caption: 'Số điện thoại',
      dataField: 'phoneNumber',
      dataType: 'string',
    },
  ];
  //team
  team: Team;
  teams: Team[] = [];
  isChecked: boolean = false;
  /* 
  fields for object
  */
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = '';

  verifyForm!: FormGroup;

  unverifyForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  isFromHome: boolean = false;

  isFromAppointment: boolean = false;

  isFromContract: boolean = false;

  isShowSuggestion = false;

  constructor(
    private brandServices: BrandService,
    private route: ActivatedRoute,
    private router: Router,
    private industryServices: IndustryService,
    private reloadRouteServices: ReloadRouteService,
    private ref: ChangeDetectorRef,
    private fb: FormBuilder,
    private brokerServices: BrokerService,
    private teamServices: TeamService,
    private brandVerificationService: BrandVerificationService
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.statuses = [
      { label: 'Đã xoá', value: 0 },
      { label: 'Đợi xét duyệt', value: 1 },
      { label: 'Được duyệt', value: 2 },
      { label: 'Không được duyệt', value: 3 },
    ];
    this.fromSuggestion = false;
    this.unverifyModal = false;
    this.verifyModal = false;
    this.reasons = [
      'thông tin thương hiệu không tồn tại',
      'mã số doanh nghiệp không xác định',
    ];

    // validate
    this.validationMessages = {
      broker: {
        required: 'Nhân viên môi giới không được để trống.',
      },
      rejectMessage: {
        required: 'Lý do từ chối xét duyệt không được để trống.',
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);

    // validate
    this.verifyForm = this.fb.group({
      broker: ['', [Validators.required]],
    });
    this.unverifyForm = this.fb.group({
      rejectMessage: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.route.queryParams
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((params) => {
          this.fromSuggestion = params['fromSuggestion'];
          this.isFromHome = params['fromHome'];
          this.isFromAppointment = params['fromAppointment'];
          this.isFromContract = params['fromContract'];
          return this.brandServices.getBrandById(params['id']);
        }),
        switchMap((brand) => {
          this.brand = brand;
          return this.industryServices.getIndustries();
        }),
        switchMap((industries) => {
          this.industries = industries;
          this.brand = this.updateIndustry(this.brand);
          this.isShowSkeleton = false;
          return this.brandVerificationService.getCompany(
            this.brand.registrationNumber
          );
        })
      )
      .subscribe((company) => {
        this.isChecked = true;
      });

    this.loadBrokers();
  }

  ngAfterViewInit(): void {
    // Watch for the blur event from any input element on the form.
    // This is required because the valueChanges does not provide notification on blur
    const controlBlurs: Observable<any>[] = this.formInputElements.map(
      (formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur')
    );

    // Merge the blur event observable with the valueChanges observable
    // so we only need to subscribe once.
    merge(this.verifyForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.verifyForm
        );
      });
    merge(this.unverifyForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.unverifyForm
        );
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    if (this.fromSuggestion) {
      this.reloadRouteServices.routingNotReload(
        '/bat-dong-san/danh-sach/de-xuat',
        null
      );
    }
    else if (this.isFromContract) {
      this.reloadRouteServices.routingNotReload('/thuong-hieu/hop-dong', null);
    }
    else if (this.isFromAppointment) {
      this.reloadRouteServices.routingNotReload('/thuong-hieu/cuoc-hen', null);
    }
    else if (this.isFromHome) {
      this.reloadRouteServices.routingNotReload('/tong-quan', null);
    } else {
      this.reloadRouteServices.routingReload('/thuong-hieu/danh-sach', null);
    }
  }

  cancelDialog() {
    if (this.fromSuggestion) {
      this.reloadRouteServices.routingNotReload(
        '/bat-dong-san/danh-sach/de-xuat',
        null
      );
    }
    else if (this.isFromHome) {
      this.reloadRouteServices.routingNotReload('/tong-quan', null);
    } else {
      this.reloadRouteServices.routingNotReload('/thuong-hieu/danh-sach', null);
    }
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
  loadBrokers() {
    this.teamServices
      .getTeams()
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((teams) => {
          this.teams = teams;

          return this.brokerServices.getBrokers();
        })
      )
      .subscribe((brokers) => {
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

  //action function
  verifyBrand() {
    this.brand.brokerId = this.selectedBroker.id;
    this.brand.status = 2;
    this.brandServices.updateBrand(this.brand).subscribe((brand) => {
      swal.fire({
        title: 'Thành công!',
        text: 'Xét duyệt thương hiệu thành công.',
        icon: 'success',
        customClass: {
          confirmButton: 'btn btn-success animation-on-hover',
        },
        buttonsStyling: false,
        timer: 2000,
      });
      this.verifyForm.reset();
      this.hideVerifyModal();
    });
  }

  openVerifyModal() {
    this.verifyModal = true;
  }

  openUnverifyModal() {
    this.unverifyModal = true;
  }
  hideVerifyModal() {
    this.verifyModal = false;
  }
  hideUnverifyModal() {
    this.unverifyModal = false;
  }

  openSuggestionModal() {
    this.isShowSuggestion = true;
  }

  getStateSuggestion() {
    this.isShowSuggestion = false;
  }


  unverify() {
    this.brand.status = 3;
    this.brand.rejectMessage = this.rejectMessage;
    this.brandServices.updateBrand(this.brand).subscribe((brand) => {
      swal.fire({
        title: 'Thành công!',
        text: 'Từ chối xét duyệt thương hiệu thành công.',
        icon: 'success',
        customClass: {
          confirmButton: 'btn btn-success animation-on-hover',
        },
        buttonsStyling: false,
        timer: 2000,
      });
      this.unverifyForm.reset();
      this.hideUnverifyModal();
    });
  }
}
