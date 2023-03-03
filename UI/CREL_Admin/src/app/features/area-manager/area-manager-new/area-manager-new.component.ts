import {
  Component,
  OnInit,
  OnDestroy,
  ElementRef,
  ViewChildren,
  AfterViewInit,
} from "@angular/core";
import { Router } from "@angular/router";
import { fromEvent, merge, Observable, Subject } from "rxjs";
import { AreaManager } from "../area-manager.model";
import { AreaManagerService } from "../area-manager.service";
import swal from "sweetalert2";
import { debounceTime, finalize, switchMap, takeUntil } from "rxjs/operators";
import "devextreme/ui/html_editor/converters/markdown";
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormControlName,
} from "@angular/forms";
import { GenericValidator } from "src/app/shared/validator/generic-validator";
import { TeamService } from "../../team/team.service";
import { Team } from "../../team/team.model";
import { EMAIL_PATTERN } from "src/app/shared/constants/common.const";
import moment from "moment";
import { PrimeNGConfig } from "primeng/api";
import { default as data } from "../../../../assets/json/vi.json";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { StringValidator } from "src/app/shared/validator/string-validator";

@Component({
  selector: "app-area-manager-new",
  templateUrl: "./area-manager-new.component.html",
  styleUrls: ["./area-manager-new.component.scss"],
})
export class AreaManagerNewComponent
  implements OnInit, AfterViewInit, OnDestroy
{
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  //areaManager
  areaManager: AreaManager;

  //gender
  genders: any;

  selectedGender = { gender: "Nam" };

  //date
  maxDate: Date = new Date();
  minDate: Date = new Date();

  //image
  avatarFile: File;

  changedImg: boolean;

  //editor
  editorValueType = "html";

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = "";

  areaManagerForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  constructor(
    private areaManagerServices: AreaManagerService,
    private router: Router,
    private fb: FormBuilder,
    private teamServices: TeamService,
    public primengConfig: PrimeNGConfig,
    private reloadService: ReloadRouteService
  ) {
    // Config VI for calendar
    this.primengConfig.setTranslation(data.primeng);

    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;

    this.areaManager = new AreaManager(null, false);

    this.maxDate = new Date(
      this.maxDate.setFullYear(this.maxDate.getFullYear() - 18)
    );
    this.minDate = new Date(
      this.minDate.setFullYear(this.minDate.getFullYear() - 100)
    );

    this.genders = [{ gender: "Nam" }, { gender: "Nữ" }];

    //editor
    this.editorValueType = "html";

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
        invalid: "Số điện thoại đã tồn tại",
      },
      email: {
        required: "Địa chỉ email không được để trống.",
        pattern: "Địa chỉ email không hợp lệ",
        invalid: "Địa chỉ email đã tồn tại",
      },
      gender: {
        required: "Giới tính không được để trống.",
      },
      userName: {
        required: "Tài khoản không được để trống.",
        minlength: "Tài khoản phải có ít nhất 3 kí tự.",
        maxlength: "Tài khoản có nhiều nhất 100 kí tự.",
        invalid: "Tài khoản đã tồn tại",
      },
      birthday: {
        required: "Ngày sinh không được để trống.",
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.areaManagerForm = this.fb.group({
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
      gender: ["", [Validators.required]],
      userName: [
        "",
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ],
      ],
      birthday: ["", [Validators.required]],
    });

    // Load Data
    this.areaManager.birthday = new Date(this.maxDate);
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
    this.reloadService.routingReload("/nhan-su/nguoi-quan-ly-khu-vuc", null);
  }

  cancelDialog() {
    this.reloadService.routingReload("/nhan-su/nguoi-quan-ly-khu-vuc", null);
  }

  //data functions

  // Image Function
  getImgFromChild(imgFile) {
    this.avatarFile = imgFile;
    this.changedImg = true;
  }

  saveAreaManager() {
    this.loading = true;
    this.areaManager.email = this.areaManager.email.toLowerCase();
    if (this.areaManager.phoneNumber.includes(" ")) {
      let phoneArr = this.areaManager.phoneNumber.split(" ");
      this.areaManager.phoneNumber = phoneArr[0] + phoneArr[1] + phoneArr[2];
    }
    this.selectedGender.gender == "Nam"
      ? (this.areaManager.gender = true)
      : (this.areaManager.gender = false);

    this.areaManager.birthday = new Date(
      moment(this.areaManager.birthday).format("YYYY-MM-DDThh:mm:ssZ")
    );
    this.areaManager.status = 1;
    this.areaManager = new AreaManager(this.areaManager, true);
    this.areaManagerServices
      .insertAreaManager(this.areaManager)
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((areaManager) => {

          //update image
          if (this.changedImg && this.avatarFile) {
            let formData: FormData = new FormData();
            formData.append("file", this.avatarFile);
            return this.areaManagerServices.updateAreaManagerAvatar(
              areaManager.id,
              formData
            );
          }
          return this.areaManagerServices.updateAreaManager(areaManager);
        }),
        finalize(() => (this.loading = false))
      )
      .subscribe(
        (areaManagerAfterUpdateImage) => {
          swal.fire({
            title: "Thành công!",
            text: "Tạo mới người quản lý khu vực thành công.",
            icon: "success",
            customClass: {
              confirmButton: "btn btn-success animation-on-hover",
            },
            buttonsStyling: false,
            timer: 2000,
          });
          this.areaManagerForm.reset();
          this.hideDialog();
        },
        (error) => {
          if (error.error.message === "UserName is already exist") {
            this.areaManagerForm
              .get("userName")
              .setValidators([StringValidator.invalid()]);
            this.areaManagerForm.controls["userName"].updateValueAndValidity();
          } else if (error.error.message === "Email is already exist") {
            this.areaManagerForm
              .get("email")
              .setValidators([StringValidator.invalid()]);
            this.areaManagerForm.controls["email"].updateValueAndValidity();
          } else if (error.error.message === "PhoneNumber is already exist") {
            this.areaManagerForm
              .get("phoneNumber")
              .setValidators([StringValidator.invalid()]);
            this.areaManagerForm.controls[
              "phoneNumber"
            ].updateValueAndValidity();
          } else {
            swal.fire({
              title: "Thất bại!",
              text: "Tạo mới thất bại vui lòng thử lại.",
              icon: "error",
              customClass: {
                confirmButton: "btn btn-info animation-on-hover",
              },
              buttonsStyling: false,
              timer: 2000,
            });
          }
        }
      );
  }
  onChangeUserName() {
    this.areaManagerForm.get("userName").clearValidators();
    this.areaManagerForm
      .get("userName")
      .setValidators([
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(100),
      ]);
    this.areaManagerForm.controls["userName"].updateValueAndValidity();
  }
  onChangeEmail() {
    this.areaManagerForm.get("email").clearValidators();
    this.areaManagerForm
      .get("email")
      .setValidators([Validators.required, Validators.pattern(EMAIL_PATTERN)]);
    this.areaManagerForm.controls["email"].updateValueAndValidity();
  }
  onChangePhone() {
    this.areaManagerForm.get("phoneNumber").clearValidators();
    this.areaManagerForm
      .get("phoneNumber")
      .setValidators([Validators.required]);
    this.areaManagerForm.controls["phoneNumber"].updateValueAndValidity();
  }
}
