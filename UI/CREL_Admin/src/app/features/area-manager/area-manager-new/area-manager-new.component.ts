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

    this.genders = [{ gender: "Nam" }, { gender: "N???" }];

    //editor
    this.editorValueType = "html";

    //image
    this.changedImg = false;

    //validate
    this.validationMessages = {
      name: {
        required: "T??n kh??ng ???????c ????? tr???ng.",
        minlength: "T??n ph???i c?? ??t nh???t 3 k?? t???.",
        maxlength: "T??n c?? nhi???u nh???t 100 k?? t???.",
      },
      phoneNumber: {
        required: "S??? ??i???n tho???i kh??ng ???????c ????? tr???ng.",
        invalid: "S??? ??i???n tho???i ???? t???n t???i",
      },
      email: {
        required: "?????a ch??? email kh??ng ???????c ????? tr???ng.",
        pattern: "?????a ch??? email kh??ng h???p l???",
        invalid: "?????a ch??? email ???? t???n t???i",
      },
      gender: {
        required: "Gi???i t??nh kh??ng ???????c ????? tr???ng.",
      },
      userName: {
        required: "T??i kho???n kh??ng ???????c ????? tr???ng.",
        minlength: "T??i kho???n ph???i c?? ??t nh???t 3 k?? t???.",
        maxlength: "T??i kho???n c?? nhi???u nh???t 100 k?? t???.",
        invalid: "T??i kho???n ???? t???n t???i",
      },
      birthday: {
        required: "Ng??y sinh kh??ng ???????c ????? tr???ng.",
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
            title: "Th??nh c??ng!",
            text: "T???o m???i ng?????i qu???n l?? khu v???c th??nh c??ng.",
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
              title: "Th???t b???i!",
              text: "T???o m???i th???t b???i vui l??ng th??? l???i.",
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
