import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subject } from "rxjs";
import { finalize, switchMap, takeUntil } from "rxjs/operators";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { Owner } from "../../owner/owner.model";
import { OwnerService } from "../../owner/owner.service";
import { Contract } from "../contract.model";

@Component({
  selector: "app-owner-detail",
  templateUrl: "./owner-detail.component.html",
  styleUrls: ["./owner-detail.component.scss"],
})
export class OwnerDetailComponent implements OnInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  statuses: { label: string; value: number }[];

  ownerId: string;

  owner: Owner;

  @Output() statusOwnerDialog = new EventEmitter();
  @Input() contract: Contract;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private ownerServices: OwnerService,
    private reloadService: ReloadRouteService
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.statuses = [
      { label: "Đã xoá", value: 0 },
      { label: "Hoạt động", value: 1 },
    ];
  }

  ngOnInit(): void {
    this.ownerServices
      .getOwnerById(this.contract.ownerId)
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
    this.statusOwnerDialog.emit();
  }
}
