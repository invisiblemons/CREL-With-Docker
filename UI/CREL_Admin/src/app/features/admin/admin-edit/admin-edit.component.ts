import {
  Component,
  OnInit,
  AfterViewInit,
  OnDestroy,
  ViewChildren,
  ElementRef,
  ChangeDetectorRef,
} from "@angular/core";
import {
  FormBuilder,
  FormControlName,
  FormGroup,
  Validators,
} from "@angular/forms";
import { fromEvent, merge, Observable, Subject, throwError } from "rxjs";
import {
  catchError,
  debounceTime,
  finalize,
  switchMap,
  takeUntil,
} from "rxjs/operators";
import { EMAIL_PATTERN } from "src/app/shared/constants/common.const";
import { GenericValidator } from "src/app/shared/validator/generic-validator";
import swal from "sweetalert2";
import { Admin } from "../admin.model";
import { AdminService } from "../admin.service";
import { LocalStorageService } from "../../login/local-storage.service";
import { Router } from "@angular/router";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { PrimeNGConfig } from "primeng/api";
import { default as data } from "../../../../assets/json/vi.json";
import moment from "moment";

@Component({
  selector: "app-admin-edit",
  templateUrl: "./admin-edit.component.html",
  styleUrls: ["./admin-edit.component.scss"],
})
export class AdminEditComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowDialog: boolean;

  isShowSkeleton: boolean;

  loading: boolean;

  admin: Admin;

  // PhoneNumber
  isChangedPhoneNumber: boolean;

  //gender
  genders: any;

  selectedGender = { gender: "Nam" };

  //date
  maxDate: Date = new Date();
  minDate: Date = new Date();

  //image
  avatarFile: File;

  changedImg: boolean;

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = "";

  adminForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  constructor(
    private adminServices: AdminService,
    private localStorage: LocalStorageService,
    private fb: FormBuilder,
    private ref: ChangeDetectorRef,
    private router: Router,
    private reloadService: ReloadRouteService,
    public primengConfig: PrimeNGConfig
  ) {
    // Config VI for calendar
    this.primengConfig.setTranslation(data.primeng);

    this.admin = new Admin(null);
    this.genders = [{ gender: "Nam" }, { gender: "Nữ" }];

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
        required: "Tên không được để trống.",
        minlength: "Tên phải có ít nhất 3 kí tự.",
        maxlength: "Tên có nhiều nhất 100 kí tự.",
      },
      phoneNumber: {
        required: "Số điện thoại không được để trống.",
      },
      email: {
        required: "Địa chỉ email không được để trống.",
        pattern: "Địa chỉ email không hợp lệ",
      },
      gender: {
        required: "Giới tính không được để trống.",
      },
      birthday: {
        required: "Ngày sinh không được để trống.",
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
    //validate
    this.adminForm = this.fb.group({
      name: [
        "",
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ],
      ],
      userName: [{ value: "", disabled: true }],
      email: ["", [Validators.required, Validators.pattern(EMAIL_PATTERN)]],
      phoneNumber: ["", [Validators.required]],
      birthday: ["", [Validators.required]],
      gender: ["", [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.adminServices
      .getAdminById(this.localStorage.getUserObject().id)
      .subscribe((admin) => {
        this.admin = admin;
        this.admin.gender == true
          ? (this.selectedGender = { gender: "Nam" })
          : (this.selectedGender = { gender: "Nữ" });
        this.admin.birthday = new Date(this.admin.birthday);
        this.adminForm.controls.gender.setValue(this.selectedGender);
        this.adminForm.controls.phoneNumber.setValue(this.admin.phoneNumber);
        this.isShowSkeleton = false;
        this.ref.detectChanges();
      });
  }

  ngAfterViewInit(): void {
    // Watch for the blur event from any input element on the form.
    // This is required because the valueChanges does not provide notification on blur
    const controlBlurs: Observable<any>[] = this.formInputElements.map(
      (formControl: ElementRef) => fromEvent(formControl.nativeElement, "blur")
    );

    // Merge the blur event observable with the valueChanges observable
    // so we only need to subscribe once.
    merge(this.adminForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.adminForm
        );
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingReload("/ho-so", null);
  }

  cancelDialog() {
    this.reloadService.routingNotReload("/ho-so", null);
  }

  // Image Function
  getImgFromChild(imgFile) {
    this.avatarFile = imgFile;
    this.changedImg = true;
  }

  changeInputPhoneStyle() {
    this.isChangedPhoneNumber = true;
  }

  updateAdmin() {
    this.selectedGender.gender == "Nam"
      ? (this.admin.gender = true)
      : (this.admin.gender = false);
    if (this.isChangedPhoneNumber) {
      if (this.admin.phoneNumber.includes(" ")) {
        let phoneArr = this.admin.phoneNumber.split(" ");
        this.admin.phoneNumber = phoneArr[0] + phoneArr[1] + phoneArr[2];
      }
    }
    this.admin.birthday = new Date(
      moment(this.admin.birthday).format("YYYY-MM-DDThh:mm:ssZ")
    );
    this.admin.email = this.admin.email.toLowerCase();
    this.adminServices
      .updateAdmin(this.admin)
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((admin) => {
          this.localStorage.setUserObject(admin);
          swal.fire({
            title: "Thành công!",
            text: "Cập nhật admin thành công.",
            icon: "success",
            customClass: {
              confirmButton: "btn btn-success animation-on-hover",
            },
            buttonsStyling: false,
            timer: 2000,
          });
          this.adminForm.reset();
          this.hideDialog();
          if (this.changedImg) {
            let formData: FormData = new FormData();
            formData.append("file", this.avatarFile);
            return this.adminServices.updateAvatarAdmin(admin.id, formData);
          }
        }),
        catchError((err) => {
          swal.fire({
            title: "Thất bại!",
            text: "Cập nhật admin thất bại",
            icon: "error",
            customClass: {
              confirmButton: "btn btn-info animation-on-hover",
            },
            buttonsStyling: false,
            timer: 2000,
          });
          return throwError(err);
        }),
        finalize(() => {
          this.loading = false;
        })
      )
      .subscribe((areaManager) => {
        this.localStorage.setUserObject(areaManager);
      });
  }
}
