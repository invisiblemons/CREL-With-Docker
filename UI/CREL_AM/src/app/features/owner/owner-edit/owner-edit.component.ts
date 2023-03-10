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
import { Owner } from '../owner.model';
import { OwnerService } from '../owner.service';
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
import { EMAIL_PATTERN } from 'src/app/shared/constants/common.const';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';

@Component({
  selector: 'app-owner-edit',
  templateUrl: './owner-edit.component.html',
  styleUrls: ['./owner-edit.component.scss'],
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

  errorMessage = '';

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
      { label: '???? xo??', value: 0 },
      { label: 'Ho???t ?????ng', value: 1 },
    ];

    this.genders = [{ gender: 'Nam' }, { gender: 'N???' }];

    //editor
    this.editorValueType = 'html';

    //validate
    this.validationMessages = {
      name: {
        required: 'T??n kh??ng ???????c ????? tr???ng.',
        minlength: 'T??n ph???i c?? ??t nh???t 3 k?? t???.',
        maxlength: 'T??n c?? nhi???u nh???t 100 k?? t???.',
      },
      status: {
        required: 'Tr???ng th??i kh??ng ???????c ????? tr???ng.',
      },
      email: {
        required: '?????a ch??? email kh??ng ???????c ????? tr???ng.',
        pattern: '?????a ch??? email kh??ng h???p l???',
      },
      phoneNumber: {
        required: 'S??? ??i???n tho???i kh??ng ???????c ????? tr???ng.',
      },
      gender: {
        required: 'Gi???i t??nh kh??ng ???????c ????? tr???ng.',
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.ownerForm = this.fb.group({
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ],
      ],
      email: ['', [Validators.required, Validators.pattern(EMAIL_PATTERN)]],
      phoneNumber: ['', [Validators.required]],
      gender: ['', [Validators.required]],
      status: ['', [Validators.required]],
    });

    // Load Data
    this.route.queryParams
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((params) => {
          return this.ownerServices.getOwnerById(params['id']);
        })
      )
      .subscribe((owner) => {
        this.owner = owner;
        //Update label status
        this.statuses.forEach((status) => {
          status.value === this.owner.status
            ? (this.selectedStatus = status)
            : '';
        });

        //label for gender
        this.owner.gender == true
          ? (this.selectedGender = { gender: 'Nam' })
          : (this.selectedGender = { gender: 'N???' });

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
    this.loading = true;
    if (this.ownerForm.valid) {
      this.selectedGender.gender == 'Nam'
        ? (this.owner.gender = true)
        : (this.owner.gender = false);

      if (this.owner.phoneNumber.includes(' ')) {
        let phoneArr = this.owner.phoneNumber.split(' ');
        this.owner.phoneNumber = phoneArr[0] + phoneArr[1] + phoneArr[2];
      }

      this.owner.status = this.selectedStatus.value;
      this.owner.email = this.owner.email.toLowerCase();
      this.owner = new Owner(this.owner, false);
      this.ownerServices
        .updateOwner(this.owner)
        .pipe(
          takeUntil(this.destroySubs$),
          finalize(() => (this.loading = false))
        )
        .subscribe(
          (owner: Owner) => {
            //get Owner
            this.owner = owner;
            swal.fire({
              title: 'Th??nh c??ng!',
              text: 'C???p nh???t th??ng tin ng?????i s??? h???u th??nh c??ng.',
              icon: 'success',
              customClass: {
                confirmButton: 'btn btn-success animation-on-hover',
              },
              buttonsStyling: false,
              timer: 2000,
            });
            this.ownerForm.reset();
            this.hideDialog();
          },
          (error) => {
            swal.fire({
              title: 'Th???t b???i!',
              text:
                'C???p nh???t th??ng tin ng?????i s??? h???u th???t b???i v???i l???i ' +
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
        title: 'Th???t b???i!',
        text: 'Vui l??ng s???a l???i trong bi???u m???u',
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

  changeGender(ev) {
    this.selectedGender = ev.value;
  }
}
