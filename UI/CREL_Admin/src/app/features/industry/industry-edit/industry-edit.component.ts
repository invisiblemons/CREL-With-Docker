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
import { Industry } from "../industry.model";
import { IndustryService } from "../industry.service";
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
  selector: "app-industry-edit",
  templateUrl: "./industry-edit.component.html",
  styleUrls: ["./industry-edit.component.scss"],
})
export class IndustryEditComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  reasons: string[];

  statuses: { label: string; value: number }[];

  selectedStatus: { label: string; value: number };

  //industry
  industry: Industry;

  selectedIndustry: Industry;

  //editor
  editorValueType: string;

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  industryForm!: FormGroup;

  errorMessage = "";

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  constructor(
    private industryServices: IndustryService,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private reloadService: ReloadRouteService,
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;
    this.statuses = [
      { label: "???? xo??", value: 0 },
      { label: "Ho???t ?????ng", value: 1 },
    ];

    this.reasons = [
      "L???i n???i dung sai s??? th???t",
      "L???i h??nh ???nh kh??ng li??n quan",
      "L???i d??ng t??? ng??? th?? t???c",
      "L???i n???i dung ch???a th??ng tin ph??n bi???t ch???ng t???c, v??ng mi???n",
    ];

    //editor
    this.editorValueType = "html";

    //validate
    this.validationMessages = {
      name: {
        required: "T??n kh??ng ???????c ????? tr???ng.",
        minlength: "T??n ph???i c?? ??t nh???t 3 k?? t???.",
        maxlength: "T??n c?? nhi???u nh???t 100 k?? t???.",
      },
      status: {
        required: "Tr???ng th??i kh??ng ???????c ????? tr???ng.",
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.industryForm = this.fb.group({
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
          return this.industryServices.getIndustryById(params["id"]);
        })
      )
      .subscribe((industry) => {
        this.industry = industry;
        this.statuses.forEach((status) => {
          status.value === this.industry.status
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
    merge(this.industryForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.industryForm
        );
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingReload('/cau-hinh/nganh', null);
  }

  cancelDialog() {
    this.reloadService.routingNotReload('/cau-hinh/nganh', null);
  }

  saveIndustry() {
    if (this.industryForm.valid) {
      this.loading = true;
      this.industry.status = this.selectedStatus.value;
      this.industry = new Industry(this.industry, false);
      this.industryServices
        .updateIndustry(this.industry)
        .pipe(
          takeUntil(this.destroySubs$),
          finalize(() => (this.loading = false))
        )
        .subscribe(
          (industry: Industry) => {
            //get Industry
            this.industry = industry;

            swal.fire({
              title: "Th??nh c??ng!",
              text: "C???p nh???t ng??nh kinh doanh th??nh c??ng.",
              icon: "success",
              customClass: {
                confirmButton: "btn btn-success animation-on-hover",
              },
              buttonsStyling: false,
              timer: 2000,
            });
            this.industryForm.reset();
            this.hideDialog();
          },
          (error) => {
            swal.fire({
              title: "Th???t b???i!",
              text:
                "C???p nh???t ng??nh kinh doanh th???t b???i v???i l???i " +
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

  //extend
  changeStatus(ev) {
    this.selectedStatus = ev.value;
  }
}
