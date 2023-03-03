import {
  Component,
  OnInit,
  OnDestroy,
  ViewChildren,
  ElementRef,
  AfterViewInit,
} from "@angular/core";
import { fromEvent, merge, Observable, Subject } from "rxjs";
import { ActivatedRoute, Router } from "@angular/router";
import { Owner } from "../owner.model";
import { OwnerService } from "../owner.service";
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
import { EMAIL_PATTERN } from "src/app/shared/constants/common.const";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";

@Component({
  selector: "app-owner-edit",
  templateUrl: "./owner-edit.component.html",
  styleUrls: ["./owner-edit.component.scss"],
})
export class OwnerEditComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  reasons: string[];

  statuses: { label: string; value: number }[];

  selectedStatus: { label: string; value: number };

  genders: any;

  selectedGender = { gender: "Nam" };

  //owner
  owner: Owner;

  selectedOwner: Owner;

  //editor
  editorValueType: string;

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  ownerForm!: FormGroup;

  errorMessage = "";

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  constructor(
    private ownerServices: OwnerService,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private reloadService: ReloadRouteService,
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;
    this.statuses = [
      { label: "Đã xoá", value: 0 },
      { label: "Hoạt động", value: 1 },
    ];

    this.genders = [{ gender: "Nam" }, { gender: "Nữ" }];

    //editor
    this.editorValueType = "html";

    //validate
    this.validationMessages = {
      name: {
        required: "Tên không được để trống.",
        minlength: "Tên phải có ít nhất 3 kí tự.",
        maxlength: "Tên có nhiều nhất 100 kí tự.",
      },
      status: {
        required: "Trạng thái không được để trống.",
      },
      email: {
        required: "Địa chỉ email không được để trống.",
        pattern: "Địa chỉ email không hợp lệ",
      },
      phoneNumber: {
        required: "Số điện thoại không được để trống.",
      },
      gender: {
        required: "Giới tính không được để trống.",
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.ownerForm = this.fb.group({
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
      status: ["", [Validators.required]],
    });

    // Load Data
    this.route.queryParams
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((params) => {
          return this.ownerServices.getOwnerById(params["id"]);
        })
      )
      .subscribe((owner) => {
        this.owner = owner;
        //Update label status
        this.statuses.forEach((status) => {
          status.value === this.owner.status
            ? (this.selectedStatus = status)
            : "";
        });

        //label for gender
        this.owner.gender == true
          ? (this.selectedGender = { gender: "Nam" })
          : (this.selectedGender = { gender: "Nữ" });

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
    merge(this.ownerForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.ownerForm
        );
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingReload('/bat-dong-san/nguoi-so-huu', null);
  }

  cancelDialog() {
    this.reloadService.routingNotReload('/bat-dong-san/nguoi-so-huu', null);
  }

  saveOwner() {
    if (this.ownerForm.valid) {
      this.loading = true;

      if (this.owner.phoneNumber.includes(" ")) {
        let phoneArr = this.owner.phoneNumber.split(" ");
        this.owner.phoneNumber = phoneArr[0] + phoneArr[1] + phoneArr[2];
      }

      this.selectedGender.gender == "Nam"
        ? (this.owner.gender = true)
        : (this.owner.gender = false);

      this.owner.status = this.selectedStatus.value;
      this.owner.email = this.owner.email.toLowerCase();
      this.owner = new Owner(this.owner, false);
      this.ownerServices.updateOwner(this.owner)
        .pipe(
          takeUntil(this.destroySubs$),
          finalize(() => (this.loading = false))
        )
        .subscribe(
          (owner: Owner) => {
            //get Owner
            this.owner = owner;
            swal.fire({
              title: "Thành công!",
              text: "Cập nhật thông tin người sở hữu thành công.",
              icon: "success",
              customClass: {
                confirmButton: "btn btn-success animation-on-hover",
              },
              buttonsStyling: false,
              timer: 2000,
            });
            this.ownerForm.reset();
            this.hideDialog();
          },
          (error) => {
            swal.fire({
              title: "Thất bại!",
              text:
                "Cập nhật thông tin người sở hữu thất bại với lỗi " +
                error.error.title,
              icon: "error",
              customClass: {
                confirmButton: "btn btn-info animation-on-hover",
              },
              buttonsStyling: false,
              timer: 2000,
            });
          }
        );
    } else {
      swal.fire({
        title: "Thất bại!",
        text: "Vui lòng sửa lỗi trong biểu mẫu",
        icon: "error",
        customClass: {
          confirmButton: "btn btn-info animation-on-hover",
        },
        buttonsStyling: false,
        timer: 2000,
      });
    }
  }

  //extend
  changeStatus(ev) {
    this.selectedStatus = ev.value;
  }

  changeGender(ev) {
    this.selectedGender = ev.value;
  }
}
