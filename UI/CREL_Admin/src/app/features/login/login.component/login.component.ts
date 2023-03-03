import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  ViewChildren,
} from "@angular/core";
import { Router } from "@angular/router";
import { MessageService } from "primeng/api";
import { AuthService } from "../auth.service";
import { LocalStorageService } from "../local-storage.service";
import { Account } from "../user.model";
import swal from "sweetalert2";
import {
  FormBuilder,
  FormControlName,
  FormGroup,
  Validators,
} from "@angular/forms";
import { GenericValidator } from "src/app/shared/validator/generic-validator";
import { fromEvent, merge, Observable } from "rxjs";
import { debounceTime } from "rxjs/operators";

@Component({
  selector: "app-login",
  templateUrl: "login.component.html",
  styleUrls: ["login.component.scss"],
})
export class LoginComponent implements OnInit, AfterViewInit {
  userName: string;

  password: string;

  account: Account;

  loading: boolean;

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = "";

  loginForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  constructor(
    private authService: AuthService,
    private messageService: MessageService,
    private localStorageService: LocalStorageService,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.account = new Account(null);
    this.loading = false;

    //validate
    this.validationMessages = {
      userName: {
        required: "Tài khoản không được để trống.",
        minlength: "Tài khoản phải có ít nhất 3 kí tự.",
        maxlength: "Tài khoản có nhiều nhất 100 kí tự.",
      },
      password: {
        required: "Mật khẩu không được để trống.",
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
    this.loginForm = this.fb.group({
      userName: [
        "",
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ],
      ],
      password: ["", [Validators.required]],
    });
  }

  ngOnInit() {
    var body = document.getElementsByTagName("body")[0];
    body.classList.add("login-page");
  }

  ngAfterViewInit(): void {
    // Watch for the blur event from any input element on the form.
    // This is required because the valueChanges does not provide notification on blur
    const controlBlurs: Observable<any>[] = this.formInputElements.map(
      (formControl: ElementRef) => fromEvent(formControl.nativeElement, "blur")
    );

    // Merge the blur event observable with the valueChanges observable
    // so we only need to subscribe once.
    merge(this.loginForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.loginForm
        );
      });
  }

  // Login user with  provided Email/ Password
  loginUser() {
    this.loading = true;
    this.account.userName = this.userName;
    this.account.password = this.password;
    this.authService.login(this.account).subscribe(
      (res) => {
        this.loading = false;
        this.localStorageService.setUser(res);
        swal.fire({
          title: "Thành công!",
          text: "Đăng nhập thành công.",
          icon: "success",
          customClass: {
            confirmButton: "btn btn-success animation-on-hover",
          },
          buttonsStyling: false,
          timer: 2000,
        });
        this.router.navigate(["/tong-quan"]);
      },
      (err) => {
        this.loading = false;
        swal.fire({
          title: "Thất bại!",
          text: "Tài khoản hoặc mật khẩu không chính xác.",
          icon: "error",
          customClass: {
            confirmButton: "btn btn-danger animation-on-hover",
          },
          buttonsStyling: false,
          timer: 2000,
        });
      }
    );
  }
}
