import {
  Component,
  OnInit,
  OnDestroy,
  ViewChildren,
  ElementRef,
  AfterViewInit,
} from "@angular/core";
import { fromEvent, merge, Observable, of, Subject } from "rxjs";
import { ActivatedRoute, Router } from "@angular/router";
import { AreaManager } from "../area-manager.model";
import { AreaManagerService } from "../area-manager.service";
import { debounceTime, finalize, switchMap, takeUntil } from "rxjs/operators";
import swal from "sweetalert2";
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormControlName,
} from "@angular/forms";
import { GenericValidator } from "src/app/shared/validator/generic-validator";
import "devextreme/ui/html_editor/converters/markdown";
import { TeamService } from "../../team/team.service";
import { Team } from "../../team/team.model";
import { EMAIL_PATTERN } from "src/app/shared/constants/common.const";
import moment from "moment";
import { PrimeNGConfig } from "primeng/api";
import { default as data } from "../../../../assets/json/vi.json";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { StringValidator } from "src/app/shared/validator/string-validator";

@Component({
  selector: "app-area-manager-edit",
  templateUrl: "./area-manager-edit.component.html",
  styleUrls: ["./area-manager-edit.component.scss"],
})
export class AreaManagerEditComponent
  implements OnInit, AfterViewInit, OnDestroy
{
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  isChangedPhoneNumber: boolean;

  reasons= [];

  //gender
  genders: any;

  selectedGender = { gender: "Nam" };

  //date
  maxDate: Date = new Date();
  minDate: Date = new Date();

  statuses: { label: string; value: number }[];

  selectedStatus: { label: string; value: number };

  //areaManager
  areaManager: AreaManager;

  //image
  avatarFile: File;

  changedImg: boolean;

  //editor
  editorValueType: string;

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = "";

  areaManagerForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  isChangeStatus: boolean = false;

  constructor(
    private areaManagerServices: AreaManagerService,
    private teamServices: TeamService,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    public primengConfig: PrimeNGConfig,
    private reloadService: ReloadRouteService
  ) {
    // Config VI for calendar
    this.primengConfig.setTranslation(data.primeng);

    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;
    this.statuses = [
      { label: "???? xo??", value: 0 },
      { label: "Ho???t ?????ng", value: 1 },
    ];

    this.reasons = [
      "Nh??n vi??n n??y ???? ngh??? l??m",
      "Th??ng tin nh??n vi??n kh??ng c??n ch??nh x??c",
    ];

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
      status: {
        required: "Tr???ng th??i kh??ng ???????c ????? tr???ng.",
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
      userName: [{ value: "", disabled: true }],
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
      status: ["", [Validators.required]],
      gender: ["", [Validators.required]],
      birthday: ["", [Validators.required]],
    });

    // Load Data
    this.route.queryParams
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((params) => {
          return this.areaManagerServices.getAreaManagerById(params["id"]);
        })
      )
      .subscribe((areaManager) => {
        this.areaManager = areaManager;
        if (this.areaManager.status === 0) {
          this.selectedStatus = this.statuses[0];
        } else if (this.areaManager.status === 1 || this.areaManager.status === 2) {
          this.selectedStatus = this.statuses[1];
        }

        //label for gender
        this.areaManager.gender == true
          ? (this.selectedGender = { gender: "Nam" })
          : (this.selectedGender = { gender: "N???" });

        this.areaManager.birthday = new Date(this.areaManager.birthday);

        this.isShowSkeleton = false;
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
    this.reloadService.routingNotReload("/nhan-su/nguoi-quan-ly-khu-vuc", null);
  }

  //data functions

  // Image Function
  getImgFromChild(imgFile) {
    this.avatarFile = imgFile;
    this.changedImg = true;
  }

  changeInputPhoneStyle() {
    this.isChangedPhoneNumber = true;
  }

  saveAreaManager() {
    this.loading = true;
    this.areaManager.email = this.areaManager.email.toLowerCase();
    if (this.isChangedPhoneNumber) {
      if (this.areaManager.phoneNumber.includes(" ")) {
        let phoneArr = this.areaManager.phoneNumber.split(" ");
        this.areaManager.phoneNumber = phoneArr[0] + phoneArr[1] + phoneArr[2];
      }
    }
    this.selectedGender.gender == "Nam"
      ? (this.areaManager.gender = true)
      : (this.areaManager.gender = false);
    this.areaManager.birthday = new Date(
      moment(this.areaManager.birthday).format("YYYY-MM-DDThh:mm:ssZ")
    );
    this.areaManager = new AreaManager(this.areaManager, false);

    if (this.isChangeStatus) {
      if (this.selectedStatus.value === 1) {
        if (this.areaManager.status === 0) {
          this.areaManager.status = 1;
          this.areaManagerServices
            .updateAreaManager(this.areaManager)
            .subscribe((areaManager) => {
              console.log(1)
              this.areaManagerServices
                .resetPassword(areaManager.email)
                .subscribe((res) => {
                  this.mainUpdate();
                });
            });
        } else {
          this.mainUpdate();
        }
      } else {
        this.areaManager.status = 0;
        this.mainUpdate();
      }
    } else {
      this.mainUpdate();
    }
  }

  mainUpdate() {
    this.areaManagerServices
      .updateAreaManager(this.areaManager)
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
          return of(areaManager);
        }),
        finalize(() => {
          this.loading = false;
        })
      )
      .subscribe(
        (areaManagerAfterUpdateImage) => {
          swal.fire({
            title: "Th??nh c??ng!",
            text: "C???p nh???t ng?????i qu???n l?? khu v???c th??nh c??ng.",
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
              text: "C???p nh???t kh??ng th??nh c??ng, vui l??ng th??? l???i.",
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

  //extend
  changeStatus(ev) {
    this.selectedStatus = ev.value;
    this.isChangeStatus = true;
  }
}
