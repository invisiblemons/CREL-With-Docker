import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  ViewChildren,
} from "@angular/core";
import {
  FormBuilder,
  FormControlName,
  FormGroup,
  Validators,
} from "@angular/forms";
import { fromEvent, merge, Observable } from "rxjs";
import { debounceTime } from "rxjs/operators";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { GenericValidator } from "src/app/shared/validator/generic-validator";
import swal from 'sweetalert2';
import { EMAIL_PATTERN } from "src/app/shared/constants/common.const";
import { AreaManagerService } from "../area-manager/area-manager.service";

@Component({
  selector: "app-reset-password",
  templateUrl: "./reset-password.component.html",
  styleUrls: ["./reset-password.component.scss"],
})
export class ResetPasswordComponent implements OnInit, AfterViewInit {
  email: string;

  loading: boolean;

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = "";

  resetForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  constructor(private fb: FormBuilder, private areaManagerService: AreaManagerService, private reloadService: ReloadRouteService) {
    this.loading = false;

    //validate
    this.validationMessages = {
      email: {
        required: "Địa chỉ email không được để trống.",
        pattern: "Địa chỉ email không hợp lệ",
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
    this.resetForm = this.fb.group({
      email: ["", [Validators.required, Validators.pattern(EMAIL_PATTERN)]],
    });
  }

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    // Watch for the blur event from any input element on the form.
    // This is required because the valueChanges does not provide notification on blur
    const controlBlurs: Observable<any>[] = this.formInputElements.map(
      (formControl: ElementRef) => fromEvent(formControl.nativeElement, "blur")
    );

    // Merge the blur event observable with the valueChanges observable
    // so we only need to subscribe once.
    merge(this.resetForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.resetForm
        );
      });
  }

  resetPassword() {
    this.loading = true;
    this.areaManagerService.resetPassword(this.email).subscribe((res) => {
      this.loading = false;
      this.reloadService.routingReload('/dang-nhap', null);
      swal.fire({
        title: 'Thành công!',
        text: 'Đổi mật khẩu thành công. Kiểm tra địa chỉ email để lấy mật khẩu mới.',
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
        title: "Thất bại!",
        text: "Địa chỉ email không tồn tại.",
        icon: "error",
        customClass: {
          confirmButton: "btn btn-danger animation-on-hover",
        },
        buttonsStyling: false,
        timer: 2000,
      });
    });
  }

  routingToLogin() {
    this.reloadService.routingReload('/dang-nhap', null);
  }
}
