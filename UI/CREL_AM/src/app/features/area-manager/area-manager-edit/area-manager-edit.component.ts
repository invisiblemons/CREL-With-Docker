import {
  Component,
  OnInit,
  ChangeDetectionStrategy,
  AfterViewInit,
  OnDestroy,
  ViewChildren,
  ElementRef,
  ChangeDetectorRef,
} from '@angular/core';
import {
  FormBuilder,
  FormControlName,
  FormGroup,
  Validators,
} from '@angular/forms';
import { fromEvent, merge, Observable, of, Subject, throwError } from 'rxjs';
import {
  catchError,
  debounceTime,
  finalize,
  switchMap,
  takeUntil,
} from 'rxjs/operators';
import { EMAIL_PATTERN } from 'src/app/shared/constants/common.const';
import { GenericValidator } from 'src/app/shared/validator/generic-validator';
import swal from 'sweetalert2';
import { DatePipe } from '@angular/common';
import { LocalStorageService } from '../../login/local-storage.service';
import { Router } from '@angular/router';
import { AreaManagerService } from '../area-manager.service';
import { AreaManager } from '../area-manager.model';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';
import { PrimeNGConfig } from 'primeng/api';
import { default as data } from '../../../../assets/json/vi.json';
import moment from 'moment';
import { StringValidator } from 'src/app/shared/validator/string-validator';

