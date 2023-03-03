import { Component, OnDestroy, OnInit } from '@angular/core';
import Chart from 'chart.js/auto';
import { Subject } from 'rxjs';
import { switchMap, takeUntil } from 'rxjs/operators';
import { Column } from '../../core/models/table.model';
import { AreaManager } from '../area-manager/area-manager.model';
import { AreaManagerService } from '../area-manager/area-manager.service';
import { Brand } from '../brand/brand.model';
import { BrandService } from '../brand/brand.service';
import { Broker } from '../broker/broker.model';
import { BrokerService } from '../broker/broker.service';
import { Contract } from '../contract/contract.model';
import { ContractService } from '../contract/contract.service';
import { LocalStorageService } from '../login/local-storage.service';
import { OwnerService } from '../owner/owner.service';
import { Property } from '../property/property.model';
import { PropertyService } from '../property/property.service';
import { Team } from '../team/team.model';
import { TeamService } from '../team/team.service';
import { DATE_FORMAT, TABLE_CONFIG } from '../../shared/constants/common.const';
import { ReloadRouteService } from '../../shared/services/reload-route.service';
import moment from 'moment';

@Component({
  selector: 'app-home',
  templateUrl: 'home.component.html',
  styleUrls: ['home.component.scss'],
})
export class HomeComponent implements OnInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();
  DATE_FORMAT = DATE_FORMAT;
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

  isShowSpin: boolean = true;
  isShowSkeleton: boolean = true;

  properties: Property[];
  brands: Brand[];
  brokers: Broker[];
  areaManagers: AreaManager[];

  contracts: Contract[] = [];

  maxDate: Date = new Date();

  teams: Team[] = [];

  isShowErrorPanel: boolean = false;

  constructor(
    private propertyService: PropertyService,
    private brandService: BrandService,
    private brokerService: BrokerService,
    private areaManagerService: AreaManagerService,
    private reloadRouteService: ReloadRouteService,
    private contractServices: ContractService,
    private ownerService: OwnerService,
    private teamServices: TeamService,
    private localStorageService: LocalStorageService
  ) {
    this.cols = [
      {
        field: 'id',
        header: 'Mã',
        width: '5rem',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: 'startDate',
        header: 'Ngày bắt đầu',
        width: '12rem',
        disabled: false,
        visible: false,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: 'endDate',
        header: 'Ngày kết thúc',
        width: '12rem',
        disabled: false,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: 'brandName',
        header: 'Tên thương hiệu',
        width: '12rem',
        disabled: false,
        visible: false,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'ownerName',
        header: 'Người sở hữu BĐS',
        width: '12rem',
        disabled: false,
        visible: false,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'brokerName',
        header: 'Nhân viên môi giới hỗ trợ',
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
    this.maxDate = new Date(this.maxDate.setMonth(this.maxDate.getMonth() + 1));
  }

  ngOnInit() {
    let rawProperties = [];
    this.propertyService
      .getProperties()
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((properties) => {
          rawProperties = properties;
          return this.brandService.getBrands();
        }),
        switchMap((brands) => {
          this.brands = brands;
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
            let propertiesTemp = [];
            rawProperties.forEach((property, index) => {
              this.teams.forEach((team) => {
                team.wards.forEach((ward) => {
                  if (property.location.ward.id === ward.id) {
                    propertiesTemp.push(property);
                  }
                });
              });
              if (rawProperties.length - 1 === index) {
                if (propertiesTemp && propertiesTemp.length > 0) {
                  this.properties = propertiesTemp;
                } else {
                  this.properties = [];
                }
              }
            });
          } else {
            this.teams = [];
          }
          if (this.teams.length === 0) {
            this.isShowErrorPanel = true;
          }
          return this.areaManagerService.getAreaManagers();
        }),
        switchMap((areaManagers) => {
          this.areaManagers = areaManagers;
          return this.brokerService.getBrokers();
        }),
        switchMap((brokers) => {
          let brokersTemp = [];
          brokers.forEach((broker) => {
            let team: Team = this.getTeam(broker);
            if (team) {
              brokersTemp.push(broker);
              broker.team = team;
              broker.teamName = team.name;
            }
          });
          this.brokers = brokersTemp;

          return this.contractServices.getMaxEndDateContracts(
            moment(this.maxDate).format('YYYY-MM-DDThh:mm:ss')+"Z"
          );
        })
      )
      .subscribe((contracts: Contract[]) => {
        if (contracts && contracts.length > 0) {
          contracts.forEach((childContract, childIndex) => {
            this.brokerService
              .getBrokerById(childContract.brokerId)
              .pipe(
                takeUntil(this.destroySubs$),
                switchMap((broker) => {
                  childContract.broker = broker;
                  childContract.brokerName = childContract.broker.name;
                  return this.ownerService.getOwnerById(childContract.ownerId);
                })
              )
              .subscribe((owner) => {
                childContract.owner = owner;
                childContract.ownerName = childContract.owner.name;
                childContract.brandName = childContract.brand.name;
                if (childIndex === contracts.length - 1) {
                  this.contracts = contracts;
                  this.isShowSkeleton = false;
                  this.isShowSpin = false;
                }
              });
          });
        } else {
          this.contracts = [];
          this.isShowSkeleton = false;
          this.isShowSpin = false;
        }
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  getTeam(broker: Broker): Team {
    if (this.teams && this.teams.length > 0) {
      for (let i = 0; i < this.teams.length; i++) {
        if (broker.teamId === this.teams[i].id) {
          return this.teams[i];
        }
      }
    } else return null;
  }

  openProperties() {
    this.reloadRouteService.routingReload('/bat-dong-san/danh-sach', null);
  }

  openBrands() {
    this.reloadRouteService.routingReload('/thuong-hieu/danh-sach', null);
  }

  openBrokers() {
    this.reloadRouteService.routingReload('/nguoi-moi-gioi', null);
  }

  openTeams() {
    this.reloadRouteService.routingReload('/nhom/danh-sach', null);
  }

  openDetailContract(contract) {
    this.reloadRouteService.routingReload(
      '/thuong-hieu/hop-dong/chi-tiet',
      contract.id
    );
  }
}
