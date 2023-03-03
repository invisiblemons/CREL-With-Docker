import {
  Component,
  OnInit,
  OnDestroy,
  ElementRef,
  ViewChildren,
  AfterViewInit,
  ChangeDetectorRef,
} from '@angular/core';
import { Router } from '@angular/router';
import { fromEvent, merge, Observable, of, Subject } from 'rxjs';
import { Brand } from '../brand.model';
import { BrandService } from '../brand.service';
import swal from 'sweetalert2';
import { debounceTime, finalize, switchMap, takeUntil } from 'rxjs/operators';
import 'devextreme/ui/html_editor/converters/markdown';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormControlName,
} from '@angular/forms';
import { GenericValidator } from 'src/app/shared/validator/generic-validator';
import { IndustryService } from '../../industry/industry.service';
import { Industry } from '../../industry/industry.model';
import { EMAIL_PATTERN } from 'src/app/shared/constants/common.const';
import { StringValidator } from 'src/app/shared/validator/string-validator';
import { Broker } from '../../broker/broker.model';
import { Team } from '../../team/team.model';
import { BrokerService } from '../../broker/broker.service';
import { TeamService } from '../../team/team.service';
import CustomStore from 'devextreme/data/custom_store';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';

@Component({
  selector: 'app-brand-new',
  templateUrl: './brand-new.component.html',
  styleUrls: ['./brand-new.component.scss'],
})
export class BrandNewComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  // Object Fields
  brand: Brand;
  checkingPassword: string;
  industry: Industry;
  industries: Industry[] = [];
  selectedIndustry: Industry;
  teams: Team[];
  brokers: Broker[];
  selectedBroker: Broker;
  brokerGridDataSource: any;
  brokerGridBoxValue: number[] = [];
  isBrokerGridBoxOpened: boolean = false;
  brokerGridColumns: any = [{
    caption:"Tên",
    dataField: "name",
    dataType: "string",
  },
  {
    caption:"Số điện thoại",
    dataField: "phoneNumber",
    dataType: "string",
  },{
    caption:"Tên nhóm",
    dataField: "teamName",
    dataType: "string",
  },];

  //image
  avatarFile: File;

  changedImg: boolean;

  //editor
  editorValueType = 'html';

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
    private router: Router,
    private fb: FormBuilder,
    private industryServices: IndustryService,
    private brokerServices: BrokerService,
    private teamServices: TeamService,
    private ref: ChangeDetectorRef,
    private reloadService: ReloadRouteService
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;

    //editor
    this.editorValueType = 'html';

    //image
    this.changedImg = false;

    this.brand = new Brand(null, false);
    this.selectedIndustry = null;

    //validate
    this.validationMessages = {
      userName: {
        required: 'Tài khoản không được để trống.',
      },
      name: {
        required: 'Tên không được để trống.',
        minlength: 'Tên phải có ít nhất 3 kí tự.',
        maxlength: 'Tên có nhiều nhất 100 kí tự.',
      },
      phoneNumber: {
        required: 'Số điện thoại không được để trống.',
      },
      email: {
        required: 'Địa chỉ email không được để trống.',
        pattern: 'Địa chỉ email không hợp lệ',
      },
      industry: {
        required: 'Ngành kinh doanh không được để trống.',
      },
      registrationNumber: {
        required: 'Mã số doanh nghiệp không được để trống.',
      },
      password: {
        required: 'Mật khẩu không được để trống.',
      },
      checkingPassword: {
        required: 'Mật khẩu xác nhận không được để trống.',
        passwordNotMatch: 'Mật khẩu xác nhận phải trùng với mật khẩu mới',
      },
      broker: {
        required: 'Nhân viên môi giới không được để trống.',
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
      email: ['', [Validators.required, Validators.pattern(EMAIL_PATTERN)]],
      phoneNumber: ['', [Validators.required]],
      industry: ['', [Validators.required]],
      registrationNumber: ['', [Validators.required]],
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]],
      checkingPassword: [
        '',
        [Validators.required, StringValidator.confirmedPassword('password')],
      ],
      broker: ['', [Validators.required]],
    });

    // Load Data
    this.industryServices
      .getIndustries()
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((industries) => {
          this.industries = industries;
          return this.teamServices.getTeams();
        }),
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
      this.isShowSkeleton = false;
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
    return item && `${item.name} <${item.phoneNumber} ${item.teamName}>`;
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

  saveBrand() {
    this.loading = true;
    this.brandServices
      .getBrandByUserName(this.brand.userName)
      .subscribe((brands) => {
        if (brands.length > 0) {
          this.brand.userName = null;
          swal.fire({
            title: 'Cảnh báo!',
            text: 'Tài khoản đã tồn tại.',
            icon: 'warning',
            customClass: {
              confirmButton: 'btn btn-warning animation-on-hover',
            },
            buttonsStyling: false,
            timer: 2000,
          });
        } else {
          this.brand.industryId = this.selectedIndustry.id;
          this.brand.email = this.brand.email.toLowerCase();
          if (this.brand.phoneNumber.includes(' ')) {
            let phoneArr = this.brand.phoneNumber.split(' ');
            this.brand.phoneNumber = phoneArr[0] + phoneArr[1] + phoneArr[2];
          }
          this.brand.brokerId = this.selectedBroker.id;
          this.brand = new Brand(this.brand, true);
          this.brandServices
            .insertBrand(this.brand)
            .pipe(
              takeUntil(this.destroySubs$),
              switchMap((brand) => {
                //update image
                if (this.changedImg && this.avatarFile) {
                  let formData: FormData = new FormData();
                  formData.append('file', this.avatarFile);
                  return this.brandServices.updateBrandAvatar(
                    brand.id,
                    formData
                  );
                }
                return of(brand);
              }),
              finalize(() => (this.loading = false))
            )
            .subscribe(
              (brandAfterUpdateImage) => {
                //get industry
                this.brand = this.updateIndustry(brandAfterUpdateImage);
                swal.fire({
                  title: 'Thành công!',
                  text: 'Tạo mới thương hiệu thành công.',
                  icon: 'success',
                  customClass: {
                    confirmButton: 'btn btn-success animation-on-hover',
                  },
                  buttonsStyling: false,
                  timer: 2000,
                });
                this.brandForm.reset();
                this.hideDialog();
              },
              (error) => {
                swal.fire({
                  title: 'Thất bại!',
                  text:
                    'Tạo mới thương hiệu thất bại với lỗi ' + error.error.title,
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
      });
  }
}
