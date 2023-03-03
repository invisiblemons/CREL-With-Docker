import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subject } from "rxjs";
import { finalize, switchMap, takeUntil } from "rxjs/operators";
import { DATE_FORMAT } from "src/app/shared/constants/common.const";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { Team } from "../../team/team.model";
import { TeamService } from "../../team/team.service";
import { Broker } from "../../broker/broker.model";
import { BrokerService } from "../../broker/broker.service";
import swal from "sweetalert2";
import { Appointment } from "../appointment.model";

@Component({
  selector: "app-broker-detail",
  templateUrl: "./broker-detail.component.html",
  styleUrls: ["./broker-detail.component.scss"],
})
export class BrokerDetailComponent implements OnInit, OnDestroy {
  DATE_FORMAT = DATE_FORMAT;
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  isShowUpdatedPropertyDialog: boolean;
  isShowRentingPropertyDialog: boolean;
  isShowSuportingBrandDialog: boolean;
  isShowAppointmentDialog: boolean;

  statuses: { label: string; value: number }[];

  brokerId: string;

  broker: Broker;

  team: Team;

  teams: Team[] = [];

  allData: any;

  allOptions: any;

  @Input() appointment: Appointment;

  @Output() statusBrokerDialog = new EventEmitter();

  constructor(
    private brokerServices: BrokerService,
    private route: ActivatedRoute,
    private router: Router,
    private teamServices: TeamService,
    private reloadService: ReloadRouteService,
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
  }

  ngOnInit(): void {
    // Load Data
    this.brokerServices.getBrokerById(this.appointment.broker.id)
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((broker) => {
          this.broker = broker;
          return this.teamServices.getTeams();
        })
      )
      .subscribe((teams) => {
        this.teams = teams;
        this.broker = this.updateTeam(this.broker);
        this.isShowSkeleton = false;
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  //data functions
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
    if (team) {
      broker.team = team;
      broker.teamName = team.name;
    }
    return broker;
  }
  
  hideDialog() {
    this.statusBrokerDialog.emit();
  }

  openTeamDetail() {
    this.reloadService.routingReload("/nhom/danh-sach/chi-tiet", this.broker.team.id)
  }
}
