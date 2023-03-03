import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Appointment } from '../appointment.model';
import { AppointmentService } from '../appointment.service';
import { Column } from 'src/app/core/models/table.model';
import swal from 'sweetalert2';
import { Subject } from 'rxjs';
import {
  AVATAR_DEFAULT,
  DATE_TIME_FORMAT,
  TABLE_CONFIG,
} from 'src/app/shared/constants/common.const';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';
import { switchMap, takeUntil } from 'rxjs/operators';
import { TeamService } from '../../team/team.service';
import { LocalStorageService } from '../../login/local-storage.service';
import { Team } from '../../team/team.model';
import { BrokerService } from '../../broker/broker.service';
import { PropertyService } from '../../property/property.service';

@Component({
  selector: 'app-appointment-list',
  templateUrl: './appointment-list.component.html',
  styleUrls: ['./appointment-list.component.scss'],
})
export class AppointmentListComponent implements OnInit, OnDestroy {
  DATE_TIME_FORMAT = DATE_TIME_FORMAT;
  AVATAR_DEFAULT = AVATAR_DEFAULT;
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

  statuses: { label: string; value: number }[];

  selectedStatus: { label: string; value: number };

  //appointment
  appointment: Appointment;

  appointments: Appointment[] = [];

  selectedAppointments: Appointment[] = [];

  index: number;

  teams: Team[] = [];

  constructor(
    private appointmentServices: AppointmentService,
    private router: Router,
    private reloadServices: ReloadRouteService,
    private teamService: TeamService,
    private localStorageService: LocalStorageService,
    private propertyService: PropertyService
  ) {
    this.isShowSpin = true;
    this.loading = false;
    this.statuses = [
      { label: 'Đã xoá', value: 0 },
      { label: 'Đợi xét duyệt', value: 1 },
      { label: 'Sắp diễn ra', value: 2 },
      { label: 'Không duyệt', value: 3 },
      { label: 'Đã diễn ra', value: 4 },
      { label: 'Diễn ra thất bại', value: 5 },
      { label: 'Yêu cầu duyệt', value: 6 },
    ];
    this.appointments = [];

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
        header: 'Mã',
        width: '5rem',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: 'createDateTime',
        header: 'Thời gian tạo',
        width: '12rem',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: 'onDateTime',
        header: 'Thời gian diễn ra',
        width: '12rem',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: "brokerName",
        header: "Nhân viên môi giới",
        width: "12rem",
        disabled: false,
        visible: true,
        headerAlign: "left",
        textAlign: "left",
      },
      {
        field: 'status',
        header: 'Trạng thái',
        width: '10rem',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
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
  }

  ngOnInit() {
    this.getAppointments();
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  //data functions
  getAppointment(appointment: Appointment): Appointment {
    for (let i = 0; i < this.appointments.length; i++) {
      if (appointment.id === this.appointments[i].id) {
        return this.appointments[i];
      }
    }
    return;
  }
  getAppointments() {
    let appointmentsTemp = [];
    this.appointmentServices
      .getAppointments()
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((appointments) => {
          appointmentsTemp = appointments;
          return this.teamService.getTeams();
        })
      )
      .subscribe((teams) => {
        if (teams && teams.length > 0) {
          teams.forEach((team) => {
            if (team.areaManagerTeams.length > 0) {
              if (
                team.areaManagerTeams[team.areaManagerTeams.length - 1]
                  .areaManagerId === this.localStorageService.getUserObject().id
              ) {
                this.teams.push(team);
              }
            }
          });
        } else {
          this.teams = [];
        }
        let propertiesTemp = [];
        if (this.teams && this.teams.length > 0) {
          teams.forEach((team, i) => {
            team.wards.forEach((ward) => {
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
          });

          if (propertiesTemp && propertiesTemp.length > 0) {
            if (appointmentsTemp && appointmentsTemp.length > 0) {
              appointmentsTemp.forEach((appItem, i) => {
                propertiesTemp.forEach((propertyItem) => {
                  if (appItem.propertyId === propertyItem.id) {
                    this.appointments.push(appItem);
                  }
                });
              })

              this.isShowSpin = false;
            } else {
              this.appointments = [];
              this.isShowSpin = false;
            }
          } else {
            this.appointments = [];
            this.isShowSpin = false;
          }
        } else {
          this.appointments = [];
          this.isShowSpin = false;
        }
      });
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
    return this.appointments
      ? this.first === this.appointments.length - this.TABLE_CONFIG.ROWS
      : true;
  }
  isFirstPage(): boolean {
    return this.appointments ? this.first === 0 : true;
  }

  //table functions
  deleteSelectedAppointments() {
    swal
      .fire({
        title: 'Bạn có chắc muốn xoá các cuộc hẹn đã chọn?',
        text: 'Những cuộc hẹn này sẽ bị xoá!',
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
          this.selectedAppointments.forEach((appointment, index) => {
            this.appointmentServices
              .deleteAppointment(appointment)
              .subscribe((res) => {
                if (res) {
                  this.selectedAppointments[index] = res;
                }
              });
          });
          //Update table
          this.appointments.forEach((appointmentItem) => {
            this.selectedAppointments.forEach((selectedAppointmentItem) => {
              if (appointmentItem.id === selectedAppointmentItem.id) {
                appointmentItem = selectedAppointmentItem;
              }
            });
          });
          swal.fire({
            title: 'Xoá thành công!',
            text: 'Cuộc hẹn đã bị xoá.',
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
  deleteAppointment(appointment) {
    swal
      .fire({
        title: 'Bạn có chắc muốn xoá?',
        text: 'Cuộc hẹn ' + appointment.name + ' đã bị xoá.',
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
          this.appointmentServices
            .deleteAppointment(appointment)
            .subscribe((res) => {
              if (res) {
                appointment.status = 0;
                swal.fire({
                  title: 'Xoá thành công!',
                  text: 'Cuộc hẹn đã bị xoá.',
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
  openDetailAppointment(appointment) {
    this.reloadServices.routingNotReload(
      '/thuong-hieu/cuoc-hen/chi-tiet',
      appointment.id
    );
  }
  openEditAppointment(appointment) {
    this.reloadServices.routingNotReload(
      '/thuong-hieu/cuoc-hen/chinh-sua',
      appointment.id
    );
  }
  openNewAppointment() {
    this.reloadServices.routingNotReload('/thuong-hieu/cuoc-hen/tao-moi', null);
  }

  openBrandDetail(appointment) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return true;
    };
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate(['/thuong-hieu/danh-sach/chi-tiet'], {
      queryParams: { id: appointment.brandId, fromAppointment: true },
      skipLocationChange: true,
    });
  }
}
