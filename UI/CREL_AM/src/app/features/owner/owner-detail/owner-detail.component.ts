import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subject } from "rxjs";
import { finalize, switchMap, takeUntil } from "rxjs/operators";
import { DATE_FORMAT } from "src/app/shared/constants/common.const";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { Owner } from "../owner.model";
import { OwnerService } from "../owner.service";

@Component({
  selector: "app-owner-detail",
  templateUrl: "./owner-detail.component.html",
  styleUrls: ["./owner-detail.component.scss"],
})
export class OwnerDetailComponent implements OnInit, OnDestroy {
  DATE_FORMAT = DATE_FORMAT;
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  statuses: { label: string; value: number }[];

  ownerId: string;

  owner: Owner;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private ownerServices: OwnerService,
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
          return this.ownerServices.getOwnerById(params["id"]);
        })
      )
      .subscribe((owner) => {
        this.owner = owner;
        this.isShowSkeleton = false;
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingNotReload('/bat-dong-san/nguoi-so-huu', null);
  }
}
