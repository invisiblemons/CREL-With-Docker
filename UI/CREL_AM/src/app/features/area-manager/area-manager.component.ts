import { Component, OnInit, OnDestroy, ChangeDetectorRef, SimpleChanges, Input, Output, EventEmitter } from '@angular/core';
import { Subject } from 'rxjs';
import { LocalStorageService } from '../login/local-storage.service';
import { Router } from '@angular/router';
import { AreaManagerService } from './area-manager.service';
import { AreaManager } from './area-manager.model';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';
import { AVATAR_DEFAULT, DATE_FORMAT } from 'src/app/shared/constants/common.const';

@Component({
  selector: 'app-area-manager',
  templateUrl: 'area-manager.component.html',
  styleUrls: ['area-manager.component.scss'],
})
export class AreaManagerComponent implements OnInit, OnDestroy {
  AVATAR_DEFAULT = AVATAR_DEFAULT;
  DATE_FORMAT = DATE_FORMAT;
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  loading: boolean;

  areaManager: AreaManager;

  @Input() isFromTeamDetail;

  constructor(
    private areaManagerServices: AreaManagerService,
    private localStorage: LocalStorageService,
    private ref: ChangeDetectorRef,
    private router: Router,
    private reloadService: ReloadRouteService
  ) {
    this.areaManager;

    this.isShowSkeleton = true;
  }

  ngOnInit(): void {
    if (!this.isFromTeamDetail) {
    this.areaManagerServices
      .getAreaManagerById(this.localStorage.getUserObject().id)
      .subscribe((areaManager) => {
        this.areaManager = areaManager;
        this.isShowSkeleton = false;
        this.ref.detectChanges();
      });
  }
  }


  ngOnChanges(changes: SimpleChanges) {
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

  editProfile() {
    this.reloadService.routingNotReload('/ho-so/chinh-sua', null);
  }

  changePassword() {
    this.reloadService.routingNotReload('/ho-so/doi-mat-khau', null);
  }
}
