import {
  AfterViewInit,
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChildren,
} from "@angular/core";
import { MessageService } from "primeng/api";
import { Admin } from "../admin.model";
import { AdminService } from "../admin.service";
import swal from "sweetalert2";
import { fromEvent, merge, Observable, Subject, throwError } from "rxjs";
import {
  takeUntil,
  switchMap,
  debounceTime,
  catchError,
  finalize,
} from "rxjs/operators";
import {
  FormBuilder,
  FormControlName,
  FormGroup,
  Validators,
} from "@angular/forms";
import { GenericValidator } from "src/app/shared/validator/generic-validator";
import { EMAIL_PATTERN } from "src/app/shared/constants/common.const";
import { Router } from "@angular/router";

@Component({
  selector: "app-new-admin",
  templateUrl: "./new-admin.component.html",
  styleUrls: ["./new-admin.component.scss"],
})
export class NewAdminComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  loading: boolean;

  admin: Admin;

  //gender
  genders: any;

  selectedGender = { gender: "Nam" };

  //date
  maxDate: Date = new Date();

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
    private services: AdminService,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.admin = new Admin(null);
    this.genders = [{ gender: "Nam" }, { gender: "Nữ" }];
    this.maxDate = new Date(
      this.maxDate.setFullYear(this.maxDate.getFullYear() - 18)
    );
    //image
    this.changedImg = false;

    this.isShowSkeleton = true;

    this.loading = false;

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
      userName: {
        required: "Tài khoản không được để trống.",
      },
      gender: {
        required: "Giới tính không được để trống.",
      },
      password: {
        required: "Mật khẩu không được để trống.",
      },
      birthday: {
        required: "Ngày sinh không được để trống.",
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
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
      email: ["", [Validators.required, Validators.pattern(EMAIL_PATTERN)]],
      phoneNumber: ["", [Validators.required]],
      password: ["", [Validators.required]],
      birthday: ["", [Validators.required]],
      gender: ["", [Validators.required]],
      userName: ["", [Validators.required]],
    });
    this.admin.birthday = this.maxDate;
    this.adminForm.controls.birthday.setValue(this.maxDate);

    this.isShowSkeleton = false;
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

  // Image Function
  getImgFromChild(imgFile) {
    this.avatarFile = imgFile;
    this.changedImg = true;
  }

  createAdmin() {
    this.loading = true;
    this.selectedGender.gender == "Nam"
      ? (this.admin.gender = true)
      : (this.admin.gender = false);
    let phoneArr = this.admin.phoneNumber.split(" ");
    this.admin.phoneNumber = phoneArr[0] + phoneArr[1] + phoneArr[2];
    this.admin.email = this.admin.email.toLowerCase();
    this.services
      .createAdmin(this.admin)
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((admin) => {
          swal.fire({
            title: "Thành công!",
            text: "Tạo mới admin thành công.",
            icon: "success",
            customClass: {
              confirmButton: "btn btn-success animation-on-hover",
            },
            buttonsStyling: false,
            timer: 2000,
          });
          let formData: FormData = new FormData();
          formData.append("file", this.avatarFile);
          return this.services.updateAvatarAdmin(admin.id, formData);
        }),
        catchError((err) => {
          if (err.error.message === "User name is already exist") {
            swal.fire({
              title: "Thất bại!",
              text: "Tài khoản này đã tồn tại",
              icon: "error",
              customClass: {
                confirmButton: "btn btn-info animation-on-hover",
              },
              buttonsStyling: false,
              timer: 2000,
            });
          } else {
            swal.fire({
              title: "Thất bại!",
              text: "Tạo mới admin thất bại",
              icon: "error",
              customClass: {
                confirmButton: "btn btn-info animation-on-hover",
              },
              buttonsStyling: false,
              timer: 2000,
            });
           }
          return throwError(err);
        }),
        finalize(() => {
          this.loading = false;
          this.admin.userName = null; //clear field on form
          this.admin.phoneNumber = null;
        })
      )
      .subscribe((err) => {
        this.router
          .navigateByUrl("/", { skipLocationChange: true })
          .then(() => {
            this.router.navigate(["/trang-chu"]);
          });
      });
  }
}
