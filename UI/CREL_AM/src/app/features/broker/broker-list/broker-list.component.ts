import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  SimpleChanges,
  ViewChildren,
} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Broker } from '../broker.model';
import { BrokerService } from '../broker.service';
import { Column } from 'src/app/core/models/table.model';
import swal from 'sweetalert2';
import { Subject } from 'rxjs';
import { TeamService } from '../../team/team.service';
import { Team } from '../../team/team.model';
import { TABLE_CONFIG } from 'src/app/shared/constants/common.const';
import { switchMap, takeUntil } from 'rxjs/operators';
import {
  FormBuilder,
  FormControlName,
  FormGroup,
  Validators,
} from '@angular/forms';
import { GenericValidator } from 'src/app/shared/validator/generic-validator';
import CustomStore from 'devextreme/data/custom_store';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';
import { LocalStorageService } from '../../login/local-storage.service';

@Component({
  selector: 'app-broker-list',
  templateUrl: './broker-list.component.html',
  styleUrls: ['./broker-list.component.scss'],
})
export class BrokerListComponent implements OnInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

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

  // Object Fields
  statuses: { label: string; value: number }[];
  selectedStatus: { label: string; value: number };
  //broker
  broker: Broker;
  brokers: Broker[] = [];
  rawBrokers: Broker[] = [];
  selectedBrokers: Broker[] = [];
  index: number;
  //team
  team: Team;
  teams: Team[] = [];

  // Fields Of Team Detail
  tableTitle: string;
  isLoadActionOfTeamDetail: boolean;
  brokerModal: boolean = false;
  // Input Fields
  @Input() teamFromTeamDetail: Team;
  // Form Fields
  selectedBrokersOfTeamDetail: Broker[] = [];
  brokerGridDataSource: any;
  brokerGridBoxValue: number[] = [];
  isBrokerGridBoxOpened: boolean = false;
  brokerGridColumns: any = [
    {
      caption: 'T??n',
      dataField: 'name',
      dataType: 'string',
    },
    {
      caption: 'S??? ??i???n tho???i',
      dataField: 'phoneNumber',
      dataType: 'string',
    },
  ];
  // Validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];
  errorMessage = '';
  brokerForm!: FormGroup;
  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;

  @Output() countBrokers = new EventEmitter<number>();

  constructor(
    private brokerServices: BrokerService,
    private teamServices: TeamService,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private reloadServices: ReloadRouteService,
    private ref: ChangeDetectorRef,
    private localStorageService: LocalStorageService
  ) {
    this.isShowSpin = true;
    this.loading = false;
    this.statuses = [
      { label: '???? xo??', value: 0 },
      { label: 'Ho???t ?????ng', value: 1 },
    ];
    this.tableTitle = 'Qu???n l?? nh??n vi??n m??i gi???i';
    this.isLoadActionOfTeamDetail = false;
    this.brokers = [];

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
        field: 'id',
        header: 'M??',
        width: '5rem',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: 'name',
        header: 'T??n',
        width: '12rem',
        disabled: true,
        visible: true,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'userName',
        header: 'T??n t??i kho???n',
        width: '12rem',
        disabled: true,
        visible: true,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'email',
        header: '?????a ch??? email',
        width: '15rem',
        disabled: false,
        visible: false,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'gender',
        header: 'Gi???i t??nh',
        width: '8rem',
        disabled: false,
        visible: false,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'phoneNumber',
        header: 'S??? ??i???n tho???i',
        width: '12rem',
        disabled: false,
        visible: false,
        headerAlign: 'right',
        textAlign: 'right',
      },
      {
        field: 'teamName',
        header: 'T??n nh??m',
        width: '12rem',
        disabled: false,
        visible: false,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'status',
        header: 'Tr???ng th??i',
        width: '10rem',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: 'action',
        header: 'Thao t??c',
        width: '15rem',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
    ];
    this.displayCols = this.cols.filter((element) => !element.disabled);
    this._selectedColumns = this.cols.filter((element) => element.visible);

    // Generate Broker Form
    //validate
    this.validationMessages = {
      broker: {
        required: 'Nh??n vi??n m??i gi???i kh??ng ???????c ????? tr???ng.',
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit() {
    this.teamServices
      .getTeams()
      .pipe(
        takeUntil(this.destroySubs$),
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
          return this.brokerServices.getBrokers();
        })
      )
      .subscribe((brokers) => {
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
        this.rawBrokers = this.brokers;

        // Load Data Broker Of Team Detail
        if (this.isLoadActionOfTeamDetail) {
          // gridbox devex
          this.brokerGridDataSource = this.makeAsyncDataSource(this.brokers);
          this.brokers = this.brokers.filter(
            (broker) => broker.teamId === this.team.id
          );
        }
        this.isShowSpin = false;
      });

    // Validate
    this.brokerForm = this.fb.group({
      broker: ['', [Validators.required]],
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes["teamFromTeamDetail"].currentValue) {
      this.team = changes["teamFromTeamDetail"].currentValue;
      this.tableTitle = "Nh??n vi??n m??i gi???i trong nh??m";
      this.isLoadActionOfTeamDetail = true;
      this.brokerServices
        .getBrokersOfTeam(this.team.id)
        .subscribe((brokers) => {
          this.brokers = brokers;
          this.countBrokers.emit(brokers.length);
          this.brokers.forEach((broker) => {
            let team: Team = this.getTeam(broker);
            if (team) {
              broker.team = team;
              broker.teamName = team.name;
            }
          });
          // Load Data Broker Of Team Detail
          if (this.isLoadActionOfTeamDetail) {
            // gridbox devex
            let activeBrokers;
            this.brokerServices.getActiveBrokers().subscribe((brokersRes) => {
              let brokersOfTeam = [];
              brokersRes.forEach((brokerOfTeam) => {
                if (!brokerOfTeam.teamId) {
                  brokersOfTeam.push(brokerOfTeam);
                }
              });
            });
          }
        });
    }
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
  updateTeam(broker: Broker): Broker {
    let team = this.getTeam(broker);
    broker.team = team;
    broker.teamName = team.name;
    return broker;
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
    return this.brokers
      ? this.first === this.brokers.length - this.TABLE_CONFIG.ROWS
      : true;
  }
  isFirstPage(): boolean {
    return this.brokers ? this.first === 0 : true;
  }

  //table functions
  deleteSelectedBrokers() {
    swal
      .fire({
        title: 'B???n c?? ch???c mu???n xo?? c??c h???p ?????ng ???? ch???n?',
        text: 'Nh???ng th????ng hi???u n??y s??? b??? xo??!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'C??, xo?? n??!',
        cancelButtonText: 'Kh??ng, gi??? nguy??n',
        customClass: {
          confirmButton: 'btn btn-danger animation-on-hover mr-1',
          cancelButton: 'btn btn-default animation-on-hover',
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          this.selectedBrokers.forEach((broker, index) => {
            this.brokerServices.deleteBroker(broker).subscribe((res) => {
              if (res) {
                this.selectedBrokers[index] = res;
              }
            });
          });
          //Update table
          this.brokers.forEach((brokerItem) => {
            this.selectedBrokers.forEach((selectedBrokerItem) => {
              if (brokerItem.id === selectedBrokerItem.id) {
                brokerItem = selectedBrokerItem;
                brokerItem = this.updateTeam(brokerItem);
              }
            });
          });
          swal.fire({
            title: 'Xo?? th??nh c??ng!',
            text: '???? xo?? nh???ng Nh??n vi??n m??i gi???i.',
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
  deleteBroker(broker) {
    swal
      .fire({
        title: 'B???n c?? ch???c mu???n xo???',
        text: "'Nh??n vi??n m??i gi???i'" + broker.name + "' s??? b??? xo??!'",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'C??, xo?? n??!',
        cancelButtonText: 'Kh??ng, gi??? nguy??n',
        customClass: {
          confirmButton: 'btn btn-danger animation-on-hover mr-1',
          cancelButton: 'btn btn-default animation-on-hover',
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          broker.status = 1;
          this.brokerServices.deleteBroker(broker).subscribe((res) => {
            if (res) {
              broker.status = 0;
              swal.fire({
                title: 'Xo?? th??nh c??ng!',
                text: "'Nh??n vi??n m??i gi???i'" + broker.name + "' ???? b??? xo??!'",
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

  // Action Of Team Detail
  openAddBrokerOfTeamDetail() {
    this.brokerModal = true;
  }
  hideBrokerModal() {
    this.brokerModal = false;
  }
  removeSelectedBrokersOfTeamDetail() {
    swal
      .fire({
        title: 'B???n c?? ch???c mu???n lo???i b????',
        text: 'Nh??n vi??n m??i gi???i n??y s??? b??? lo???i b???!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'C??, xo?? n??!',
        cancelButtonText: 'Kh??ng, gi??? nguy??n',
        customClass: {
          confirmButton: 'btn btn-danger animation-on-hover mr-1',
          cancelButton: 'btn btn-default animation-on-hover',
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          this.selectedBrokers.forEach((selectedBroker, index) => {
            selectedBroker.teamId = null;
            selectedBroker.status = 1;
            this.brokerServices
              .updateBroker(selectedBroker)
              .subscribe((brokerRes) => {
                this.brokers = this.brokers.filter(
                  (item) => selectedBroker.id !== item.id
                );
                if (this.selectedBrokers.length - 1 === index) {
                  swal.fire({
                    title: 'Th??nh c??ng!',
                    text: 'Lo???i b??? Nh??n vi??n m??i gi???i kh???i nh??m th??nh c??ng.',
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
  removeBrokerOfTeamDetail(broker) {
    swal
      .fire({
        title: 'B???n c?? ch???c mu???n lo???i b????',
        text: 'Nh??n vi??n m??i gi???i n??y s??? b??? lo???i b??? kh???i nh??m!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'C??, xo?? n??!',
        cancelButtonText: 'Kh??ng, gi??? nguy??n',
        customClass: {
          confirmButton: 'btn btn-danger animation-on-hover mr-1',
          cancelButton: 'btn btn-default animation-on-hover',
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          broker.teamId = null;
          broker.status = 1;
          this.brokerServices.updateBroker(broker).subscribe((brokerRes) => {
            this.brokers = this.brokers.filter((item) => broker.id !== item.id);
            swal.fire({
              title: 'Th??nh c??ng!',
              text: 'Lo???i b??? Nh??n vi??n m??i gi???i kh???i nh??m th??nh c??ng.',
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
  // Dev extreme
  makeAsyncDataSource(items) {
    return new CustomStore({
      loadMode: 'raw',
      key: 'id',
      load() {
        return items;
      },
    });
  }
  brokerGridBox_displayExpr(item) {
    return item && `${item.name} <${item.phoneNumber}>`;
  }
  onBrokerGridBoxOptionChanged(e) {
    if (e.name === 'value' && e.value) {
      this.selectedBrokersOfTeamDetail = [];
      this.rawBrokers.forEach((broker) => {
        e.value.forEach((id) => {
          broker.id === id ? this.selectedBrokersOfTeamDetail.push(broker) : '';
        });
      });
      if (!e.previousValue) {
        this.ref.detectChanges();
      }
    }
  }
  addBrokerIntoTeam() {
    console.log(this.selectedBrokersOfTeamDetail);
    this.selectedBrokersOfTeamDetail.forEach((broker, index) => {
      broker.teamId = this.team.id;
      broker.status = 2;
      this.brokerServices.updateBroker(broker).subscribe((brokerRes) => {
        this.brokers.unshift(brokerRes);
        if (this.selectedBrokersOfTeamDetail.length - 1 === index) {
          swal.fire({
            title: 'Th??nh c??ng!',
            text: 'Th??m Nh??n vi??n m??i gi???i v??o nh??m th??nh c??ng.',
            icon: 'success',
            customClass: {
              confirmButton: 'btn btn-success animation-on-hover',
            },
            buttonsStyling: false,
            timer: 2000,
          });
          this.hideBrokerModal();
        }
      });
    });
  }

  openDetailBroker(broker) {
    this.reloadServices.routingNotReload(
      '/nguoi-moi-gioi/danh-sach/chi-tiet',
      broker.id
    );
  }
  openEditBroker(broker) {
    this.reloadServices.routingNotReload(
      '/nguoi-moi-gioi/danh-sach/chinh-sua',
      broker.id
    );
  }
  openNewBroker() {
    this.reloadServices.routingNotReload(
      '/nguoi-moi-gioi/danh-sach/tao-moi',
      null
    );
  }
}
