import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subject } from "rxjs";
import { finalize, switchMap, takeUntil } from "rxjs/operators";
import { LocationService } from "src/app/shared/services/location.service";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { Project } from "../project.model";
import { ProjectService } from "../project.service";

@Component({
  selector: "app-project-detail",
  templateUrl: "./project-detail.component.html",
  styleUrls: ["./project-detail.component.scss"],
})
export class ProjectDetailComponent implements OnInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  statuses: { label: string; value: number }[];

  projectId: string;

  project: Project;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private projectServices: ProjectService,
    private locationServices: LocationService,
    private reloadService: ReloadRouteService,
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
          return this.projectServices.getProjectById(params["id"]);
        }),
        switchMap((project) => {
          this.project = project;
          return this.locationServices.getDistrictById(project.districtId);
        })
      )
      .subscribe((district) => {
        this.project.districtName = district.name;
        this.project.handoverYear = new Date(this.project.handoverYear);
        this.isShowSkeleton = false;
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingNotReload('/bat-dong-san/du-an', null);
  }
}
