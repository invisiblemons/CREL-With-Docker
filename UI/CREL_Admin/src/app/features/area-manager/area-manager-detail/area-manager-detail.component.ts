import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  SimpleChanges,
} from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subject } from "rxjs";
import { finalize, switchMap, takeUntil } from "rxjs/operators";
import { DATE_FORMAT } from "src/app/shared/constants/common.const";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { Team } from "../../team/team.model";
import { TeamService } from "../../team/team.service";
import { AreaManager } from "../area-manager.model";
import { AreaManagerService } from "../area-manager.service";
import swal from "sweetalert2";

@Component({
  selector: "app-area-manager-detail",
  templateUrl: "./area-manager-detail.component.html",
  styleUrls: ["./area-manager-detail.component.scss"],
})
export class AreaManagerDetailComponent implements OnInit, OnDestroy {
  DATE_FORMAT = DATE_FORMAT;
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  statuses: { label: string; value: number }[];

  areaManagerId: string;

  areaManager: AreaManager;

  team: Team;

  teams: Team[] = [];

  @Input() isFromTeamDetail;

  @Input() isFromTeam;

  @Output() amState = new EventEmitter();

  constructor(
    private areaManagerServices: AreaManagerService,
    private route: ActivatedRoute,
    private router: Router,
    private teamServices: TeamService,
    private reloadRouteServices: ReloadRouteService
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
  }

  ngOnInit(): void {
    if (!this.isFromTeamDetail) {
      this.route.queryParams
        .pipe(
          takeUntil(this.destroySubs$),
          switchMap((params) => {
            return this.areaManagerServices.getAreaManagerById(params["id"]);
          }),
          switchMap((areaManager) => {
            this.areaManager = areaManager;
            return this.teamServices.getTeams();
          })
        )
        .subscribe((teams) => {
          this.teams = teams;
          this.isShowSkeleton = false;
        });
    }
  }

  ngOnChanges(changes: SimpleChanges) {
    if (this.isFromTeam) {
      this.reloadRouteServices.routingNotReload(
        '/nhan-su/nhom',
        null
      );
    }
    if (
      changes["isFromTeamDetail"] &&
      changes["isFromTeamDetail"].currentValue
    ) {
      this.areaManagerServices
        .getAreaManagerById(changes["isFromTeamDetail"].currentValue)
        .subscribe((areaManager) => {
          this.areaManager = areaManager;
          this.isShowSkeleton = false;
        });
    }
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    if (this.isFromTeamDetail) {
      this.amState.emit();
    } else {
      this.reloadRouteServices.routingNotReload("/nhan-su/nguoi-quan-ly-khu-vuc", null);
    }
  }

  //data functions
  getTeam(areaManager: AreaManager): Team {
    for (let i = 0; i < this.teams.length; i++) {
      if (
        this.teams[i].areaManagerTeams[
          this.teams[i].areaManagerTeams.length - 1
        ].areaManagerId === areaManager.id
      ) {
        return this.teams[i];
      }
    }
    return;
  }

  
  resetPassword() {
    this.areaManagerServices.resetPassword(this.areaManager.email).subscribe((res) => {
      swal.fire({
        title: "Thành công!",
        text: "Đổi mật khẩu thành công. Kiểm tra địa chỉ email để lấy mật khẩu mới.",
        icon: "success",
        customClass: {
          confirmButton: "btn btn-success animation-on-hover",
        },
        buttonsStyling: false,
        timer: 2000,
      });
    });
  }
}
