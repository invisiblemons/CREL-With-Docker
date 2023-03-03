import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChildren,
} from '@angular/core';
import {
  FormBuilder,
  FormControlName,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { fromEvent, merge, Observable, Subject } from 'rxjs';
import { debounceTime, finalize, switchMap, takeUntil } from 'rxjs/operators';
import { Column } from 'src/app/core/models/table.model';
import { TABLE_CONFIG } from 'src/app/shared/constants/common.const';
import { Ward } from 'src/app/shared/models/ward.model';
import { LocationService } from 'src/app/shared/services/location.service';
import { GenericValidator } from 'src/app/shared/validator/generic-validator';
import { Team } from '../team.model';
import { TeamService } from '../team.service';
import swal from 'sweetalert2';
import { AreaManagerService } from '../../area-manager/area-manager.service';
import { AreaManager } from '../../area-manager/area-manager.model';
import { District } from 'src/app/shared/models/district.model';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';
import { Property } from '../../property/property.model';
import { StringValidator } from 'src/app/shared/validator/string-validator';
import { BrokerService } from '../../broker/broker.service';

@Component({
  selector: 'app-team-detail',
  templateUrl: './team-detail.component.html',
  styleUrls: ['./team-detail.component.scss'],
})
export class TeamDetailComponent implements OnInit, AfterViewInit, OnDestroy {
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
  selectedWardsInTable: Ward[];
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
  errorMessage = '';
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
      { label: 'Không hoạt động', value: 0 },
      { label: 'Hoạt động', value: 1 },
    ];

    this.cols = [
      {
        field: 'cbb',
        header: '',
        width: '3rem',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: 'name',
        header: 'Tên',
        width: '20rem',
        disabled: true,
        visible: true,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'districtName',
        header: 'Quận/Huyện',
        width: '12rem',
        disabled: false,
        visible: false,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'action',
        header: 'Thao tác',
        width: '15rem',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
    ];
    this.displayCols = this.cols.filter((element) => !element.disabled);
    this._selectedColumns = this.cols.filter((element) => element.visible);

    // validate
    this.validationMessages = {
      ward: {
        required: 'Quận, phường không được để trống.',
        maxWards: 'Một người quản lý chỉ có thể quản lý tối đa 20 phường.'
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);

    // validate
    this.areaForm = this.fb.group({
      ward: ['', [Validators.required,StringValidator.maxWards()]],
    });
  }

  ngOnInit(): void {
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
              if(districts.length - 1 === index) {
                if(this.nodes.length===0) {
                  this.isShowError = true;
                  this.areaForm.get('ward').disable();
                }
              }
            });
        });
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
    this.reloadService.routingNotReload('/nhom/danh-sach', null);
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
    if (ev.node.type === 'district') {
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
    if (ev.node.type === 'district') {
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
    for (let i = 0; i < this.selectedWardsInModel.length; i++) {
      this.selectedWardsInModel[i].teamId = this.team.id;
      this.selectedWardsInModel[i].status = 2;
      this.locationService
        .updateWard(this.selectedWardsInModel[i])
        .subscribe((respondedWard) => {
          if (this.selectedWardsInModel.length - 1 === i) {
            this.locationService
              .getWardsByTeamId(this.team.id)
              .subscribe((wards) => {
                this.wards = wards;
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
              title: 'Thành công!',
              text: 'Thêm mới khu vực vào nhóm thành công.',
              icon: 'success',
              customClass: {
                confirmButton: 'btn btn-success animation-on-hover',
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
        title: 'Bạn có chắc muốn loại bỏ khu vực này khỏi nhóm?',
        text: 'Khu vực này sẽ bị loại bỏ khỏi nhóm!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Có, loại bỏ nó!',
        cancelButtonText: 'Không, giữ nguyên',
        customClass: {
          confirmButton: 'btn btn-danger animation-on-hover mr-1',
          cancelButton: 'btn btn-default animation-on-hover',
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
            swal.fire({
              title: 'Thành công!',
              text: 'Loại bỏ khu vực khỏi nhóm thành công.',
              icon: 'success',
              customClass: {
                confirmButton: 'btn btn-success animation-on-hover',
              },
              buttonsStyling: false,
              timer: 2000,
            });
          });
        }
      });
  }
  deleteSelectedWards() {
    swal
      .fire({
        title: 'Bạn có chắc muốn loại bỏ những khu vực đã chọn khỏi nhóm?',
        text: 'Khu vực đã chọn sẽ bị loại bỏ khỏi nhóm!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Có, loại bỏ nó!',
        cancelButtonText: 'Không, giữ nguyên',
        customClass: {
          confirmButton: 'btn btn-danger animation-on-hover mr-1',
          cancelButton: 'btn btn-default animation-on-hover',
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
                swal.fire({
                  title: 'Thành công!',
                  text: 'Loại bỏ khu vực khỏi nhóm thành công.',
                  icon: 'success',
                  customClass: {
                    confirmButton: 'btn btn-success animation-on-hover',
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