@Component({
  selector: 'app-area-manager-edit',
  templateUrl: './area-manager-edit.component.html',
  styleUrls: ['./area-manager-edit.component.scss'],
})
export class AreaManagerEditComponent
  implements OnInit, AfterViewInit, OnDestroy
{
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowDialog: boolean;

  isShowSkeleton: boolean;

  loading: boolean;

  areaManager: AreaManager;

  // PhoneNumber
  isChangedPhoneNumber: boolean;

  //gender
  genders: any;

  selectedGender = { gender: 'Nam' };

  //date
  minDate: Date = new Date();
  maxDate: Date = new Date();

  //image
  avatarFile: File;

  changedImg: boolean;

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = '';

  areaManagerForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  constructor(
    private areaManagerServices: AreaManagerService,
    private localStorage: LocalStorageService,
    private fb: FormBuilder,
    private ref: ChangeDetectorRef,
    private router: Router,
    private reloadService: ReloadRouteService,
    public primengConfig: PrimeNGConfig
  ) {
    // Config VI for calendar
    this.primengConfig.setTranslation(data.primeng);

    this.genders = [{ gender: 'Nam' }, { gender: 'N???' }];

    this.maxDate = new Date(
      this.maxDate.setFullYear(this.maxDate.getFullYear() - 18)
    );

    this.minDate = new Date(
      this.minDate.setFullYear(this.minDate.getFullYear() - 100)
    );

    this.isShowSkeleton = true;

    this.isShowDialog = true;

    this.loading = false;

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
        invalid: 'S??? ??i???n tho???i ???? t???n t???i',
      },
      email: {
        required: '?????a ch??? email kh??ng ???????c ????? tr???ng.',
        pattern: '?????a ch??? email kh??ng h???p l???',
        invalid: '?????a ch??? email ???? t???n t???i',
      },
      gender: {
        required: 'Gi???i t??nh kh??ng ???????c ????? tr???ng.',
      },
      birthday: {
        required: 'Ng??y sinh kh??ng ???????c ????? tr???ng.',
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.areaManagerForm = this.fb.group({
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
      birthday: ['', [Validators.required]],
      gender: ['', [Validators.required]],
    });

    // Load Data
    this.areaManagerServices
      .getAreaManagerById(this.localStorage.getUserObject().id)
      .subscribe((areaManager) => {
        this.areaManager = areaManager;
        this.areaManager.birthday = new Date(this.areaManager.birthday);
        this.areaManager.gender == true
          ? (this.selectedGender = { gender: 'Nam' })
          : (this.selectedGender = { gender: 'N???' });
        this.areaManagerForm.controls.gender.setValue(this.selectedGender);
        this.areaManagerForm.controls.phoneNumber.setValue(
          this.areaManager.phoneNumber
        );
        this.isShowSkeleton = false;
        this.ref.detectChanges();
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
    merge(this.areaManagerForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.areaManagerForm
        );
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingReload('/ho-so', null);
  }

  cancelDialog() {
    this.reloadService.routingNotReload('/ho-so', null);
  }

  changeInputPhoneStyle() {
    this.isChangedPhoneNumber = true;
  }

  // Image Function
  getImgFromChild(imgFile) {
    this.avatarFile = imgFile;
    this.changedImg = true;
  }

  updateAreaManager() {
    this.loading = true;
    this.selectedGender.gender == 'Nam'
      ? (this.areaManager.gender = true)
      : (this.areaManager.gender = false);
    if (this.isChangedPhoneNumber) {
      if (this.areaManager.phoneNumber.includes(' ')) {
        let phoneArr = this.areaManager.phoneNumber.split(' ');
        this.areaManager.phoneNumber = phoneArr[0] + phoneArr[1] + phoneArr[2];
      }
    }
    this.areaManager.birthday = new Date(
      moment(this.areaManager.birthday).format('YYYY-MM-DDThh:mm:ssZ')
    );
    this.areaManager.email = this.areaManager.email.toLowerCase();
    this.areaManagerServices
      .updateAreaManager(new AreaManager(this.areaManager, false))
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((areaManager) => {
          this.localStorage.setUserObject(areaManager);
          if (this.changedImg && this.avatarFile) {
            let formData: FormData = new FormData();
            formData.append('file', this.avatarFile);
            return this.areaManagerServices.updateAreaManagerAvatar(formData);
          } else {
            return of(areaManager);
          }
        }),
        finalize(() => {
          this.loading = false;
        })
      )
      .subscribe(
        (areaManager) => {
          this.localStorage.setUserObject(areaManager);
          swal.fire({
            title: 'Th??nh c??ng!',
            text: 'C???p nh???t ng?????i qu???n l?? khu v???c th??nh c??ng.',
            icon: 'success',
            customClass: {
              confirmButton: 'btn btn-success animation-on-hover',
            },
            buttonsStyling: false,
            timer: 2000,
          });
          this.areaManagerForm.reset();
          this.hideDialog();
        },
        (error) => {
          if (error.error.message === 'UserName is already exist') {
            this.areaManagerForm
              .get('userName')
              .setValidators([StringValidator.invalid()]);
            this.areaManagerForm.controls['userName'].updateValueAndValidity();
          } else if (error.error.message === 'Email is already exist') {
            this.areaManagerForm
              .get('email')
              .setValidators([StringValidator.invalid()]);
            this.areaManagerForm.controls['email'].updateValueAndValidity();
          } else if (error.error.message === 'PhoneNumber is already exist') {
            this.areaManagerForm
              .get('phoneNumber')
              .setValidators([StringValidator.invalid()]);
            this.areaManagerForm.controls[
              'phoneNumber'
            ].updateValueAndValidity();
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
        }
      );
  }

  onChangeUserName() {
    this.areaManagerForm.get('userName').clearValidators();
    this.areaManagerForm
      .get('userName')
      .setValidators([
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(100),
      ]);
    this.areaManagerForm.controls['userName'].updateValueAndValidity();
  }
  onChangeEmail() {
    this.areaManagerForm.get('email').clearValidators();
    this.areaManagerForm
      .get('email')
      .setValidators([Validators.required, Validators.pattern(EMAIL_PATTERN)]);
    this.areaManagerForm.controls['email'].updateValueAndValidity();
  }
  onChangePhone() {
    this.areaManagerForm.get('phoneNumber').clearValidators();
    this.areaManagerForm
      .get('phoneNumber')
      .setValidators([Validators.required]);
    this.areaManagerForm.controls['phoneNumber'].updateValueAndValidity();
  }
}
