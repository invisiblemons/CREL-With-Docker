import {
  Component,
  OnInit,
  ChangeDetectionStrategy,
  OnDestroy,
  ChangeDetectorRef,
} from "@angular/core";
import { Subject } from "rxjs";
import { LocalStorageService } from "../login/local-storage.service";
import { Admin } from "./admin.model";
import { AdminService } from "./admin.service";
import { Router } from "@angular/router";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { DATE_FORMAT } from "src/app/shared/constants/common.const";

@Component({
  selector: "app-admin",
  templateUrl: "./admin.component.html",
  styleUrls: ["./admin.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AdminComponent implements OnInit, OnDestroy {
  DATE_FORMAT = DATE_FORMAT;
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  loading: boolean;

  admin: Admin;

  constructor(
    private adminServices: AdminService,
    private localStorage: LocalStorageService,
    private ref: ChangeDetectorRef,
    private router: Router,
    private reloadService: ReloadRouteService,
  ) {
    this.isShowSkeleton = true;
  }

  ngOnInit(): void {
    this.adminServices
      .getAdminById(this.localStorage.getUserObject().id)
      .subscribe((admin) => {
        this.admin = admin;
        this.isShowSkeleton = false;
        this.ref.detectChanges();
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  editProfile() {
    this.reloadService.routingNotReload('/ho-so/chinh-sua', null);
  }

  changePassword() {
    this.reloadService.routingNotReload('/ho-so/doi-mat-khau', null);
  }
}
