import { Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import {
  FormBuilder,
  FormControlName,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { fromEvent, merge, Observable } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { GenericValidator } from 'src/app/shared/validator/generic-validator';
import swal from 'sweetalert2';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';
import { LocalStorageService } from '../../login/local-storage.service';
import { AreaManager } from '../area-manager.model';
import { AreaManagerService } from '../area-manager.service';
import { StringValidator } from 'src/app/shared/validator/string-validator';
import { PASSWORD_PATTERN } from 'src/app/shared/constants/common.const';

@Component({
  selector: 'app-password-change',
  templateUrl: './password-change.component.html',
  styleUrls: ['./password-change.component.scss'],
})
export class PasswordChangeComponent implements OnInit {
  isShowSkeleton: boolean;
  loading: boolean;
  isShowDialog: boolean;

  password: string;
  newPassword: string;
  checkingPassword: string;
  areaManager: AreaManager;

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = '';

  areaManagerForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  isNeedUpdating: boolean = false;

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private areaManagerServices: AreaManagerService,
    private reloadService: ReloadRouteService,
    private localStorage: LocalStorageService,
    private route: ActivatedRoute
  ) {
    this.isShowSkeleton = true;

    this.isShowDialog = true;
    this.loading = false; //validate
    this.validationMessages = {
      password: {
        required: 'M???t kh???u c?? kh??ng ???????c ????? tr???ng.',
      },
      newPassword: {
        required: 'M???t kh???u m???i kh??ng ???????c ????? tr???ng.',
        pattern:
          'M???t kh???u ch??a ????ng ?????nh d???ng',
      },
      checkingPassword: {
        required: 'M???t kh???u x??c nh???n kh??ng ???????c ????? tr???ng.',
        passwordNotMatch: 'M???t kh???u x??c nh???n ph???i tr??ng v???i m???t kh???u m???i',
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.areaManagerForm = this.fb.group({
      password: ['', [Validators.required]],
      newPassword: [
        '',
        [Validators.required, Validators.pattern(PASSWORD_PATTERN)],
      ],
      checkingPassword: [
        '',
        [Validators.required, StringValidator.confirmedPassword('newPassword')],
      ],
    });

    // Load Data
    this.areaManager = this.localStorage.getUserObject();
    this.isShowSkeleton = false;

    this.route.queryParams.subscribe((params) => {
      if (params['isNeedUpdating']) {
        this.isNeedUpdating = true;
      }
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

  hideDialog() {
    this.reloadService.routingReload('/ho-so', null);
  }

  onChangeCheckingPassword() {}

  changePassword() {
    this.loading = true;
    this.areaManagerServices
      .updateAreaManagerPassword({
        newPassword: this.newPassword,
        oldPassword: this.password,
      })
      .subscribe(
        (res) => {
          this.loading = false;
          let user = this.localStorage.getUserObject();
          user.status = 2;
          this.localStorage.setUserObject(user);
          this.reloadService.routingReload('/ho-so', null);
          swal.fire({
            title: 'Th??nh c??ng!',
            text: '?????i m???t kh???u th??nh c??ng.',
            icon: 'success',
            customClass: {
              confirmButton: 'btn btn-success animation-on-hover',
            },
            buttonsStyling: false,
            timer: 2000,
          });
        },
        (err) => {
          this.loading = false;
          swal.fire({
            title: 'Th???t b???i!',
            text: 'M???t kh???u c?? kh??ng ch??nh x??c.',
            icon: 'error',
            customClass: {
              confirmButton: 'btn btn-danger animation-on-hover',
            },
            buttonsStyling: false,
            timer: 2000,
          });
        }
      );
  }
}
