import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subject } from "rxjs";
import { finalize, switchMap, takeUntil } from "rxjs/operators";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { Industry } from "../industry.model";
import { IndustryService } from "../industry.service";

@Component({
  selector: "app-industry-detail",
  templateUrl: "./industry-detail.component.html",
  styleUrls: ["./industry-detail.component.scss"],
})
export class IndustryDetailComponent implements OnInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  statuses: { label: string; value: number }[];

  industryId: string;

  industry: Industry;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private industryServices: IndustryService,
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
          return this.industryServices.getIndustryById(params["id"]);
        }),
        finalize(() => (this.isShowSkeleton = false))
      )
      .subscribe((industry) => {
        this.industry = industry;
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingNotReload('/thuong-hieu/nganh', null);
  }
}
