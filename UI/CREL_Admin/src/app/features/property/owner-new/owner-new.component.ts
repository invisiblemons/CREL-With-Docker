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
import { fromEvent, merge, Observable, Subject } from "rxjs";
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
import { EMAIL_PATTERN } from "src/app/shared/constants/common.const";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { Owner } from "../../owner/owner.model";
import { OwnerService } from "../../owner/owner.service";

@Component({
  selector: "app-owner-new-in-property",
  templateUrl: "./owner-new.component.html",
  styleUrls: ["./owner-new.component.scss"],
})
export class OwnerNewInPropertyComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  genders: any;

  selectedGender = { gender: "Nam" };

  //owner
  owner: Owner;

  owners: Owner[] = [];

  //editor
  editorValueType = "html";

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = "";

  ownerForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  // Fields of Input Output
  @Input() isShowOwnerDialog: boolean;
  @Output() ownerStatusDialog = new EventEmitter<Owner>();

  constructor(
    private ownerServices: OwnerService,
    private router: Router,
    private fb: FormBuilder,
    private reloadServices: ReloadRouteService
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;

    //editor
    this.editorValueType = "html";

    this.owner = new Owner(null, true);

    this.genders = [{ gender: "Nam" }, { gender: "Nữ" }];

    this.selectedGender = { gender: 'Nam' };

    //validate
    this.validationMessages = {
      name: {
        required: "Tên không được để trống.",
        minlength: "Tên phải có ít nhất 3 kí tự.",
        maxlength: "Tên có nhiều nhất 100 kí tự.",
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
    if(this.isShowOwnerDialog) {
      this.ownerStatusDialog.emit(this.owner);
      this.ownerForm.reset();
      this.owner = new Owner(null, true);
    }
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
      this.owner.email = this.owner.email.toLowerCase();
      this.owner = new Owner(this.owner, true);
      this.ownerServices.insertOwner(this.owner)
        .pipe(
          takeUntil(this.destroySubs$),
          finalize(() => (this.loading = false))
        )
        .subscribe(
          (owner) => {
            //get owner
            this.owner = owner;
            this.hideDialog();
          },
        );
    }
  }

  changeGender(ev) {
    this.selectedGender = ev.value;
  }
}
