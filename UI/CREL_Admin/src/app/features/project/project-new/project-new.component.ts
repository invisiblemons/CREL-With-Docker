import {
  Component,
  OnInit,
  OnDestroy,
  ElementRef,
  ViewChildren,
  AfterViewInit,
  Output,
  EventEmitter,
  Input,
} from "@angular/core";
import { Router } from "@angular/router";
import { fromEvent, merge, Observable, Subject } from "rxjs";
import { Project } from "../project.model";
import { ProjectService } from "../project.service";
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
import { District } from "src/app/shared/models/district.model";
import { LocationService } from "src/app/shared/services/location.service";
import { Owner } from "../../owner/owner.model";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import moment from "moment";

@Component({
  selector: "app-project-new",
  templateUrl: "./project-new.component.html",
  styleUrls: ["./project-new.component.scss"],
})
export class ProjectNewComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  //project
  project: Project;

  industries: Project[] = [];

  //location
  districts: District[];

  selectedDistrict: District;

  //editor
  editorValueType = "html";

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = "";

  projectForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  // Fields of Input Output
  @Input() isShowProjectDialog: boolean;
  @Output() projectStatusDialog = new EventEmitter<Project>();

  constructor(
    private projectServices: ProjectService,
    private router: Router,
    private fb: FormBuilder,
    private locationService: LocationService,
    private reloadServices: ReloadRouteService
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;

    this.selectedDistrict = null;

    //editor
    this.editorValueType = "html";

    this.project = new Project(null, true);

    //validate
    this.validationMessages = {
      name: {
        required: "Tên không được để trống.",
        minlength: "Tên phải có ít nhất 3 kí tự.",
        maxlength: "Tên có nhiều nhất 100 kí tự.",
      },
      company: {
        required: "Tên chủ đầu tư không được để trống.",
      },
      handoverYear: {
        required: "Năm bàn giao không được để trống.",
      },
      district: {
        required: "Quận/Huyện không được để trống.",
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.projectForm = this.fb.group({
      name: [
        "",
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ],
      ],
      company: ["", [Validators.required]],
      handoverYear: ["", [Validators.required]],
      district: ["", [Validators.required]],
    });

    // Load Data
    this.locationService.getDistricts().subscribe((res) => {
      this.districts = res;
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
    merge(this.projectForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.projectForm
        );
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    if (this.isShowProjectDialog) {
      this.projectStatusDialog.emit(this.project);
      this.isShowDialog = false;
      this.projectForm.reset();
    } else {
      this.reloadServices.routingReload("/bat-dong-san/du-an", null);
    }
  }

  cancelDialog() {
    if (this.isShowProjectDialog) {
      this.projectStatusDialog.emit(this.project);
      this.isShowDialog = false;
      this.projectForm.reset();
    } else {
      this.reloadServices.routingNotReload("/bat-dong-san/du-an", null);
    }
  }

  saveProject() {
    this.loading = true;
    this.project.districtId = this.selectedDistrict.id;
    this.project.handoverYear = new Date(
      moment(this.project.handoverYear).format("YYYY-MM-DDThh:mm:ssZ")
    );
    this.project = new Project(this.project, true);
    this.projectServices
      .insertProject(this.project)
      .pipe(
        takeUntil(this.destroySubs$),
        finalize(() => (this.loading = false))
      )
      .subscribe(
        (project) => {
          //get project
          this.project = project;
          swal.fire({
            title: "Thành công!",
            text: "Tạo mới dự án bất động sản thương mại thành công.",
            icon: "success",
            customClass: {
              confirmButton: "btn btn-success animation-on-hover",
            },
            buttonsStyling: false,
            timer: 2000,
          });
          this.projectForm.reset();
          
          if (this.isShowProjectDialog) {
            this.projectStatusDialog.emit(this.project);
            this.isShowDialog = false;
            this.projectForm.reset();
          } else {
            this.reloadServices.routingReload("/bat-dong-san/du-an", null);
          }

        },
        (error) => {
          swal.fire({
            title: "Thất bại!",
            text:
              "Tạo mới dự án bất động sản thương mại thất bại với lỗi " +
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
  }
}
