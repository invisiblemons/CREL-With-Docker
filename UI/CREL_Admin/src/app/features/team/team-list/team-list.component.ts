import {
  Component,
  Input,
  OnDestroy,
  OnInit,
  SimpleChanges,
} from "@angular/core";
import { Router } from "@angular/router";
import { Team } from "../team.model";
import { TeamService } from "../team.service";
import { Column } from "src/app/core/models/table.model";
import swal from "sweetalert2";
import { Subject } from "rxjs";
import {
  AVATAR_DEFAULT,
  TABLE_CONFIG,
} from "src/app/shared/constants/common.const";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { AreaManagerService } from "../../area-manager/area-manager.service";
import { switchMap, takeUntil } from "rxjs/operators";
import { AreaManager } from "../../area-manager/area-manager.model";
import { BrokerService } from "../../broker/broker.service";
import { Broker } from "../../broker/broker.model";

@Component({
  selector: "app-team-list",
  templateUrl: "./team-list.component.html",
  styleUrls: ["./team-list.component.scss"],
})
export class TeamListComponent implements OnInit, OnDestroy {
  AVATAR_DEFAULT = AVATAR_DEFAULT;
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  TABLE_CONFIG = TABLE_CONFIG;

  tableTitle: string;

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

  //team
  team: Team;

  teams: Team[] = [];

  selectedTeams: Team[] = [];

  index: number;

  @Input() fromAreaManager;

  areaManagers: AreaManager[];

  brokers: Broker[];

  constructor(
    private teamServices: TeamService,
    private router: Router,
    private reloadServices: ReloadRouteService,
    private areaManagerServices: AreaManagerService,
    private brokerService: BrokerService
  ) {
    this.isShowSpin = true;
    this.loading = false;
    this.statuses = [
      { label: "Kh??ng ho???t ?????ng", value: 0 },
      { label: "Ho???t ?????ng", value: 1 },
    ];
    this.teams = [];

    this.tableTitle = "Qu???n l?? nh??m";
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
        header: "M??",
        width: "5rem",
        disabled: true,
        visible: true,
        headerAlign: "center",
        textAlign: "center",
      },
      {
        field: "name",
        header: "T??n nh??m",
        width: "12rem",
        disabled: true,
        visible: true,
        headerAlign: "left",
        textAlign: "left",
      },
      {
        field: "status",
        header: "Tr???ng th??i",
        width: "10rem",
        disabled: true,
        visible: true,
        headerAlign: "center",
        textAlign: "center",
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
  }

  ngOnInit() {
    this.getTeams();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes["fromAreaManager"] && changes["fromAreaManager"].currentValue) {
      this.fromAreaManager = changes["fromAreaManager"].currentValue;
      this.tableTitle = "Qu???n l?? c??c nh??m";
      let teamsTemp = [];
      this.teams.forEach((team) => {
        if (team.areaManagerTeams.length > 0) {
          if (
            team.areaManagerTeams[team.areaManagerTeams.length - 1]
              .areaManagerId === changes["fromAreaManager"].currentValue.id
          ) {
            teamsTemp.push(team);
          }
        }
      });
      this.teams = teamsTemp;
    }
  }
  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  //data functions
  getTeam(team: Team): Team {
    for (let i = 0; i < this.teams.length; i++) {
      if (team.id === this.teams[i].id) {
        return this.teams[i];
      }
    }
    return;
  }
  getTeams() {
    this.brokerService
      .getBrokers()
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((brokers) => {
          this.brokers = brokers;
          return this.areaManagerServices.getAreaManagers();
        }),
        switchMap((ams) => {
          this.areaManagers = ams;
          return this.teamServices.getTeams();
        })
      )
      .subscribe((res) => {
        res.forEach((team, index) => {
          if (this.brokers.length > 0) {
            if (this.brokers.filter((broker) => broker.teamId === team.id)) {
              team.isHasBroker = true;
            }
          }

          if (team.areaManagerTeams.length > 0) {
            this.areaManagers.forEach((am) => {
              if (
                team.areaManagerTeams[team.areaManagerTeams.length - 1]
                  .areaManagerId === am.id
              ) {
                team.areaManager = am;
              }
            });
          } else {
            team.areaManager = null;
          }
          if (res.length - 1 === index) {
            this.teams = res;
            this.isShowSpin = false;
          }
        });
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
    return this.teams
      ? this.first === this.teams.length - this.TABLE_CONFIG.ROWS
      : true;
  }
  isFirstPage(): boolean {
    return this.teams ? this.first === 0 : true;
  }

  //table functions
  deleteSelectedTeams() {
    swal
      .fire({
        title: "B???n c?? ch???c c??c nh??m ???? ch???n kh??ng c??n ho???t ?????ng?",
        text: "Nh???ng nh??m n??y s??? b??? ????nh d???u kh??ng ho???t ?????ng!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "C??!",
        cancelButtonText: "Kh??ng",
        customClass: {
          confirmButton: "btn btn-danger animation-on-hover mr-1",
          cancelButton: "btn btn-default animation-on-hover",
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          this.selectedTeams.forEach((team, index) => {
            this.teamServices.deleteTeam(team).subscribe((res) => {
              if (res) {
                this.selectedTeams[index] = res;
              }
            });
          });
          //Update table
          this.teams.forEach((teamItem) => {
            this.selectedTeams.forEach((selectedTeamItem) => {
              if (teamItem.id === selectedTeamItem.id) {
                teamItem = selectedTeamItem;
              }
            });
          });
          swal.fire({
            title: "Th??nh c??ng!",
            text: "C??c nh??m ???????c ch???n ???? ????nh d???u kh??ng ho???t ?????ng.",
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
  deleteTeam(team) {
    swal
      .fire({
        title: "B???n c?? ch???c nh??m n??y kh??ng c??n ho???t ?????ng?",
        text: "Nh??m n??y s??? b??? ????nh d???u kh??ng ho???t ?????ng!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "C??!",
        cancelButtonText: "Kh??ng",
        customClass: {
          confirmButton: "btn btn-danger animation-on-hover mr-1",
          cancelButton: "btn btn-default animation-on-hover",
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          this.teamServices.deleteTeam(team).subscribe((res) => {
            if (res) {
              team.status = 0;
              swal.fire({
                title: "Th??nh c??ng!",
                text: "???? ????nh d???u kh??ng ho???t ?????ng.",
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
  openDetailTeam(team) {
    this.reloadServices.routingNotReload("/nhan-su/nhom/chi-tiet", team.id);
  }
  openEditTeam(team) {
    this.reloadServices.routingNotReload("/nhan-su/nhom/chinh-sua", team.id);
  }
  openNewTeam() {
    this.reloadServices.routingNotReload("/nhan-su/nhom/tao-moi", null);
  }

  openAreaManagerDetail(team) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return true;
    };
    this.router.onSameUrlNavigation = "reload";
    this.router.navigate(["/nhan-su/nguoi-quan-ly-khu-vuc/chi-tiet"], {
      queryParams: { id: team.areaManager.id, fromTeam: true },
      skipLocationChange: true,
    });
  }

  openNewTeamOfAM() {
    this.reloadServices.routingNotReload(
      "/nhan-su/nhom/tao-moi",
      this.fromAreaManager.id
    );
  }

  openDetailTeamFromAM(am) {
    this.reloadServices.routingNotReload("/nhan-su/nhom/chi-tiet", am.id);
  }
}
