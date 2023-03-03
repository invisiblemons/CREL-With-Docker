import {
  Component,
  OnInit,
  OnDestroy,
  ElementRef,
  ViewChildren,
  AfterViewInit,
  ChangeDetectorRef,
} from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { fromEvent, merge, Observable, Subject } from "rxjs";
import { Team } from "../team.model";
import { TeamService } from "../team.service";
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
import { AreaManager } from "../../area-manager/area-manager.model";
import CustomStore from "devextreme/data/custom_store";
import { AreaManagerService } from "../../area-manager/area-manager.service";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";

@Component({
  selector: "app-team-new",
  templateUrl: "./team-new.component.html",
  styleUrls: ["./team-new.component.scss"],
})
export class TeamNewComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  //areaManager
  areaManagers: AreaManager[];

  selectedAreaManager: AreaManager;

  areaManagerGridDataSource: any;

  areaManagerGridBoxValue: number[];

  isAreaManagerGridBoxOpened: boolean = false;

  areaManagerGridColumns: any = [
    {
      caption: "Tên",
      dataField: "name",
      dataType: "string",
    },
    {
      caption: "Số điện thoại",
      dataField: "phoneNumber",
      dataType: "string",
    },
  ];

  //team
  team: Team;

  teams: Team[] = [];

  //editor
  editorValueType = "html";

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = "";

  teamForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  constructor(
    private teamServices: TeamService,
    private router: Router,
    private fb: FormBuilder,
    private ref: ChangeDetectorRef,
    private areaManagerServices: AreaManagerService,
    private reloadService: ReloadRouteService,
    private route: ActivatedRoute
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;

    //editor
    this.editorValueType = "html";

    this.team = new Team(null, true);

    //validate
    this.validationMessages = {
      name: {
        required: "Tên không được để trống.",
        minlength: "Tên phải có ít nhất 3 kí tự.",
        maxlength: "Tên có nhiều nhất 100 kí tự.",
      },
      areaManager: {
        required: "Người quản lý không được để trống.",
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.teamForm = this.fb.group({
      name: [
        "",
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ],
      ],
      areaManager: ["", [Validators.required]],
    });

    // Load Data
    this.getAreaManagers();

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
    merge(this.teamForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.teamForm
        );
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingReload("/nhan-su/nhom", null);
  }

  cancelDialog() {
    this.reloadService.routingNotReload("/nhan-su/nhom", null);
  }

  //data functions
  getAreaManagers() {
    this.areaManagerServices.getAreaManagers().subscribe((res) => {
      if (null !== res) {
        this.areaManagers = res;
        //gridbox devex
        this.areaManagerGridDataSource = this.makeAsyncDataSource(
          this.areaManagers
        );
        this.route.queryParams.subscribe((params) => {
          if (params.id) {
            this.areaManagers.forEach((am) => {
              if (am.id.toString() === params.id) {
                this.selectedAreaManager = am;
                this.areaManagerGridBoxValue = [this.selectedAreaManager.id];
                this.teamForm.controls.areaManager.setValue(
                  this.areaManagerGridBoxValue
                );
              }
            });
          }
        });
      }
    });
  }

  //dev-extreme
  makeAsyncDataSource(items) {
    return new CustomStore({
      loadMode: "raw",
      key: "id",
      load() {
        return items;
      },
    });
  }

  areaManagerGridBox_displayExpr(item) {
    return item && `${item.name} <${item.phoneNumber}>`;
  }

  onAreaManagerGridBoxOptionChanged(e) {
    if (e.name === "value" && e.value) {
      this.isAreaManagerGridBoxOpened = false;
      this.selectedAreaManager = this.areaManagers.filter(
        (res) => res.id === e.value[0]
      )[0];
      if (!e.previousValue) {
        this.ref.detectChanges();
      }
    }
  }

  saveTeam() {
    this.loading = true;
    this.team.status = 1;
    this.team = new Team(this.team, true);
    this.teamServices
      .insertTeam(this.team)
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((team) => {
          return this.teamServices.insertAreaManagerIntoTeam(
            [this.selectedAreaManager.id],
            team.id
          );
        }),
        finalize(() => (this.loading = false))
      )
      .subscribe(
        (teamAfterAdding) => {
          //get team
          this.team.areaManagerTeams = teamAfterAdding;
          swal.fire({
            title: "Thành công!",
            text: "Tạo mới nhóm thành công.",
            icon: "success",
            customClass: {
              confirmButton: "btn btn-success animation-on-hover",
            },
            buttonsStyling: false,
            timer: 2000,
          });
          this.teamForm.reset();
          this.reloadService.routingReload("/nhan-su/nhom", null);
        },
        (error) => {
          swal.fire({
            title: "Thất bại!",
            text: "Tạo mới nhóm thất bại với lỗi " + error.error.title,
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
