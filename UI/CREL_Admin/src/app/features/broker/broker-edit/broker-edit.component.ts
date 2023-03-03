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
import { Broker } from "../broker.model";
import { BrokerService } from "../broker.service";
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
  selector: "app-broker-edit",
  templateUrl: "./broker-edit.component.html",
  styleUrls: ["./broker-edit.component.scss"],
})
export class BrokerEditComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  reasons = [];

  // PhoneNumber
  isChangedPhoneNumber: boolean;

  //gender
  genders: any;

  selectedGender = { gender: "Nam" };

  //date
  maxDate: Date = new Date();
  minDate: Date = new Date();

  statuses: { label: string; value: number }[];

  selectedStatus: { label: string; value: number };

  //broker
  broker: Broker;

  //image
  avatarFile: File;

  changedImg: boolean;

  //editor
  editorValueType: string;

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = "";

  brokerForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  isChangeStatus = false;

  constructor(
    private brokerServices: BrokerService,
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
      { label: "Đã xoá", value: 0 },
      { label: "Hoạt động", value: 1 },
    ];

    this.reasons = [
      "Nhân viên này đã nghỉ làm",
      "Thông tin nhân viên không còn chính xác",
    ];

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
      status: {
        required: "Trạng thái không được để trống.",
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
  }

  ngOnInit(): void {
    this.brokerForm = this.fb.group({
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
          return this.brokerServices.getBrokerById(params["id"]);
        })
      )
      .subscribe((broker) => {
        this.broker = broker;
        if (this.broker.status === 0) {
          this.selectedStatus = this.statuses[0];
        } else if (this.broker.status === 1 || this.broker.status === 2) {
          this.selectedStatus = this.statuses[1];
        }

        //label for gender
        this.broker.gender == true
          ? (this.selectedGender = { gender: "Nam" })
          : (this.selectedGender = { gender: "Nữ" });
        this.broker.birthday = new Date(this.broker.birthday);

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
    merge(this.brokerForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.brokerForm
        );
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingReload("/nhan-su/nguoi-moi-gioi", null);
  }

  cancelDialog() {
    this.reloadService.routingNotReload("/nhan-su/nguoi-moi-gioi", null);
  }

  //data functions
  changeInputPhoneStyle() {
    this.isChangedPhoneNumber = true;
  }

  // Image Function
  getImgFromChild(imgFile) {
    this.avatarFile = imgFile;
    this.changedImg = true;
  }

  saveBroker() {
    if (this.brokerForm.valid) {
      this.loading = true;

      this.broker.email = this.broker.email.toLowerCase();
      if (this.isChangedPhoneNumber) {
        if (this.broker.phoneNumber.includes(" ")) {
          let phoneArr = this.broker.phoneNumber.split(" ");
          this.broker.phoneNumber = phoneArr[0] + phoneArr[1] + phoneArr[2];
        }
      }
      this.selectedGender.gender == "Nam"
        ? (this.broker.gender = true)
        : (this.broker.gender = false);
      this.broker.birthday = new Date(
        moment(this.broker.birthday).format("YYYY-MM-DDThh:mm:ssZ")
      );
      this.broker = new Broker(this.broker, false);
      if (this.isChangeStatus) {
        if (this.selectedStatus.value === 1) {
          if (this.broker.status === 0) {
            this.broker.status = 1;
            this.brokerServices
              .updateBroker(this.broker)
              .subscribe((broker) => {
                this.brokerServices
                  .resetPassword(broker.email)
                  .subscribe((res) => {
                    this.mainUpdate();
                  });
              });
          } else {
            this.mainUpdate();
          }
        } else {
          this.broker.status = 0;
          this.mainUpdate();
        }
      } else {
        this.mainUpdate();
      }
    }
  }

  mainUpdate() {
    this.brokerServices
      .updateBroker(this.broker)
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((broker) => {
          //update image
          if (this.changedImg && this.avatarFile) {
            let formData: FormData = new FormData();
            formData.append("file", this.avatarFile);
            return this.brokerServices.updateBrokerAvatar(broker.id, formData);
          }
          return of(broker);
        }),
        finalize(() => {
          this.loading = false;
        })
      )
      .subscribe(
        (broker: Broker) => {
          this.brokerForm.reset();
          this.hideDialog();
          swal.fire({
            title: "Thành công!",
            text: "Cập nhật người nhân viên môi giới thành công.",
            icon: "success",
            customClass: {
              confirmButton: "btn btn-success animation-on-hover",
            },
            buttonsStyling: false,
            timer: 2000,
          });
        },
        (error) => {
          if (error.error.message === "UserName is already exist") {
            this.brokerForm
              .get("userName")
              .setValidators([StringValidator.invalid()]);
            this.brokerForm.controls["userName"].updateValueAndValidity();
          } else if (error.error.message === "Email is already exist") {
            this.brokerForm
              .get("email")
              .setValidators([StringValidator.invalid()]);
            this.brokerForm.controls["email"].updateValueAndValidity();
          } else if (error.error.message === "PhoneNumber is already exist") {
            this.brokerForm
              .get("phoneNumber")
              .setValidators([StringValidator.invalid()]);
            this.brokerForm.controls["phoneNumber"].updateValueAndValidity();
          } else {
            swal.fire({
              title: "Thất bại!",
              text: "Tạo mới tài khoản thất bại vui lòng thử lại.",
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
  onChangeEmail() {
    this.brokerForm.get("email").clearValidators();
    this.brokerForm
      .get("email")
      .setValidators([Validators.required, Validators.pattern(EMAIL_PATTERN)]);
    this.brokerForm.controls["email"].updateValueAndValidity();
  }
  onChangePhone() {
    this.brokerForm.get("phoneNumber").clearValidators();
    this.brokerForm.get("phoneNumber").setValidators([Validators.required]);
    this.brokerForm.controls["phoneNumber"].updateValueAndValidity();
  }

  //extend
  changeStatus(ev) {
    this.selectedStatus = ev.value;
    this.isChangeStatus = true;
  }
}
