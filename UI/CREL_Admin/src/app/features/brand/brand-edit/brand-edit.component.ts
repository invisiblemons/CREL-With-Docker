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
import { StringValidator } from 'src/app/shared/validator/string-validator';

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
    private reloadService: ReloadRouteService,
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;
    this.statuses = [
      { label: 'Đã xoá', value: 0 },
      { label: 'Đợi xét duyệt', value: 1 },
      { label: 'Được duyệt', value: 2 },
      { label: 'Không được duyệt', value: 3 },
    ];

    this.reasons = [
      'Lỗi nội dung sai sự thật',
      'Lỗi hình ảnh không liên quan',
      'Lỗi dùng từ ngữ thô tục',
      'Lỗi nội dung chứa thông tin phân biệt chủng tộc, vùng miền',
    ];

    //editor
    this.editorValueType = 'html';

    //image
    this.changedImg = false;

    //validate
    this.validationMessages = {
      userName: {
        required: "Tài khoản không được để trống.",
        minlength: "Tài khoản phải có ít nhất 3 kí tự.",
        maxlength: "Tài khoản có nhiều nhất 100 kí tự.",
        invalid: 'Tài khoản đã tồn tại',
      },
      name: {
        required: 'Tên không được để trống.',
        minlength: 'Tên phải có ít nhất 3 kí tự.',
        maxlength: 'Tên có nhiều nhất 100 kí tự.',
      },
      phoneNumber: {
        required: 'Số điện thoại không được để trống.',
        invalid: 'Số điện thoại đã tồn tại',
      },
      email: {
        required: 'Địa chỉ email không được để trống.',
        pattern: 'Địa chỉ email không hợp lệ',
        invalid: 'Địa chỉ email đã tồn tại',
      },
      status: {
        required: 'Trạng thái không được để trống.',
      },
      selectedIndustry: {
        required: 'Ngành kinh doanh không được để trống.',
      },
      registrationNumber: {
        required: 'Mã số doanh nghiệp được để trống.',
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
      status: ['', [Validators.required]],
      selectedIndustry: ['', [Validators.required]],
      registrationNumber: ['', [Validators.required]],
      userName: [{ value: "", disabled: true }],
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
          return this.industryServices.getIndustries();
        })
      )
      .subscribe((industries) => {
        this.isShowSkeleton = false;
        this.industries = industries;
        this.brand = this.updateIndustry(this.brand);
        this.selectedIndustry = this.brand.industry;
        this.statuses.forEach((status) => {
          status.value === this.brand.status
            ? (this.selectedStatus = status)
            : '';
        });
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
      this.brand.email = this.brand.email.toLowerCase();
      if (this.brand.phoneNumber.includes(' ')) {
        let phoneArr = this.brand.phoneNumber.split(' ');
        this.brand.phoneNumber = phoneArr[0] + phoneArr[1] + phoneArr[2];
      }
      this.brand.registrationNumber = this.brand.registrationNumber.toString();
      this.brand = new Brand(this.brand, false);
      this.brandServices
        .updateBrand(this.brand)
        .pipe(
          takeUntil(this.destroySubs$),
          switchMap((brand) => {
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

            swal.fire({
              title: 'Thành công!',
              text: 'Cập nhật thương hiệu thành công.',
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
            if (error.error.message === 'UserName is already exist') {
              this.brandForm
                .get('userName')
                .setValidators([StringValidator.invalid()]);
              this.brandForm.controls['userName'].updateValueAndValidity();
            } else if (error.error.message === 'Email is already exist') {
              this.brandForm
                .get('email')
                .setValidators([StringValidator.invalid()]);
              this.brandForm.controls['email'].updateValueAndValidity();
            } else if (error.error.message === 'PhoneNumber is already exist') {
              this.brandForm
                .get('phoneNumber')
                .setValidators([StringValidator.invalid()]);
              this.brandForm.controls['phoneNumber'].updateValueAndValidity();
            } else {
              swal.fire({
                title: 'Thất bại!',
                text: 'Tạo mới tài khoản thất bại vui lòng thử lại.',
                icon: 'error',
                customClass: {
                  confirmButton: 'btn btn-info animation-on-hover',
                },
                buttonsStyling: false,
                timer: 2000,
              });
            }
          }
        );
    }
  }

  onChangeUserName() {
    this.brandForm.get('userName').clearValidators();
    this.brandForm
      .get('userName')
      .setValidators([
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(100),
      ]);
    this.brandForm.controls['userName'].updateValueAndValidity();
  }
  onChangeEmail() {
    this.brandForm.get('email').clearValidators();
    this.brandForm
      .get('email')
      .setValidators([Validators.required, Validators.pattern(EMAIL_PATTERN)]);
    this.brandForm.controls['email'].updateValueAndValidity();
  }
  onChangePhone() {
    this.brandForm.get('phoneNumber').clearValidators();
    this.brandForm.get('phoneNumber').setValidators([Validators.required]);
    this.brandForm.controls['phoneNumber'].updateValueAndValidity();
  }

  //extend
  changeStatus(ev) {
    this.selectedStatus = ev.value;
    if(this.selectedStatus.value !== 1) {
      this.brandForm.controls.broker.markAsPristine();
    }
  }
}
