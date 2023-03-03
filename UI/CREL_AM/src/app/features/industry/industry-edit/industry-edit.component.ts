import {
  Component,
  OnInit,
  OnDestroy,
  ViewChildren,
  ElementRef,
  AfterViewInit,
} from '@angular/core';
import { fromEvent, merge, Observable, Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { Industry } from '../industry.model';
import { debounceTime, finalize, switchMap, takeUntil } from 'rxjs/operators';
import swal from 'sweetalert2';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormControlName,
} from '@angular/forms';
import { GenericValidator } from 'src/app/shared/validator/generic-validator';
import 'devextreme/ui/html_editor/converters/markdown';
import { IndustryService } from '../industry.service';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';

@Component({
  selector: 'app-industry-edit',
  templateUrl: './industry-edit.component.html',
  styleUrls: ['./industry-edit.component.scss'],
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

  errorMessage = '';

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  constructor(
    private industryServices: IndustryService,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private reloadService: ReloadRouteService
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;
    this.statuses = [
      { label: 'Đã xoá', value: 0 },
      { label: 'Hoạt động', value: 1 },
    ];

    this.reasons = [
      'Lỗi nội dung sai sự thật',
      'Lỗi hình ảnh không liên quan',
      'Lỗi dùng từ ngữ thô tục',
      'Lỗi nội dung chứa thông tin phân biệt chủng tộc, vùng miền',
    ];

    //editor
    this.editorValueType = 'html';

    //validate
    this.validationMessages = {
      name: {
        required: 'Tên không được để trống.',
        minlength: 'Tên phải có ít nhất 3 kí tự.',
        maxlength: 'Tên có nhiều nhất 100 kí tự.',
      },
      status: {
        required: 'Trạng thái không được để trống.',
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.industryForm = this.fb.group({
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ],
      ],
      status: ['', [Validators.required]],
    });

    // Load Data
    this.route.queryParams
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((params) => {
          return this.industryServices.getIndustryById(params['id']);
        })
      )
      .subscribe((industry) => {
        this.industry = industry;
        this.statuses.forEach((status) => {
          status.value === this.industry.status
            ? (this.selectedStatus = status)
            : '';
        });
        this.isShowSkeleton = false;
      });
  }

  ngAfterViewInit(): void {
    // Watch for the blur event from any input element on the form.
    // This is required because the valueChanges does not provide notification on blur
    const controlBlurs: Observable<any>[] = this.formInputElements.map(
      (formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur')
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
    this.reloadService.routingReload('/thuong-hieu/nganh', null);
  }

  cancelDialog() {
    this.reloadService.routingNotReload('/thuong-hieu/nganh', null);
  }

  saveIndustry() {
    this.loading = true;
    if (this.industryForm.valid) {
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
              title: 'Thành công!',
              text: 'Cập nhật ngành kinh doanh thành công.',
              icon: 'success',
              customClass: {
                confirmButton: 'btn btn-success animation-on-hover',
              },
              buttonsStyling: false,
              timer: 2000,
            });
            this.industryForm.reset();
            this.hideDialog();
          },
          (error) => {
            swal.fire({
              title: 'Thất bại!',
              text:
                'Cập nhật ngành kinh doanh thất bại với lỗi ' +
                error.error.title,
              icon: 'error',
              customClass: {
                confirmButton: 'btn btn-info animation-on-hover',
              },
              buttonsStyling: false,
              timer: 2000,
            });
          }
        );
    } else {
      swal.fire({
        title: 'Thất bại!',
        text: 'Vui lòng sửa lỗi trong biểu mẫu',
        icon: 'error',
        customClass: {
          confirmButton: 'btn btn-info animation-on-hover',
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
