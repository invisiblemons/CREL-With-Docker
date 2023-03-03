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
import { Industry } from "../industry.model";
import { IndustryService } from "../industry.service";
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
  selector: "app-industry-new",
  templateUrl: "./industry-new.component.html",
  styleUrls: ["./industry-new.component.scss"],
})
export class IndustryNewComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  //industry
  industry: Industry;

  industries: Industry[] = [];

  //editor
  editorValueType = "html";

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = "";

  industryForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  constructor(
    private industryServices: IndustryService,
    private router: Router,
    private fb: FormBuilder,
    private reloadService: ReloadRouteService,
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;

    //editor
    this.editorValueType = "html";

    this.industry = new Industry(null, true);

    //validate
    this.validationMessages = {
      name: {
        required: "Tên không được để trống.",
        minlength: "Tên phải có ít nhất 3 kí tự.",
        maxlength: "Tên có nhiều nhất 100 kí tự.",
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
      this.industry = new Industry(this.industry, true);
      this.industryServices
        .insertIndustry(this.industry)
        .pipe(
          takeUntil(this.destroySubs$),
          finalize(() => (this.loading = false))
        )
        .subscribe(
          (industry) => {
            //get industry
            this.industry = industry;
            swal.fire({
              title: "Thành công!",
              text: "Tạo mới ngành kinh doanh thành công.",
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
              title: "Thất bại!",
              text:
                "Tạo mới ngành kinh doanh thất bại với lỗi " +
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
}
