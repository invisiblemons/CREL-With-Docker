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
} from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Broker } from "../broker.model";
import { BrokerService } from "../broker.service";
import { Column } from "src/app/core/models/table.model";
import swal from "sweetalert2";
import { Subject } from "rxjs";
import { TeamService } from "../../team/team.service";
import { Team } from "../../team/team.model";
import { TABLE_CONFIG } from "src/app/shared/constants/common.const";
import { switchMap, takeUntil } from "rxjs/operators";
import {
  FormBuilder,
  FormControlName,
  FormGroup,
  Validators,
} from "@angular/forms";
import { GenericValidator } from "src/app/shared/validator/generic-validator";
import CustomStore from "devextreme/data/custom_store";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { AppointmentService } from "../../appointment/appointment.service";
import { Appointment } from "../../appointment/appointment.model";
import { PropertyService } from "../../property/property.service";
import { Property } from "../../property/property.model";

@Component({
  selector: "app-broker-list",
  templateUrl: "./broker-list.component.html",
  styleUrls: ["./broker-list.component.scss"],
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
  @Output() countBrokers = new EventEmitter<number>();
  // Input Fields
  @Input() teamFromTeamDetail: Team;
  // Form Fields
  selectedBrokersOfTeamDetail: Broker[] = [];
  brokerGridDataSource: any;
  brokerGridBoxValue: number[] = [];
  isBrokerGridBoxOpened: boolean = false;
  brokerGridColumns: any = [
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
  // Validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];
  errorMessage = "";
  brokerForm!: FormGroup;
  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;

  isShowError: boolean = false;

  appointments: Appointment[];

  properties: Property[];

  constructor(
    private brokerServices: BrokerService,
    private teamServices: TeamService,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private reloadServices: ReloadRouteService,
    private ref: ChangeDetectorRef,
    private appointmentService: AppointmentService,
    private propertyServices: PropertyService
  ) {
    this.isShowSpin = true;
    this.loading = false;
    this.statuses = [
      { label: "Đã xoá", value: 0 },
      { label: "Hoạt động", value: 1 },
    ];
    this.tableTitle = "Quản lý nhân viên môi giới";
    this.isLoadActionOfTeamDetail = false;
    this.brokers = [];

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
        field: "id",
        header: "Mã",
        width: "5rem",
        disabled: true,
        visible: true,
        headerAlign: "center",
        textAlign: "center",
      },
      {
        field: "name",
        header: "Tên",
        width: "12rem",
        disabled: true,
        visible: true,
        headerAlign: "left",
        textAlign: "left",
      },
      {
        field: "userName",
        header: "Tên tài khoản",
        width: "12rem",
        disabled: true,
        visible: true,
        headerAlign: "left",
        textAlign: "left",
      },
      {
        field: "email",
        header: "Địa chỉ email",
        width: "15rem",
        disabled: false,
        visible: false,
        headerAlign: "left",
        textAlign: "left",
      },
      {
        field: "gender",
        header: "Giới tính",
        width: "8rem",
        disabled: false,
        visible: false,
        headerAlign: "left",
        textAlign: "left",
      },
      {
        field: "phoneNumber",
        header: "Số điện thoại",
        width: "12rem",
        disabled: false,
        visible: false,
        headerAlign: "right",
        textAlign: "right",
      },
      {
        field: "teamName",
        header: "Tên nhóm",
        width: "12rem",
        disabled: false,
        visible: false,
        headerAlign: "left",
        textAlign: "left",
      },
      {
        field: "status",
        header: "Trạng thái",
        width: "10rem",
        disabled: true,
        visible: true,
        headerAlign: "center",
        textAlign: "center",
      },
      {
        field: "action",
        header: "Thao tác",
        width: "15rem",
        disabled: true,
        visible: true,
        headerAlign: "center",
        textAlign: "center",
      },
    ];
    this.displayCols = this.cols.filter((element) => !element.disabled);
    this._selectedColumns = this.cols.filter((element) => element.visible);

    // Generate Broker Form
    //validate
    this.validationMessages = {
      broker: {
        required: "Nhân viên môi giới không được để trống.",
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit() {
    this.propertyServices
      .getSpecificProperties()
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((properties) => {
          this.properties = properties;
          return this.appointmentService.getActiveAppointments();
        }),
        switchMap((appointments) => {
          this.appointments = appointments;
          return this.teamServices.getTeams();
        }),
        switchMap((teams) => {
          this.teams = teams;

          return this.brokerServices.getBrokers();
        })
      )
      .subscribe((brokers) => {
        this.brokers = brokers;
        this.brokers.forEach((broker) => {
          let team: Team = this.getTeam(broker);
          if (team) {
            if (this.appointments.length > 0) {
              if (
                this.appointments.filter((appm) => appm.brokerId === broker.id)
                  .length > 0
              ) {
                broker.isHasAppointment = true;
              } else {
                broker.isHasAppointment = false;
              }
            }
            if (this.properties.length > 0) {
              if (
                this.properties.filter(
                  (property) => property.brokerId === broker.id
                ).length > 0
              ) {
                broker.isHasPropery = true;
              } else {
                broker.isHasPropery = false;
              }
            }
            broker.team = team;
            broker.teamName = team.name;
          }
        });
        this.rawBrokers = this.brokers;

        // Load Data Broker Of Team Detail
        if (this.isLoadActionOfTeamDetail) {
          let brokersOfTeam = [];
          this.brokers.forEach((brokerOfTeam) => {
            if (!brokerOfTeam.teamId) {
              brokersOfTeam.push(brokerOfTeam);
            }
          });
          // gridbox devex
          this.brokerGridDataSource = this.makeAsyncDataSource(brokersOfTeam);
          this.brokers = this.brokers.filter(
            (broker) => broker.teamId === this.team.id
          );
        }
        this.isShowSpin = false;
      });

    // Validate
    this.brokerForm = this.fb.group({
      broker: ["", [Validators.required]],
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes["teamFromTeamDetail"].currentValue) {
      this.team = changes["teamFromTeamDetail"].currentValue;
      this.tableTitle = "Nhân viên môi giới trong nhóm";
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
              if (brokersOfTeam.length > 0) {
                this.brokerGridDataSource =
                  this.makeAsyncDataSource(brokersOfTeam);

                this.rawBrokers = brokersRes;
              } else {
                this.brokerForm.get("broker").disable();
                this.isShowError = true;
              }
              // gridbox devex
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
    for (let i = 0; i < this.teams.length; i++) {
      if (broker.teamId === this.teams[i].id) {
        return this.teams[i];
      }
    }
    return;
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
        title: "Bạn có chắc muốn xoá các Nhân viên môi giới đã chọn?",
        text: "Những Nhân viên môi giới này sẽ bị xoá!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Có, xoá nó!",
        cancelButtonText: "Không, giữ nguyên",
        customClass: {
          confirmButton: "btn btn-danger animation-on-hover mr-1",
          cancelButton: "btn btn-default animation-on-hover",
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
          this.rawBrokers = this.brokers;
          swal.fire({
            title: "Xoá thành công!",
            text: "Đã xoá những Nhân viên môi giới.",
            icon: "success",
            customClass: {
              confirmButton: "btn btn-success animation-on-hover",
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
        title: "Bạn có chắc muốn xoá?",
        text: "Nhân viên môi giới" + broker.name + " sẽ bị xoá!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Có, xoá nó!",
        cancelButtonText: "Không, giữ nguyên",
        customClass: {
          confirmButton: "btn btn-danger animation-on-hover mr-1",
          cancelButton: "btn btn-default animation-on-hover",
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          broker.status = 1;
          this.brokerServices.deleteBroker(broker).subscribe((res) => {
            if (res) {
              broker.status = 0;
              this.rawBrokers = this.brokers;
              swal.fire({
                title: "Xoá thành công!",
                text: "Nhân viên môi giới" + broker.name + " đã bị xoá!",
                icon: "success",
                customClass: {
                  confirmButton: "btn btn-success animation-on-hover",
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
        title: "Bạn có chắc muốn xoá các Nhân viên môi giới đã chọn?",
        text: "Những Nhân viên môi giới này sẽ bị xoá!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Có, xoá nó!",
        cancelButtonText: "Không, giữ nguyên",
        customClass: {
          confirmButton: "btn btn-danger animation-on-hover mr-1",
          cancelButton: "btn btn-default animation-on-hover",
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
                  this.rawBrokers = this.brokers;
                  swal.fire({
                    title: "Thành công!",
                    text: "Loại bỏ Nhân viên môi giới khỏi nhóm thành công.",
                    icon: "success",
                    customClass: {
                      confirmButton: "btn btn-success animation-on-hover",
                    },
                    buttonsStyling: false,
                    timer: 2000,
                  });
                  this.countBrokers.emit(this.brokers.length);
                }
              });
          });
        }
      });
  }
  removeBrokerOfTeamDetail(broker) {
    swal
      .fire({
        title: "Bạn có chắc muốn loại bỏ?",
        text: "Nhân viên môi giới " + broker.name + " sẽ bị loại bỏ khỏi nhóm!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Có, xoá nó!",
        cancelButtonText: "Không, giữ nguyên",
        customClass: {
          confirmButton: "btn btn-danger animation-on-hover mr-1",
          cancelButton: "btn btn-default animation-on-hover",
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          broker.teamId = null;
          broker.status = 1;
          this.brokerServices.updateBroker(broker).subscribe((brokerRes) => {
            this.brokers = this.brokers.filter((item) => broker.id !== item.id);
            this.rawBrokers = this.brokers;
            swal.fire({
              title: "Thành công!",
              text: "Loại bỏ Nhân viên môi giới khỏi nhóm thành công.",
              icon: "success",
              customClass: {
                confirmButton: "btn btn-success animation-on-hover",
              },
              buttonsStyling: false,
              timer: 2000,
            });
            this.countBrokers.emit(this.brokers.length);
          });
        }
      });
  }
  // Dev extreme
  makeAsyncDataSource(items) {
    return new CustomStore({
      loadMode: "raw",
      key: "id",
      load() {
        return items;
      },
    });
  }
  brokerGridBox_displayExpr(item) {
    return item && `${item.name} <${item.phoneNumber}>`;
  }
  onBrokerGridBoxOptionChanged(e) {
    if (e.name === "value" && e.value) {
      this.selectedBrokersOfTeamDetail = [];
      this.rawBrokers.forEach((broker) => {
        e.value.forEach((id) => {
          broker.id === id ? this.selectedBrokersOfTeamDetail.push(broker) : "";
        });
      });
      if (!e.previousValue) {
        this.ref.detectChanges();
      }
    }
  }
  addBrokerIntoTeam() {
    this.selectedBrokersOfTeamDetail.forEach((broker, index) => {
      broker.teamId = this.team.id;
      broker.status = 2;
      this.brokerServices.updateBroker(broker).subscribe((brokerRes) => {
        this.brokers.unshift(brokerRes);
        this.rawBrokers = this.brokers;
        if (this.selectedBrokersOfTeamDetail.length - 1 === index) {
          swal.fire({
            title: "Thành công!",
            text: "Thêm Nhân viên môi giới vào nhóm thành công.",
            icon: "success",
            customClass: {
              confirmButton: "btn btn-success animation-on-hover",
            },
            buttonsStyling: false,
            timer: 2000,
          });
          this.countBrokers.emit(this.brokers.length);
          this.hideBrokerModal();
        }
      });
    });
  }

  openDetailBroker(broker) {
    this.reloadServices.routingNotReload(
      "/nhan-su/nguoi-moi-gioi/chi-tiet",
      broker.id
    );
  }
  openEditBroker(broker) {
    this.reloadServices.routingNotReload(
      "/nhan-su/nguoi-moi-gioi/chinh-sua",
      broker.id
    );
  }
  openNewBroker() {
    this.reloadServices.routingNotReload(
      "/nhan-su/nguoi-moi-gioi/tao-moi",
      null
    );
  }

  filterStatus(value) {
    this.brokers = this.rawBrokers;
    if (value === 1) {
      this.brokers = this.brokers.filter(
        (broker) => broker.status === 1 || broker.status === 2
      );
    } else if (value === 0) {
      this.brokers = this.brokers.filter((broker) => broker.status === 0);
    }
  }
}
