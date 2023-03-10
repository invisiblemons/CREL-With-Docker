import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChildren,
} from "@angular/core";
import {
  FormBuilder,
  FormControlName,
  FormGroup,
  Validators,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { fromEvent, merge, Observable, Subject } from "rxjs";
import { debounceTime, finalize, switchMap, takeUntil } from "rxjs/operators";
import { Column } from "src/app/core/models/table.model";
import {
  DATE_TIME_FORMAT,
  TABLE_CONFIG,
} from "src/app/shared/constants/common.const";
import { Ward } from "src/app/shared/models/ward.model";
import { LocationService } from "src/app/shared/services/location.service";
import { GenericValidator } from "src/app/shared/validator/generic-validator";
import { Team } from "../team.model";
import { TeamService } from "../team.service";
import swal from "sweetalert2";
import { AreaManagerService } from "../../area-manager/area-manager.service";
import { AreaManager } from "../../area-manager/area-manager.model";
import { District } from "src/app/shared/models/district.model";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { Property } from "../../property/property.model";
import { StringValidator } from "src/app/shared/validator/string-validator";
import { BrokerService } from "../../broker/broker.service";

@Component({
  selector: "app-team-detail",
  templateUrl: "./team-detail.component.html",
  styleUrls: ["./team-detail.component.scss"],
})
export class TeamDetailComponent implements OnInit, AfterViewInit, OnDestroy {
  DATE_TIME_FORMAT = DATE_TIME_FORMAT;
  destroySubs$: Subject<boolean> = new Subject<boolean>();
  isShowSkeleton: boolean;
  isShowDialog: boolean;
  areaModal: boolean;
  isShowAreaDialog: boolean;
  isShowBrokerDialog: boolean;
  isShowPropertyDialog: boolean;
  isShowAreaManagerDialog: boolean;
  isShowAM: boolean = false;

  // Table Fields
  TABLE_CONFIG = TABLE_CONFIG;
  cols: Column[];
  displayCols: Column[];
  _selectedColumns: Column[];
  get selectedColumns(): Column[] {
    return this._selectedColumns;
  }
  set selectedColumns(val: Column[]) {
    this._selectedColumns = this.cols.filter(
      (col: Column) => val.includes(col) || col.disabled
    );
  }
  first = 0;
  isShowSpin: boolean;

  // Object Fields
  statuses: { label: string; value: number }[];
  teamId: string;
  team: Team;
  ward: Ward;
  wards: Ward[] = [];
  selectedWardsInTable: Ward[] = [];
  selectedWardsToDelete: Ward[] = [];
  selectedWardsInModel: Ward[] = [];
  nodes: any = [];
  selectedNodes: any;
  selectedWards: Ward[];

  countBroker: number;

  properties: Property[];

  /* 
  fields for object
  */
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];
  errorMessage = "";
  areaForm!: FormGroup;
  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;

  isShowError: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private teamServices: TeamService,
    private locationService: LocationService,
    private fb: FormBuilder,
    private areaManagerServices: AreaManagerService,
    private ref: ChangeDetectorRef,
    private reloadService: ReloadRouteService,
    private brokerServices: BrokerService
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.isShowSpin = true;
    this.areaModal = false;
    this.statuses = [
      { label: "Kh??ng ho???t ?????ng", value: 0 },
      { label: "Ho???t ?????ng", value: 1 },
    ];

    this.cols = [
      {
        field: "cbb",
        header: "",
        width: "3rem",
        disabled: true,
        visible: true,
        headerAlign: "center",
        textAlign: "center",
      },
      {
        field: "name",
        header: "T??n",
        width: "12rem",
        disabled: true,
        visible: true,
        headerAlign: "left",
        textAlign: "left",
      },
      {
        field: "districtName",
        header: "Qu???n/Huy???n",
        width: "12rem",
        disabled: false,
        visible: false,
        headerAlign: "left",
        textAlign: "left",
      },
      {
        field: "action",
        header: "Thao t??c",
        width: "15rem",
        disabled: true,
        visible: true,
        headerAlign: "center",
        textAlign: "center",
      },
    ];
    this.displayCols = this.cols.filter((element) => !element.disabled);
    this._selectedColumns = this.cols.filter((element) => element.visible);

    // validate
    this.validationMessages = {
      ward: {
        required: "Qu???n, ph?????ng kh??ng ???????c ????? tr???ng.",
        maxWards: "M???t ng?????i qu???n l?? ch??? c?? th??? qu???n l?? t???i ??a 20 ph?????ng.",
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);

    // validate
    this.areaForm = this.fb.group({
      ward: ["", [Validators.required, StringValidator.maxWards("wards")]],
      wards: [""],
    });
  }

  ngOnInit(): void {
    this.loadData();
  }

  ngAfterViewInit(): void {
    // Watch for the blur event from any input element on the form.
    // This is required because the valueChanges does not provide notification on blur
    const controlBlurs: Observable<any>[] = this.formInputElements.map(
      (formControl: ElementRef) => fromEvent(formControl.nativeElement, "blur")
    );

    // Merge the blur event observable with the valueChanges observable
    // so we only need to subscribe once.
    merge(this.areaForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.areaForm
        );
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingNotReload("/nhan-su/nhom", null);
  }

  loadData() {
    this.route.queryParams
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((params) => {
          return this.teamServices.getTeamById(params["id"]);
        }),
        switchMap((team) => {
          this.team = team;
          let propertiesTemp = [];
          this.team.wards.forEach((ward) => {
            if (ward.locations && ward.locations.length > 0) {
              ward.locations.forEach((location) => {
                if (location.properties && location.properties.length > 0) {
                  location.properties.forEach((propertyItemOfAM) => {
                    propertiesTemp = [...propertiesTemp, propertyItemOfAM];
                  });
                }
              });
            }
          });
          this.properties = propertiesTemp;
          this.team.areaManagerTeams.forEach((amTeam, index) => {
            this.areaManagerServices
              .getAreaManagerById(amTeam.areaManagerId)
              .subscribe((am) => {
                amTeam.areaManager = am;
                if (this.team.areaManagerTeams.length - 1 === index) {
                  this.isShowSkeleton = false;
                }
              });
          });
          return this.brokerServices.getBrokersOfTeam(this.team.id);
        }),
        switchMap((brokers) => {
          this.countBroker = brokers.length;
          return this.locationService.getWardsByTeamId(this.team.id);
        }),
        switchMap((wards) => {
          this.wards = wards;
          if (this.wards.length > 0) {
            this.wards.forEach((ward, index) => {
              this.locationService
                .getDistrictById(ward.districtId)
                .subscribe((district) => {
                  ward.districtName = district.name;
                  if (this.wards.length - 1 === index) {
                    this.isShowSpin = false;
                  }
                });
            });
          } else {
            this.isShowSpin = false;
          }
          this.areaForm.controls.wards.setValue(this.wards);
          return this.locationService.getDistricts();
        })
      )
      .subscribe((districts) => {
        districts.forEach((district, index) => {
          this.locationService
            .getActiveWards(district.id)
            .subscribe((wards) => {
              if (wards.length > 0) {
                let itemOfNode = {
                  data: district,
                  label: district.name,
                  children: [],
                  type: "district",
                };
                wards.forEach((ward) => {
                  let itemOfChildrenInNode = {
                    data: ward,
                    label: ward.name,
                    type: "ward",
                  };
                  if (ward.status === 1) {
                    itemOfNode.children = [
                      ...itemOfNode.children,
                      itemOfChildrenInNode,
                    ];
                  }
                });
                this.nodes = [...this.nodes, itemOfNode];
              }
              if (districts.length - 1 === index) {
                if (this.nodes.length === 0) {
                  this.isShowError = true;
                  this.areaForm.get("ward").disable();
                }
              }
            });
        });
      });
  }

  getCount(value) {
    this.countBroker = value;
  }

  hideAreaModal() {
    this.selectedWards = [];
    this.areaModal = false;
  }

  // paging
  next() {
    this.first = this.first + this.TABLE_CONFIG.ROWS;
  }
  prev() {
    this.first = this.first - this.TABLE_CONFIG.ROWS;
  }
  reset() {
    this.first = 0;
  }
  isLastPage(): boolean {
    return this.wards
      ? this.first === this.wards.length - this.TABLE_CONFIG.ROWS
      : true;
  }
  isFirstPage(): boolean {
    return this.wards ? this.first === 0 : true;
  }

  openAreaManagerDetail() {
    this.isShowAM = true;
  }

  getAreaManagerState() {
    this.isShowAM = false;
  }

  openAreaDialog() {
    this.isShowAreaDialog = true;
  }
  hideAreaDialog() {
    this.isShowAreaDialog = false;
  }

  openBrokerDialog() {
    this.isShowBrokerDialog = true;
  }
  hideBrokerDialog() {
    this.isShowBrokerDialog = false;
  }

  openPropertyDialog() {
    this.isShowPropertyDialog = true;
  }
  hidePropertyDialog() {
    this.isShowPropertyDialog = false;
  }

  openAreaManagerDialog() {
    this.isShowAreaManagerDialog = true;
  }
  hideAreaManagerDialog() {
    this.isShowAreaManagerDialog = false;
  }

  // Functions
  openAddAreaModal() {
    this.areaModal = true;
  }

  selectNode(ev) {
    // In case select district
    if (ev.node.type === "district") {
      ev.node.children.forEach((childItem) => {
        this.selectedWardsInModel = [
          ...this.selectedWardsInModel,
          childItem.data,
        ];
      });
    } // In case select ward
    else {
      this.selectedWardsInModel = [...this.selectedWardsInModel, ev.node.data];
    }
  }

  unselectNode(ev) {
    // In case unselect district
    if (ev.node.type === "district") {
      ev.node.children.forEach((unselectItem) => {
        this.selectedWardsInModel = this.selectedWardsInModel.filter((ward) => {
          ward !== unselectItem.data;
        });
      });
    }
    // In case unselect ward
    else {
      this.selectedWardsInModel = this.selectedWardsInModel.filter((ward) => {
        ev.node.data !== ward;
      });
    }
  }

  addArea() {
    this.selectedWardsInModel = [];
    this.selectedNodes.forEach((node) => {
      if (node.type !== "district") this.selectedWardsInModel.push(node.data);
    });
    for (let i = 0; i < this.selectedWardsInModel.length; i++) {
      this.selectedWardsInModel[i].teamId = this.team.id;
      this.selectedWardsInModel[i].status = 2;
      this.selectedWardsInModel[i] = new Ward(
        this.selectedWardsInModel[i],
        false
      );
      this.locationService
        .updateWard(this.selectedWardsInModel[i])
        .subscribe((respondedWard) => {
          if (this.selectedWardsInModel.length - 1 === i) {
            this.locationService
              .getWardsByTeamId(this.team.id)
              .subscribe((wards) => {
                this.wards = wards;
                this.areaForm.controls.wards.setValue(this.wards);
                this.wards.forEach((ward, index) => {
                  this.locationService
                    .getDistrictById(ward.districtId)
                    .subscribe((district) => {
                      ward.districtName = district.name;
                      if (this.wards.length - 1 === index) {
                        this.ref.detectChanges();
                      }
                    });
                });
              });

            swal.fire({
              title: "Th??nh c??ng!",
              text: "Th??m m???i khu v???c v??o nh??m th??nh c??ng.",
              icon: "success",
              customClass: {
                confirmButton: "btn btn-success animation-on-hover",
              },
              buttonsStyling: false,
              timer: 2000,
            });
            this.areaForm.reset();
            this.areaModal = false;
          }
        });
    }
    this.areaModal = false;
  }

  removeWard(ward: Ward) {
    swal
      .fire({
        title: "B???n c?? ch???c mu???n lo???i b??? khu v???c n??y kh???i nh??m?",
        text: "Khu v???c n??y s??? b??? lo???i b??? kh???i nh??m!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "C??, lo???i b??? n??!",
        cancelButtonText: "Kh??ng, gi??? nguy??n",
        customClass: {
          confirmButton: "btn btn-danger animation-on-hover mr-1",
          cancelButton: "btn btn-default animation-on-hover",
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          ward.teamId = null;
          ward.status = 1;
          this.locationService.updateWard(ward).subscribe((wardRes) => {
            this.wards = this.wards.filter(
              (wardItem) => wardItem.id !== wardRes.id
            );
            this.areaForm.controls.wards.setValue(this.wards);
            swal.fire({
              title: "Th??nh c??ng!",
              text: "Lo???i b??? khu v???c kh???i nh??m th??nh c??ng.",
              icon: "success",
              customClass: {
                confirmButton: "btn btn-success animation-on-hover",
              },
              buttonsStyling: false,
              timer: 2000,
            });
          });
        }
      });
  }

  onSingleWardChange() {
    const choosen =
      this.selectedWardsInTable[this.selectedWardsInTable.length - 1];
    this.selectedWardsToDelete.push(choosen);
  }

  deleteSelectedWards() {
    swal
      .fire({
        title: "B???n c?? ch???c mu???n lo???i b??? nh???ng khu v???c ???? ch???n kh???i nh??m?",
        text: "Khu v???c ???? ch???n s??? b??? lo???i b??? kh???i nh??m!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "C??, lo???i b??? n??!",
        cancelButtonText: "Kh??ng, gi??? nguy??n",
        customClass: {
          confirmButton: "btn btn-danger animation-on-hover mr-1",
          cancelButton: "btn btn-default animation-on-hover",
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          this.selectedWardsInTable.forEach((ward, index) => {
            ward.teamId = null;
            ward.status = 1;
            this.locationService.updateWard(ward).subscribe((wardRes) => {
              this.wards = this.wards.filter(
                (wardItem) => wardItem.id !== wardRes.id
              );

              if (this.selectedWardsInTable.length - 1 === index) {
                this.areaForm.controls.wards.setValue(this.wards);
                swal.fire({
                  title: "Th??nh c??ng!",
                  text: "Lo???i b??? khu v???c kh???i nh??m th??nh c??ng.",
                  icon: "success",
                  customClass: {
                    confirmButton: "btn btn-success animation-on-hover",
                  },
                  buttonsStyling: false,
                  timer: 2000,
                });
              }
            });
          });
        }
      });
  }

  calculateWardTotal(name) {
    let total = 0;

    if (this.wards) {
      for (let ward of this.wards) {
        if (ward.districtName === name) {
          total++;
        }
      }
    }

    return total;
  }
}
