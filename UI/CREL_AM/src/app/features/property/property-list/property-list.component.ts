import {
  Component,
  Input,
  OnDestroy,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { Router } from '@angular/router';
import { Property } from '../property.model';
import { PropertyService } from '../property.service';
import { Column } from 'src/app/core/models/table.model';
import swal from 'sweetalert2';
import { Subject } from 'rxjs';
import { ProjectService } from '../../project/project.service';
import { Project } from '../../project/project.model';
import { LocationService } from 'src/app/shared/services/location.service';
import { TABLE_CONFIG } from 'src/app/shared/constants/common.const';
import { Broker } from '../../broker/broker.model';
import { BrokerService } from '../../broker/broker.service';
import { Owner } from '../../owner/owner.model';
import { OwnerService } from '../../owner/owner.service';
import { isNumeric } from 'rxjs/util/isNumeric';
import { Team } from '../../team/team.model';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';
import { switchMap, takeUntil } from 'rxjs/operators';
import { Ward } from 'src/app/shared/models/ward.model';
import { StreetSegment } from 'src/app/shared/models/streetSegment.model';
import { District } from 'src/app/shared/models/district.model';
import { TeamService } from '../../team/team.service';
import { LocalStorageService } from '../../login/local-storage.service';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.scss'],
})
export class PropertyListComponent implements OnInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

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
  loading: boolean;

  /* 
  fields for object
  */
  //status
  statuses: { label: string; value: number }[];
  selectedStatus: { label: string; value: number };
  //direction
  directions: { label: string; value: number }[];
  selectedDirection: { label: string; value: number };
  //property
  property: Property;
  properties: Property[];
  selectedProperties: Property[];
  index: number;
  //project
  project: Project;
  projects: Project[];
  //broker
  broker: Broker;
  brokers: Broker[];
  //owner
  owner: Owner;
  owners: Owner[];

  teams: Team[] = [];
  districtIdList: number[] = [];

  // Fields of Team Detail
  tableTitle: string;
  isShowTableAction: boolean;
  @Input() brokerFromBrokerDetail: Broker;

  // Fields of Team Detail
  @Input() teamFromTeamDetail: Team;
  @Input() propertiesOfTeam: Property[];
  isLoadActionOfTeamDetail: boolean;
  tableTitleOfTeamDetail: string;

  constructor(
    private propertyServices: PropertyService,
    private projectServices: ProjectService,
    private brokerServices: BrokerService,
    private ownerServices: OwnerService,
    private router: Router,
    private reloadServices: ReloadRouteService,
    private locationServices: LocationService,
    private teamServices: TeamService,
    private localStorageService: LocalStorageService
  ) {
    this.isShowSpin = true;
    this.loading = false;
    this.tableTitle = 'Quản lý bất động sản thương mại';
    this.isShowTableAction = true;
    this.isLoadActionOfTeamDetail = false;

    //direction
    this.directions = [
      { label: 'Bắc', value: 0 },
      { label: 'Đông Bắc', value: 1 },
      { label: 'Đông', value: 2 },
      { label: 'Đông Nam', value: 3 },
      { label: 'Nam', value: 4 },
      { label: 'Tây Nam', value: 5 },
      { label: 'Tây', value: 6 },
      { label: 'Tây Bắc', value: 7 },
    ];
    this.statuses = [
      { label: 'Đã xoá', value: 0 },
      { label: 'Đợi cập nhật', value: 1 },
      { label: 'Đợi cho thuê', value: 2 },
      { label: 'Đang được thuê', value: 3 },
      { label: 'Quá hạn thuê', value: 4 },
    ];
    this.properties = [];

    this.cols = [
      {
        field: 'cbb',
        header: '',
        width: '3rem',
        layout: 'nowrap',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: 'id',
        header: 'Mã',
        width: '5rem',
        layout: 'nowrap',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: 'addressFull',
        header: 'Địa chỉ',
        width: '20rem',
        layout: 'unset',
        disabled: true,
        visible: true,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'brokerName',
        header: 'Nhân viên môi giới cập nhật',
        width: '10rem',
        layout: 'nowrap',
        disabled: false,
        visible: false,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'ownerNames',
        header: 'Người sở hữu',
        width: '10rem',
        layout: 'nowrap',
        disabled: true,
        visible: true,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'name',
        header: 'Tiêu đề',
        width: '20rem',
        layout: 'unset',
        disabled: false,
        visible: false,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'projectName',
        header: 'Dự án',
        width: '10rem',
        layout: 'nowrap',
        disabled: false,
        visible: false,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'price',
        header: 'Giá cho thuê',
        width: '10rem',
        layout: 'nowrap',
        disabled: false,
        visible: false,
        type: 'money',
        headerAlign: 'right',
        textAlign: 'right',
      },
      {
        field: 'status',
        header: 'Trạng thái',
        width: '10rem',
        layout: 'nowrap',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: 'action',
        header: 'Thao tác',
        width: '15rem',
        layout: 'nowrap',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
    ];
    this.displayCols = this.cols.filter((element) => !element.disabled);
    this._selectedColumns = this.cols.filter((element) => element.visible);
  }

  ngOnInit() {
    this.projectServices
      .getProjects()
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((projects) => {
          this.projects = projects;
          return this.brokerServices.getBrokers();
        }),
        switchMap((brokers) => {
          this.brokers = brokers;
          return this.ownerServices.getOwners();
        }),
        switchMap((owners) => {
          this.owners = this.owners;
          return this.teamServices.getTeams();
        }),
        switchMap((teams) => {
          if (teams && teams.length > 0) {
            teams.forEach((team) => {
              if (team.areaManagerTeams.length > 0) {
                if (
                  team.areaManagerTeams[team.areaManagerTeams.length - 1]
                    .areaManagerId ===
                  this.localStorageService.getUserObject().id
                ) {
                  this.teams.push(team);
                }
              }
            });
          } else {
            this.teams = [];
          }
          return this.propertyServices.getProperties();
        })
      )
      .subscribe((properties) => {
        let propertiesTemp = [];
        properties.forEach((property, index) => {
          this.teams.forEach((team) => {
            team.wards.forEach((ward) => {
              if (property.location.ward.id === ward.id) {
                propertiesTemp.push(property);
              }
            });
          });
          if (properties.length - 1 === index) {
            if (propertiesTemp && propertiesTemp.length > 0) {
              if (this.isLoadActionOfTeamDetail) {
                this.isShowSpin = false;
              } else {
                this.getDetailProperty(propertiesTemp);
              }
            } else {
              this.properties = [];

              this.isShowSpin = false;
            }
          }
        });
      });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (
      changes['brokerFromBrokerDetail'] &&
      changes['brokerFromBrokerDetail'].currentValue
    ) {
      this.tableTitle = 'Bất động sản thương mại đã đi cập nhật';
      this.isShowTableAction = true;
    }
    if (
      changes['teamFromTeamDetail'] &&
      changes['teamFromTeamDetail'].currentValue
    ) {
      this.tableTitle = 'Bất động sản thương mại trong nhóm';
      this.isLoadActionOfTeamDetail = true;
    }
    if (
      changes['propertiesOfTeam'] &&
      changes['propertiesOfTeam'].currentValue
    ) {
      if (changes['propertiesOfTeam'].currentValue.length > 0) {
        this.getDetailProperty(changes['propertiesOfTeam'].currentValue);
      } else {
        this.properties = [];
      }
    }
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  //data functions
  getDetailProperty(properties) {
    properties.forEach((property, index) => {
      //get project name
      if (property.projectId) {
        let project: Project = this.getProject(property);
        if (project) {
          property.project = project;
          property.projectName = project.name;
        }
      }
      //get broker name
      if (property.brokerId) {
        let broker: Broker = this.getBroker(property);
        if (broker) {
          property.broker = broker;
          property.brokerName = broker.name;
        }
      }
      //get owner name
      if (property.owners.length != 0) {
        property.ownerNames = [];
        property.owners.forEach((owner) => {
          property.ownerNames.unshift(owner.name);
        });
      }
      // Get location
      this.locationServices
        .getWardById(property.location.wardId)
        .pipe(
          takeUntil(this.destroySubs$),
          switchMap((ward: Ward) => {
            property.location.ward = ward;
            //get street by id
            return this.locationServices.getStreetSegmentById(
              property.location.streetSegment.id
            );
          }),
          switchMap((streetSegment: StreetSegment) => {
            property.location.streetSegment = streetSegment;
            //get district
            return this.locationServices.getDistrictById(
              property.location.ward.districtId
            );
          })
        )
        .subscribe((district: District) => {
          //get address
          property.addressFull =
            property.location.address +
            ', ' +
            property.location.streetSegment.name +
            ', ' +
            property.location.ward.name +
            ', ' +
            district.name;
          if (properties.length - 1 === index) {
            this.properties = properties;
            this.isShowSpin = false;
          }
        });
    });
  }
  //project
  getProject(property: Property): Project {
    if (this.projects) {
      for (let i = 0; i < this.projects.length; i++) {
        if (property.projectId === this.projects[i].id) {
          return this.projects[i];
        }
      }
    }
    return;
  }
  getProjectOfPropertyByProjectIdForDelete(property: Property): Property {
    let project = this.getProject(property);
    property.project = project;
    return property;
  }

  //owner
  getOwner(property: Property): Owner[] {
    let owners;
    for (let i = 0; i < this.owners.length; i++) {
      property.owners.forEach((owner) => {
        if (owner.id === this.owners[i].id) {
          owners.unshift(owner);
        }
      });
    }
    return owners;
  }
  getOwnersOfPropertyByOwnerIdForDelete(property: Property): Property {
    let owners = this.getOwner(property);
    property.owners = owners;
    return property;
  }

  getBroker(property: Property): Broker {
    if (this.brokers) {
      for (let i = 0; i < this.brokers.length; i++) {
        if (property.brokerId === this.brokers[i].id) {
          return this.brokers[i];
        }
      }
    }
    return;
  }
  getBrokerOfPropertyByBrokerIdForDelete(property: Property): Property {
    let broker = this.getBroker(property);
    property.broker = broker;
    return property;
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
    return this.properties
      ? this.first === this.properties.length - this.TABLE_CONFIG.ROWS
      : true;
  }
  isFirstPage(): boolean {
    return this.properties ? this.first === 0 : true;
  }

  //table functions
  deleteSelectedProperties() {
    swal
      .fire({
        title: 'Bạn có chắc muốn xoá các bất động sản thương mại đã chọn?',
        text: 'Những bất động sản thương mại này sẽ bị xoá!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Có, xoá nó!',
        cancelButtonText: 'Không, giữ nguyên',
        customClass: {
          confirmButton: 'btn btn-danger animation-on-hover mr-1',
          cancelButton: 'btn btn-default animation-on-hover',
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          this.selectedProperties.forEach((property, index) => {
            this.propertyServices.deleteProperty(property).subscribe((res) => {
              if (res) {
                this.selectedProperties[index] = res;
              }
            });
          });
          //Update table
          this.properties.forEach((propertyItem) => {
            this.selectedProperties.forEach((selectedPropertyItem) => {
              if (propertyItem.id === selectedPropertyItem.id) {
                propertyItem = selectedPropertyItem;
                propertyItem =
                  this.getProjectOfPropertyByProjectIdForDelete(propertyItem);
                propertyItem =
                  this.getBrokerOfPropertyByBrokerIdForDelete(propertyItem);
                propertyItem =
                  this.getOwnersOfPropertyByOwnerIdForDelete(propertyItem);
              }
            });
          });
          swal.fire({
            title: 'Đã xoá!',
            text: 'Bất động sản đã xoá.',
            icon: 'success',
            customClass: {
              confirmButton: 'btn btn-success animation-on-hover',
            },
            buttonsStyling: false,
            timer: 2000,
          });
        }
      });
  }
  deleteProperty(property) {
    swal
      .fire({
        title: 'Bạn có chắc muốn xoá?',
        text: 'Bất động sản này sẽ bị xoá!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Có, xoá nó!',
        cancelButtonText: 'Không, giữ nguyên',
        customClass: {
          confirmButton: 'btn btn-danger animation-on-hover mr-1',
          cancelButton: 'btn btn-default animation-on-hover',
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          this.propertyServices.deleteProperty(property).subscribe((res) => {
            if (res) {
              property.status = 0;
              swal.fire({
                title: 'Đã xoá!',
                text: 'Bất động sản đã xoá.',
                icon: 'success',
                customClass: {
                  confirmButton: 'btn btn-success animation-on-hover',
                },
                buttonsStyling: false,
                timer: 2000,
              });
            }
          });
        }
      });
  }
  openDetailProperty(property) {
    this.reloadServices.routingNotReload(
      '/bat-dong-san/danh-sach/chi-tiet',
      property.id
    );
  }
  openEditProperty(property) {
    this.reloadServices.routingNotReload(
      '/bat-dong-san/danh-sach/chinh-sua',
      property.id
    );
  }
  openNewProperty() {
    this.reloadServices.routingNotReload(
      '/bat-dong-san/danh-sach/tao-moi',
      null
    );
  }
  suggestSelectedProperties() {
    let propertyIdList = [];
    let check = false;
    this.selectedProperties.forEach((property) => {
      if (property.status !== 2) {
        check = true;
      } else {
        propertyIdList = [...propertyIdList, property.id];
      }
    });
    if (check) {
      swal.fire({
        title: 'Không thể đề xuất!',
        text: 'Danh sách đã chọn chứa BĐS TM có trạng thái đợi cập nhật.',
        icon: 'warning',
        customClass: {
          confirmButton: 'btn btn-warning animation-on-hover',
        },
        buttonsStyling: false,
        timer: 2000,
      });
    } else {
      this.router.routeReuseStrategy.shouldReuseRoute = function () {
        return true;
      };
      this.router.onSameUrlNavigation = 'reload';
      this.router.navigate(['/bat-dong-san/danh-sach/de-xuat'], {
        queryParams: { propertyIdList: propertyIdList },
        skipLocationChange: true,
      });
    }
  }
}
