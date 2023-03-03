import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { AreaManager } from "../area-manager.model";
import { AreaManagerService } from "../area-manager.service";
import { Column } from "src/app/core/models/table.model";
import swal from "sweetalert2";
import { Subject } from "rxjs";
import { TeamService } from "../../team/team.service";
import { Team } from "../../team/team.model";
import { TABLE_CONFIG } from "src/app/shared/constants/common.const";
import { switchMap, takeUntil } from "rxjs/operators";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";

@Component({
  selector: "app-area-manager-list",
  templateUrl: "./area-manager-list.component.html",
  styleUrls: ["./area-manager-list.component.scss"],
})
export class AreaManagerListComponent implements OnInit, OnDestroy {
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

  //areaManager
  areaManager: AreaManager;

  areaManagers: AreaManager[] = [];
  rawAreaManagers: AreaManager[] = [];

  selectedAreaManagers: AreaManager[] = [];

  index: number;

  //team
  team: Team;

  teams: Team[] = [];

  constructor(
    private areaManagerServices: AreaManagerService,
    private teamServices: TeamService,
    private router: Router,
    private route: ActivatedRoute,
    private reloadServices: ReloadRouteService
  ) {
    this.isShowSpin = true;
    this.loading = false;
    this.statuses = [
      { label: "Đã xoá", value: 0 },
      { label: "Hoạt động", value: 1 },
    ];
    this.areaManagers = [];

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
    this.teamServices
      .getTeams()
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((teams) => {
          this.teams = teams;
          return this.areaManagerServices.getAreaManagers();
        })
      )
      .subscribe((areaManagers) => {
        if (this.teams.length > 0) {
          this.teams.forEach((team, index) => {
            if (team.areaManagerTeams.length > 0) {
              areaManagers.forEach((areaManager) => {
                if (
                  team.areaManagerTeams[team.areaManagerTeams.length - 1]
                    .areaManagerId === areaManager.id
                ) {
                  team.areaManager = areaManager;
                }
              });
              if (this.teams.length - 1 === index) {
                this.teams = this.teams;
                areaManagers.forEach((am) => {
                  if (
                    this.teams.filter(
                      (teamItem) => teamItem.areaManager.id === am.id
                    ).length > 0
                  ) {
                    am.isHasTeam = true;
                  }
                });
                this.areaManagers = areaManagers;
                this.rawAreaManagers = areaManagers;
                this.isShowSpin = false;
              }
            }
          });
        } else {
          this.areaManagers = areaManagers;
          this.rawAreaManagers = areaManagers;
          this.isShowSpin = false;
        }
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  //data functions

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
    return this.areaManagers
      ? this.first === this.areaManagers.length - this.TABLE_CONFIG.ROWS
      : true;
  }
  isFirstPage(): boolean {
    return this.areaManagers ? this.first === 0 : true;
  }

  //table functions
  deleteSelectedAreaManagers() {
    swal
      .fire({
        title: "Bạn có chắc muốn xoá các người quản lý khu vực đã chọn?",
        text: "Những người quản lý khu vực này sẽ bị xoá!",
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
          this.selectedAreaManagers.forEach((areaManager, index) => {
            this.areaManagerServices
              .deleteAreaManager(areaManager)
              .subscribe((res) => {
                if (res) {
                  this.selectedAreaManagers[index] = res;
                }
              });
          });
          //Update table
          this.areaManagers.forEach((areaManagerItem) => {
            this.selectedAreaManagers.forEach((selectedAreaManagerItem) => {
              if (areaManagerItem.id === selectedAreaManagerItem.id) {
                areaManagerItem = selectedAreaManagerItem;
              }
            });
          });
          this.rawAreaManagers = this.areaManagers;
          swal.fire({
            title: "Đã xoá!",
            text: "Người quản lý vực đã bị xoá.",
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
  deleteAreaManager(areaManager) {
    swal
      .fire({
        title: "Bạn có chắc muốn xoá?",
        text: "Người quản lý vực này sẽ bị xoá!",
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
          this.areaManagerServices
            .deleteAreaManager(areaManager)
            .subscribe((res) => {
              if (res) {
                areaManager.status = 0;
                this.rawAreaManagers = this.areaManagers;
                swal.fire({
                  title: "Đã xoá!",
                  text: "Người quản lý vực này đã bị xoá.",
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
  openDetailAreaManager(areaManager) {
    this.reloadServices.routingNotReload(
      "/nhan-su/nguoi-quan-ly-khu-vuc/chi-tiet",
      areaManager.id
    );
  }
  openEditAreaManager(areaManager) {
    this.reloadServices.routingNotReload(
      "/nhan-su/nguoi-quan-ly-khu-vuc/chinh-sua",
      areaManager.id
    );
  }
  openNewAreaManager() {
    this.reloadServices.routingNotReload(
      "/nhan-su/nguoi-quan-ly-khu-vuc/tao-moi",
      null
    );
  }
  filterStatus(value) {
    this.areaManagers = this.rawAreaManagers;
    if (value === 1) {
      this.areaManagers = this.areaManagers.filter(
        (areaManager) => areaManager.status === 1 || areaManager.status === 2
      );
    } else if (value === 0) {
      this.areaManagers = this.areaManagers.filter(
        (areaManager) => areaManager.status === 0
      );
    }
  }
}
