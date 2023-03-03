import {
  Component,
  OnInit,
  OnDestroy,
  ElementRef,
  ViewChildren,
  AfterViewInit,
  Input,
  Output,
  EventEmitter,
} from "@angular/core";
import { Router } from "@angular/router";
import { fromEvent, merge, Observable, of, Subject } from "rxjs";
import { Broker } from "../broker.model";
import { BrokerService } from "../broker.service";
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
  selector: "app-broker-new",
  templateUrl: "./broker-new.component.html",
  styleUrls: ["./broker-new.component.scss"],
})
export class BrokerNewComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  //broker
  broker: Broker;

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

  brokerForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  // Fields of Input Output
  @Input() isShowBrokerDialog: boolean;
  @Output() brokerStatusDialog = new EventEmitter<Broker>();

  constructor(
    private brokerServices: BrokerService,
    private router: Router,
    private fb: FormBuilder,
    private teamServices: TeamService,
    public primengConfig: PrimeNGConfig,
    private reloadServices: ReloadRouteService,
    private reloadService: ReloadRouteService
  ) {
    // Config VI for calendar
    this.primengConfig.setTranslation(data.primeng);

    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;

    this.broker = new Broker(null, false);

    this.maxDate = new Date(
      this.maxDate.setFullYear(this.maxDate.getFullYear() - 18)
    );
    this.minDate = new Date(
      this.minDate.setFullYear(this.minDate.getFullYear() - 100)
    );

    this.genders = [{ gender: "Nam" }, { gender: "Nữ" }];

    //image
    this.changedImg = false;

    //editor
    this.editorValueType = "html";

    //validate
    this.validationMessages = {
      name: {
        required: "Tên không được để trống.",
        minlength: "Tên phải có ít nhất 3 kí tự.",
        maxlength: "Tên có nhiều nhất 100 kí tự.",
      },
      phoneNumber: {
        required: "Số điện thoại không được để trống.",
        invalid: 'Số điện thoại đã tồn tại',
      },
      email: {
        required: "Địa chỉ email không được để trống.",
        pattern: "Địa chỉ email không hợp lệ",
        invalid: 'Địa chỉ email đã tồn tại',
      },
      gender: {
        required: "Giới tính không được để trống.",
      },
      userName: {
        required: "Tài khoản không được để trống.",
        minlength: "Tài khoản phải có ít nhất 3 kí tự.",
        maxlength: "Tài khoản có nhiều nhất 100 kí tự.",
        invalid: 'Tài khoản đã tồn tại',
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
    this.broker.birthday = new Date(this.maxDate);
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
    if (this.isShowBrokerDialog) {
      this.brokerStatusDialog.emit(this.broker);
      this.isShowDialog = false;
      this.brokerForm.reset();
    } else {
      this.reloadServices.routingReload("/nhan-su/nguoi-moi-gioi", null);
    }
  }
  cancelDialog() {
    if (this.isShowBrokerDialog) {
      this.brokerStatusDialog.emit(this.broker);
      this.isShowDialog = false;
      this.brokerForm.reset();
    } else {
      this.reloadServices.routingNotReload("/nhan-su/nguoi-moi-gioi", null);
    }
  }

  //data functions

  // Image Function
  getImgFromChild(imgFile) {
    this.avatarFile = imgFile;
    this.changedImg = true;
  }

  saveBroker() {
    this.loading = true;
    this.broker.email = this.broker.email.toLowerCase();
    if (this.broker.phoneNumber.includes(" ")) {
      let phoneArr = this.broker.phoneNumber.split(" ");
      this.broker.phoneNumber = phoneArr[0] + phoneArr[1] + phoneArr[2];
    }
    this.selectedGender.gender == "Nam"
      ? (this.broker.gender = true)
      : (this.broker.gender = false);
    this.broker.birthday = new Date(
      moment(this.broker.birthday).format("YYYY-MM-DDThh:mm:ssZ")
    );
    this.broker.status = 1;
    this.broker = new Broker(this.broker, true);
    this.brokerServices
      .insertBroker(this.broker)
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
        (brokerAfterUpdateImage) => {

          this.brokerForm.reset();
          this.hideDialog();
          swal.fire({
            title: "Thành công!",
            text: "Tạo mới Nhân viên môi giới thành công.",
            icon: "success",
            customClass: {
              confirmButton: "btn btn-success animation-on-hover",
            },
            buttonsStyling: false,
            timer: 2000,
          });
        },
        (error) => {
          if (error.error.message === 'UserName is already exist') {
            this.brokerForm
              .get('userName')
              .setValidators([StringValidator.invalid()]);
            this.brokerForm.controls['userName'].updateValueAndValidity();
          } else if (error.error.message === 'Email is already exist') {
            this.brokerForm
              .get('email')
              .setValidators([StringValidator.invalid()]);
            this.brokerForm.controls['email'].updateValueAndValidity();
          } else if (error.error.message === 'PhoneNumber is already exist') {
            this.brokerForm
              .get('phoneNumber')
              .setValidators([StringValidator.invalid()]);
            this.brokerForm.controls['phoneNumber'].updateValueAndValidity();
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

  onChangeUserName() {
    this.brokerForm.get('userName').clearValidators();
    this.brokerForm
      .get('userName')
      .setValidators([
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(100),
      ]);
    this.brokerForm.controls['userName'].updateValueAndValidity();
  }
  onChangeEmail() {
    this.brokerForm.get('email').clearValidators();
    this.brokerForm
      .get('email')
      .setValidators([Validators.required, Validators.pattern(EMAIL_PATTERN)]);
    this.brokerForm.controls['email'].updateValueAndValidity();
  }
  onChangePhone() {
    this.brokerForm.get('phoneNumber').clearValidators();
    this.brokerForm.get('phoneNumber').setValidators([Validators.required]);
    this.brokerForm.controls['phoneNumber'].updateValueAndValidity();
  }
}
