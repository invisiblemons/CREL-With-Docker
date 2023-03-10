import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  SimpleChanges,
  ViewChildren,
} from '@angular/core';
import {
  FormBuilder,
  FormControlName,
  FormGroup,
  Validators,
} from '@angular/forms';
import {
  debounceTime,
  fromEvent,
  merge,
  Observable,
  of,
  Subject,
  switchMap,
  takeUntil,
} from 'rxjs';
import { Industry } from 'src/app/shared/models/industry.model';
import { IndustryService } from 'src/app/shared/services/industry.service';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';
import { GenericValidator } from 'src/app/shared/validator/generic-validator';
import { Brand } from '../../brand/brand.model';
import { LocalStorageService } from '../local-storage.service';
import swal from 'sweetalert2';
import { BrandService } from '../../brand/brand.service';
import { AuthService } from '../auth.service';
import { Account } from '../user.model';
import { StringValidator } from 'src/app/shared/validator/string-validator';
import { EMAIL_PATTERN } from 'src/app/shared/constants/common.const';

@Component({
  selector: 'app-create-by-userpass',
  templateUrl: './create-by-userpass.component.html',
  styleUrls: ['./create-by-userpass.component.scss'],
})
export class CreateByUserpassComponent
  implements OnInit, OnDestroy, AfterViewInit
{
  destroySubs$: Subject<boolean> = new Subject<boolean>();
  industries: Industry[];
  selectedIndustry: Industry;
  loading: boolean = false;

  @Input() brand: Brand;

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];
  createForm!: FormGroup;
  errorMessage = '';
  editorValueType: string;
  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;

  isShowDialog: boolean = true;

  //image
  avatarFile: File;
  changedImg: boolean;

  account: Account;

  @Output() stateCreateWithUsernameForm = new EventEmitter();

  constructor(
    private fb: FormBuilder,
    private localStorageService: LocalStorageService,
    private reloadService: ReloadRouteService,
    private industryService: IndustryService,
    private brandService: BrandService,
    private authService: AuthService
  ) {
    //validate
    this.validationMessages = {
      name: {
        required: 'T??n kh??ng ???????c ????? tr???ng.',
        minlength: 'T??n ph???i c?? ??t nh???t 3 k?? t???.',
        maxlength: 'T??n c?? nhi???u nh???t 100 k?? t???.',
      },
      userName: {
        required: 'T??i kho???n kh??ng ???????c ????? tr???ng.',
        minlength: 'T??i kho???n ph???i c?? ??t nh???t 3 k?? t???.',
        maxlength: 'T??i kho???n c?? nhi???u nh???t 100 k?? t???.',
        invalid: 'T??i kho???n ???? t???n t???i',
      },
      password: {
        required: 'M???t kh???u kh??ng ???????c ????? tr???ng.',
      },
      confirmedPassword: {
        required: 'M???t kh???u kh??ng ???????c ????? tr???ng.',
        passwordNotMatch: 'M???t kh???u x??c nh???n ph???i tr??ng v???i m???t kh???u ???? nh???p',
      },
      email: {
        required: '?????a ch??? email kh??ng ???????c ????? tr???ng.',
        pattern: '?????a ch??? email kh??ng h???p l???',
        invalid: '?????a ch??? email ???? t???n t???i',
      },
      industry: {
        required: 'Ng??nh kinh doanh kh??ng ???????c ????? tr???ng.',
      },
      phoneNumber: {
        required: 'S??? ??i???n tho???i kh??ng ???????c ????? tr???ng.',
        invalid: 'S??? ??i???n tho???i ???? t???n t???i',
      },
      registrationNumber: {
        required: 'M?? s??? doanh nghi???p ???????c ????? tr???ng.',
      },
    };

    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.createForm = this.fb.group({
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ],
      ],
      email: ['', [Validators.required, Validators.pattern(EMAIL_PATTERN)]],
      industry: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      registrationNumber: ['', [Validators.required]],
      description: [''],
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['brand'].currentValue) {
      this.brand = changes['brand'].currentValue;
    }
  }

  ngAfterViewInit(): void {
    // Watch for the blur event from any input element on the form.
    // This is required because the valueChanges does not provide notification on blur
    const controlBlurs: Observable<any>[] = this.formInputElements.map(
      (formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur')
    );

    // Merge the blur event observable with the valueChanges observable
    // so we only need to subscribe once.
    merge(this.createForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe(() => {
        this.displayMessage = this.genericValidator.processMessages(
          this.createForm
        );
      });
  }

  ngOnDestroy() {
    var body = document.getElementsByTagName('body')[0];
    body.classList.remove('authen-page');
  }

  hideCreateAccountDialog() {
    //Log out
    this.localStorageService.removeUser();
    this.reloadService.routingReload('/trang-chu', null);
  }

  loadIndustries() {
    this.industryService.getIndustries().subscribe((res) => {
      this.industries = res;
    });
  }

  // Image Function
  getImgFromChild(imgFile) {
    this.avatarFile = imgFile;
    this.changedImg = true;
  }

  hideDialog() {
    this.stateCreateWithUsernameForm.emit();
  }

  createAccount() {
    this.loading = true;
    this.brand.industryId = this.selectedIndustry.id;
    this.brand.registrationNumber = this.brand.registrationNumber.toString();
    if (this.brand.phoneNumber.includes(' ')) {
      let phoneArr = this.brand.phoneNumber.split(' ');
      this.brand.phoneNumber = phoneArr[0] + phoneArr[1] + phoneArr[2];
    }
    this.brand = new Brand(this.brand, true);
    this.brandService
      .createBrand(this.brand)
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((brand) => {
          return this.brandService.updateBrandPassword({newPassword:this.brand.password, oldPassword: null});
        }),
        switchMap((brand) => {
          //update image
          if (this.changedImg && this.avatarFile) {
            let formData: FormData = new FormData();
            formData.append('file', this.avatarFile);
            return this.brandService.updateBrandAvatar(formData);
          } else return of(brand);
        })
      )
      .subscribe({
        next: (brandAfterUpdateImage) => {
          this.account = new Account(null);
          this.account.userName = this.brand.userName;
          this.account.password = this.brand.password;
          this.authService.loginWithAccount(this.account).subscribe({
            next: (res) => {
              swal.fire({
                title: 'Th??nh c??ng!',
                text: 'C???p nh???t t??i kho???n m???i th??nh c??ng.',
                icon: 'success',
                customClass: {
                  confirmButton: 'btn btn-success animation-on-hover',
                },
                buttonsStyling: false,
                timer: 1000,
              });
              this.loading = false;
              this.localStorageService.setUser(res);

              this.reloadService.routingReload('/mat-bang', null);
            },
          });
        },
        error: (error) => {
          if (error.error.message === 'UserName is already exist') {
            this.createForm
              .get('userName')
              .setValidators([StringValidator.invalid()]);
            this.createForm.controls['userName'].updateValueAndValidity();
          }
          else if (error.error.message === 'Email is already exist') {
            this.createForm
              .get('email')
              .setValidators([StringValidator.invalid()]);
            this.createForm.controls['email'].updateValueAndValidity();
          }
          else if (error.error.message === 'PhoneNumber is already exist') {
            this.createForm
              .get('phoneNumber')
              .setValidators([StringValidator.invalid()]);
            this.createForm.controls['phoneNumber'].updateValueAndValidity();
          } else {
            swal.fire({
              title: 'Th???t b???i!',
              text: 'T???o m???i t??i kho???n th???t b???i vui l??ng th??? l???i.',
              icon: 'error',
              customClass: {
                confirmButton: 'btn btn-info animation-on-hover',
              },
              buttonsStyling: false,
              timer: 2000,
            });
          }

          this.loading = false;
        },
      });
  }

  onChangeUserName() {
    this.createForm.get('userName').clearValidators();
    this.createForm
      .get('userName')
      .setValidators([
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(100),
      ]);
    this.createForm.controls['userName'].updateValueAndValidity();
  }
  onChangeEmail() {
    this.createForm.get('email').clearValidators();
    this.createForm
      .get('email')
      .setValidators([Validators.required, Validators.pattern(EMAIL_PATTERN)]);
    this.createForm.controls['email'].updateValueAndValidity();
  }
  onChangePhone() {
    this.createForm.get('phoneNumber').clearValidators();
    this.createForm.get('phoneNumber').setValidators([Validators.required]);
    this.createForm.controls['phoneNumber'].updateValueAndValidity();
  }
}
