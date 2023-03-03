import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subject } from "rxjs";
import { finalize, switchMap, takeUntil } from "rxjs/operators";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { Industry } from "../../industry/industry.model";
import { IndustryService } from "../../industry/industry.service";
import { Store } from "../store.model";
import { StoreService } from "../store.service";

@Component({
  selector: "app-store-detail",
  templateUrl: "./store-detail.component.html",
  styleUrls: ["./store-detail.component.scss"],
})
export class StoreDetailComponent implements OnInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  statuses: { label: string; value: number }[];

  storeId: string;

  store: Store;

  industry: Industry;

  industries: Industry[] = [];

  constructor(
    private storeServices: StoreService,
    private route: ActivatedRoute,
    private router: Router,
    private industryServices: IndustryService,
    private reloadServices: ReloadRouteService
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.statuses = [
      { label: "Đã xoá", value: 0 },
      { label: "Hoạt động", value: 1 },
      { label: "Đang được thuê", value: 2 },
      { label: "Quá hạn thuê", value: 3 },
    ];
  }

  ngOnInit(): void {
    this.route.queryParams
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((params) => {
          return this.storeServices.getStoreById(params["id"]);
        }),
        finalize(() => (this.isShowSkeleton = false))
      )
      .subscribe((store) => {
        this.store = store;
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadServices.routingNotReload('/thuong-hieu/cua-hang', null);
  }
}
