import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subject } from "rxjs";
import { finalize, switchMap, takeUntil } from "rxjs/operators";
import { Team } from "../../team/team.model";
import { TeamService } from "../../team/team.service";
import { Broker } from "../broker.model";
import { BrokerService } from "../broker.service";
import { DATE_FORMAT } from "src/app/shared/constants/common.const";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";

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
    this.route.queryParams
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((params) => {
          return this.brokerServices.getBrokerById(params["id"]);
        }),
        switchMap((broker) => {
          this.broker = broker;
          return this.teamServices.getTeams();
        })
      )
      .subscribe((teams) => {
        this.teams = teams;
        this.broker = this.updateTeam(this.broker);
        this.loadChartData();
        this.isShowSkeleton = false;
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingNotReload('/nguoi-moi-gioi', null);
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
  loadChartData() {
    this.allData = {
      labels: ["Th??ng 1","Th??ng 2","Th??ng 3","Th??ng 4","Th??ng 5","Th??ng 6","Th??ng 7","Th??ng 8","Th??ng 9","Th??ng 10","Th??ng 11","Th??ng 12"],
      datasets: [
        {
          label: "B??S TM ???? ??i c???p nh???t",
          backgroundColor: "#006cdf",
          data: [2, 3, 4, 5, 3, 2, 3, 5, 6, 7, 2, 9],
        },
        {
          label: "B??S TM ???? cho thu??",
          backgroundColor: "#006a9c",
          data: [3, 5, 6, 7, 2, 9, 2, 3, 4, 5, 3, 2],
        },
        {
          label: "Th????ng hi???u ???? h??? tr???",
          backgroundColor: "#bfeaff",
          data: [2, 3, 4, 6, 7, 2, 9, 5, 3, 2, 3, 5],
        },
        {
          label: "Cu???c h???n ???? th???c hi???n",
          backgroundColor: "#80d6ff",
          data: [2, 3, 3, 2, 3, 5, 4, 6, 7, 2, 9, 5],
        },
      ],
    };
    //load bar chart
    this.allOptions = {
      plugins: {
        legend: {
          labels: {
            color: "#1D253B",
          },
        },
      },
      scales: {
        x: {
          ticks: {
            color: "#1D253B",
          },
          grid: {
            color: "rgba(0,0,0,0.1)",
          },
        },
        yAxes: [
          {
            scaleLabel: {
              display: true,
              labelString: "S??? l?????ng b???t ?????ng s???n th????ng m???i",
              fontColor: "#757575",
              fontSize: 12,
            },
            ticks: {
              stepSize: 1,
              beginAtZero: true,
            },
          },
        ],
        y: {
          ticks: {
            color: "#1D253B",
          },
          grid: {
            color: "rgba(0,0,0,0.1)",
          },
        },
      },
    };
  }

  openUpdatedPropertyDialog() {
    this.isShowUpdatedPropertyDialog = true;
  }
  hideUpdatedPropertyDialog() {
    this.isShowUpdatedPropertyDialog = false;
  }

  openRentingPropertyDialog() {
    this.isShowRentingPropertyDialog = true;
  }
  hideRentingPropertyDialog() {
    this.isShowRentingPropertyDialog = false;
  }
  
  openSuportingBrandDialog() {
    this.isShowSuportingBrandDialog = true;
  }
  hideSuportingBrandDialog() {
    this.isShowSuportingBrandDialog = false;
  }

  openAppointmentDialog() {
    this.isShowAppointmentDialog = true;
  }
  hideAppointmentDialog() {
    this.isShowAppointmentDialog = false;
  }
  openTeamDetail() {
    this.reloadService.routingReload("/nhom/danh-sach/chi-tiet", this.broker.team.id)
  }
}
