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
import { Appointment } from "../appointment.model";
import { AppointmentService } from "../appointment.service";
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
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";

@Component({
  selector: 'app-appointment-new',
  templateUrl: './appointment-new.component.html',
  styleUrls: ['./appointment-new.component.scss']
})
export class AppointmentNewComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  //appointment
  appointment: Appointment;

  appointments: Appointment[] = [];

  //editor
  editorValueType = "html";

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = "";

  appointmentForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  constructor(
    private appointmentServices: AppointmentService,
    private router: Router,
    private fb: FormBuilder,
    private reloadService: ReloadRouteService,
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;

    //editor
    this.editorValueType = "html";

    this.appointment = new Appointment(null, true);

    //validate
    this.validationMessages = {
      name: {
        required: "T??n kh??ng ???????c ????? tr???ng.",
        minlength: "T??n ph???i c?? ??t nh???t 3 k?? t???.",
        maxlength: "T??n c?? nhi???u nh???t 100 k?? t???.",
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
    });

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
      this.appointment = new Appointment(this.appointment, true);
      this.appointmentServices
        .insertAppointment(this.appointment)
        .pipe(
          takeUntil(this.destroySubs$),
          finalize(() => (this.loading = false))
        )
        .subscribe(
          (appointment) => {
            //get appointment
            this.appointment = appointment;
            swal.fire({
              title: "Th??nh c??ng!",
              text: "T???o m???i cu???c h???n th??nh c??ng.",
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
              title: "Th???t b???i!",
              text:
                "T???o m???i cu???c h???n th???t b???i v???i l???i " +
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
        title: "Th???t b???i!",
        text: "Vui l??ng s???a l???i trong bi???u m???u",
        icon: "error",
        customClass: {
          confirmButton: "btn btn-info animation-on-hover",
        },
        buttonsStyling: false,
        timer: 2000,
      });
    }
  }
}

