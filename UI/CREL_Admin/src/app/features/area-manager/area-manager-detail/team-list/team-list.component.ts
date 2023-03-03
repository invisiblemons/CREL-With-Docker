import {
  Component,
  Input,
  OnDestroy,
  OnInit,
  SimpleChanges,
} from "@angular/core";
import { Router } from "@angular/router";
import { Column } from "src/app/core/models/table.model";
import swal from "sweetalert2";
import { Subject } from "rxjs";
import {
  AVATAR_DEFAULT,
  TABLE_CONFIG,
} from "src/app/shared/constants/common.const";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { Team } from "src/app/features/team/team.model";
import { TeamService } from "src/app/features/team/team.service";
import { AreaManagerService } from "../../area-manager.service";

@Component({
  selector: "app-team-list-in-am",
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

  rawTeams: Team[] = [];

  selectedTeams: Team[] = [];

  index: number;

  @Input() fromAreaManager;

  constructor(
    private teamServices: TeamService,
    private router: Router,
    private reloadServices: ReloadRouteService,
    private areaManagerServices: AreaManagerService
  ) {
    this.isShowSpin = true;
    this.loading = false;
    this.statuses = [
      { label: "Đã xoá", value: 0 },
      { label: "Hoạt động", value: 1 },
    ];
    this.teams = [];

    this.tableTitle = "Quản lý nhóm";
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
        header: "Tên nhóm",
        width: "12rem",
        disabled: true,
        visible: true,
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
  }

  ngOnInit() {
    this.getTeams();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes["fromAreaManager"] && changes["fromAreaManager"].currentValue) {
      this.teamServices.getTeams().subscribe((res) => {
        res.forEach((team, index) => {
          if (team.areaManagerTeams.length > 0) {
            this.areaManagerServices
              .getAreaManagerById(
                team.areaManagerTeams[team.areaManagerTeams.length - 1]
                  .areaManagerId
              )
              .subscribe((am) => {
                team.areaManager = am;
                if (res.length - 1 === index) {
                  this.rawTeams = res;
                  this.fromAreaManager =
                    changes["fromAreaManager"].currentValue;
                  this.tableTitle = "Quản lý các nhóm";
                  let teamsTemp = [];
                  this.rawTeams.forEach((teamRoot) => {
                    if (teamRoot.areaManagerTeams.length > 0) {
                      if (
                        teamRoot.areaManagerTeams[
                          teamRoot.areaManagerTeams.length - 1
                        ].areaManagerId === this.fromAreaManager.id
                      ) {
                        if (teamRoot.status > 0) {
                          teamsTemp.push(teamRoot);
                        }
                      }
                    }
                  });
                  this.teams = teamsTemp;
                  this.isShowSpin = false;
                }
              });
          } else {
            team.areaManager = [];
          }
        });
      });
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
    this.teamServices.getTeams().subscribe((res) => {
      res.forEach((team, index) => {
        if (team.areaManagerTeams.length > 0) {
          this.areaManagerServices
            .getAreaManagerById(
              team.areaManagerTeams[team.areaManagerTeams.length - 1]
                .areaManagerId
            )
            .subscribe((am) => {
              team.areaManager = am;
              if (res.length - 1 === index) {
                this.rawTeams = res;
                this.isShowSpin = false;
              }
            });
        } else {
          team.areaManager = [];
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
        title: "Bạn có chắc muốn xoá các nhóm đã chọn?",
        text: "Những nhóm này sẽ bị xoá!",
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
            title: "Đã xoá!",
            text: "Nhóm đã xoá.",
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
        title: "Bạn có chắc muốn xoá?",
        text: "Nhóm này sẽ bị xoá!",
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
          this.teamServices.deleteTeam(team).subscribe((res) => {
            if (res) {
              team.status = 0;
              swal.fire({
                title: "Đã xoá!",
                text: "Nhóm đã xoá.",
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
    this.reloadServices.routingNotReload(
      "/nhan-su/nhom/tao-moi",
      this.fromAreaManager.id
    );
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
    this.reloadServices.routingReload(
      "/nhan-su/nhom/tao-moi",
      this.fromAreaManager.id
    );
  }

  openDetailTeamFromAM(team) {
    this.reloadServices.routingReload("/nhan-su/nhom/chi-tiet", team.id);
  }
}
