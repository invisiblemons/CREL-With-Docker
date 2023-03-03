import {
  Component,
  OnInit,
  OnDestroy,
  ViewChildren,
  ElementRef,
  AfterViewInit,
  ChangeDetectorRef,
} from "@angular/core";
import { fromEvent, merge, Observable, of, Subject } from "rxjs";
import { ActivatedRoute, Router } from "@angular/router";
import { AreaManagerTeam, Team } from "../team.model";
import { TeamService } from "../team.service";
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
import { AreaManager } from "../../area-manager/area-manager.model";
import { AreaManagerService } from "../../area-manager/area-manager.service";
import CustomStore from "devextreme/data/custom_store";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";

@Component({
  selector: "app-team-edit",
  templateUrl: "./team-edit.component.html",
  styleUrls: ["./team-edit.component.scss"],
})
export class TeamEditComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  statuses: { label: string; value: number }[];

  selectedStatus: { label: string; value: number };

  //team
  team: Team;

  selectedTeam: Team;

  //areaManager
  areaManagers: AreaManager[];

  amIdList: number[] = [];

  selectedAreaManager: AreaManager;

  areaManagerGridDataSource: any;

  areaManagerGridBoxValue: number[] = [];

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

  //editor
  editorValueType: string;

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  teamForm!: FormGroup;

  errorMessage = "";

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  constructor(
    private teamServices: TeamService,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private ref: ChangeDetectorRef,
    private areaManagerServices: AreaManagerService,
    private reloadService: ReloadRouteService
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;
    this.statuses = [
      { label: "Không hoạt động", value: 0 },
      { label: "Hoạt động", value: 1 },
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
      areaManager: {
        required: "Người quản lý không được để trống.",
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.teamForm = this.fb.group({
      id: [{ value: "", disabled: true }],
      name: [
        "",
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ],
      ],
      status: ["", [Validators.required]],
      areaManager: ["", [Validators.required]],
    });

    // Load Data
    this.route.queryParams
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((params) => {
          return this.teamServices.getTeamById(params["id"]);
        }),
        switchMap((team) => {
          this.team = team;
          this.team.areaManagerTeams.forEach((amTeam) => {
            this.amIdList = [...this.amIdList, amTeam.areaManagerId];
          });
          this.statuses.forEach((status) => {
            status.value === this.team.status
              ? (this.selectedStatus = status)
              : "";
          });
          return this.areaManagerServices.getAreaManagers();
        })
      )
      .subscribe((areaManagers) => {
        this.areaManagers = areaManagers;
        //gridbox dev extreme
        this.areaManagerGridDataSource = this.makeAsyncDataSource(
          this.areaManagers
        );

        this.areaManagerGridBoxValue = [
          ...this.areaManagerGridBoxValue,
          this.team.areaManagerTeams[this.team.areaManagerTeams.length - 1]
            .areaManagerId,
        ];
        this.teamForm.controls.areaManager.setValue(
          this.areaManagerGridBoxValue
        );

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
    merge(this.teamForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe(() => {
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
    this.team.status = this.selectedStatus.value;
    this.team = new Team(this.team, false);
    this.teamServices
      .updateTeam(this.team)
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((team) => {
          if (
            this.areaManagerGridBoxValue[0] !==
            this.team.areaManagerTeams[this.team.areaManagerTeams.length - 1]
              .areaManagerId
          ) {
            return this.teamServices.deleteAreaManagerIntoTeam(
              [
                this.team.areaManagerTeams[
                  this.team.areaManagerTeams.length - 1
                ].areaManagerId,
              ],
              this.team.id
            );
          }
          return of(team);
        }),
        switchMap((team) => {
          if (
            this.areaManagerGridBoxValue[0] !==
            this.team.areaManagerTeams[this.team.areaManagerTeams.length - 1]
              .areaManagerId
          )
            return this.teamServices.insertAreaManagerIntoTeam(
              this.areaManagerGridBoxValue,
              this.team.id
            );
          return of(team);
        }),
        finalize(() => (this.loading = false))
      )
      .subscribe(
        (teamAfterAdding) => {
          swal.fire({
            title: "Thành công!",
            text: "Cập nhật nhóm thành công.",
            icon: "success",
            customClass: {
              confirmButton: "btn btn-success animation-on-hover",
            },
            buttonsStyling: false,
            timer: 2000,
          });
          this.teamForm.reset();
          this.hideDialog();
        },
        (error) => {
          swal.fire({
            title: "Thất bại!",
            text: "Cập nhật nhóm thất bại",
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

  //extend
  changeStatus(ev) {
    this.selectedStatus = ev.value;
  }
}
