import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subject } from "rxjs";
import { finalize, switchMap, takeUntil } from "rxjs/operators";
import { DATE_FORMAT } from "src/app/shared/constants/common.const";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { BrokerService } from "../../broker/broker.service";
import { Appointment } from "../appointment.model";
import { AppointmentService } from "../appointment.service";

@Component({
  selector: "app-appointment-detail",
  templateUrl: "./appointment-detail.component.html",
  styleUrls: ["./appointment-detail.component.scss"],
})
export class AppointmentDetailComponent implements OnInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  DATE_FORMAT = DATE_FORMAT;

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  statuses: { label: string; value: number }[];

  appointmentId: string;

  appointment: Appointment;

  isShowBroker = false;

  isShowProperty = false;

  isShowBrand = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private appointmentServices: AppointmentService,
    private reloadService: ReloadRouteService,
    private brokerService: BrokerService
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.statuses = [
      { label: "Đã xoá", value: 0 },
      { label: "Hoạt động", value: 1 },
    ];
  }

  ngOnInit(): void {
    this.route.queryParams
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((params) => {
          return this.appointmentServices.getAppointmentById(params["id"]);
        }),
        switchMap((appointment) => {
          this.appointment = appointment;
          return this.brokerService.getBrokerById(this.appointment.brokerId);
        })
      )
      .subscribe((broker) => {
        this.appointment.broker = broker;
        this.isShowSkeleton = false;
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingNotReload("/thuong-hieu/cuoc-hen", null);
  }

  openBrokerDetail() {
    this.isShowBroker = true;
  }

  statusBrokerDialog() {
    this.isShowBroker = false;
  }
  
  openBrandDetail() {
    this.isShowBrand = true;
  }

  statusBrandDialog() {
    this.isShowBrand = false;
  }

  openPropertyDetail() {
    this.isShowProperty = true;
  }

  statusPropertyDialog() {
    this.isShowProperty = false;
  }
}
