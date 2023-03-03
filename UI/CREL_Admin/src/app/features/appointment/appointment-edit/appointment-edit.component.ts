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
import { Appointment } from "../appointment.model";
import { AppointmentService } from "../appointment.service";
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
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";

@Component({
  selector: 'app-appointment-edit',
  templateUrl: './appointment-edit.component.html',
  styleUrls: ['./appointment-edit.component.scss']
})
export class AppointmentEditComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  reasons: string[];

  statuses: { label: string; value: number }[];

  selectedStatus: { label: string; value: number };

  //appointment
  appointment: Appointment;

  selectedAppointment: Appointment;

  //editor
  editorValueType: string;

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  appointmentForm!: FormGroup;

  errorMessage = "";

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  constructor(
    private appointmentServices: AppointmentService,
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

    this.reasons = [
      "Lỗi nội dung sai sự thật",
      "Lỗi hình ảnh không liên quan",
      "Lỗi dùng từ ngữ thô tục",
      "Lỗi nội dung chứa thông tin phân biệt chủng tộc, vùng miền",
    ];

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
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.appointmentForm = this.fb.group({
      name: [
        "",
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ],
      ],
      status: ["", [Validators.required]],
    });

    // Load Data
    this.route.queryParams
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((params) => {
          return this.appointmentServices.getAppointmentById(params["id"]);
        })
      )
      .subscribe((appointment) => {
        this.appointment = appointment;
        this.statuses.forEach((status) => {
          status.value === this.appointment.status
            ? (this.selectedStatus = status)
            : "";
        });

        // set value for fields form

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
    merge(this.appointmentForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.appointmentForm
        );
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingReload('/thuong-hieu/cuoc-hen', null);
  }

  cancelDialog() {
    this.reloadService.routingNotReload('/thuong-hieu/cuoc-hen', null);
  }

  saveAppointment() {
    if (this.appointmentForm.valid) {
      this.loading = true;
      this.appointment.status = this.selectedStatus.value;
      this.appointment = new Appointment(this.appointment, false);
      this.appointmentServices
        .updateAppointment(this.appointment)
        .pipe(
          takeUntil(this.destroySubs$),
          finalize(() => (this.loading = false))
        )
        .subscribe(
          (appointment: Appointment) => {
            //get Appointment
            this.appointment = appointment;

            swal.fire({
              title: "Thành công!",
              text: "Cập nhật cuộc hẹn thành công.",
              icon: "success",
              customClass: {
                confirmButton: "btn btn-success animation-on-hover",
              },
              buttonsStyling: false,
              timer: 2000,
            });
            this.appointmentForm.reset();
            this.hideDialog();
          },
          (error) => {
            swal.fire({
              title: "Thất bại!",
              text:
                "Cập nhật cuộc hẹn thất bại với lỗi " +
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
}
