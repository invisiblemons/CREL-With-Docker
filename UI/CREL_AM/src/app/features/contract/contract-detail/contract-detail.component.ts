import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subject } from "rxjs";
import { finalize, switchMap, takeUntil } from "rxjs/operators";
import {
  DATE_FORMAT,
  DATE_TIME_FORMAT,
  IMAGE_DEFAULT,
} from "src/app/shared/constants/common.const";
import { Media } from "src/app/shared/models/media.model";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { BrokerService } from "../../broker/broker.service";
import { OwnerService } from "../../owner/owner.service";
import { Contract } from "../contract.model";
import { ContractService } from "../contract.service";
import * as FileSaver from "file-saver";

@Component({
  selector: "app-contract-detail",
  templateUrl: "./contract-detail.component.html",
  styleUrls: ["./contract-detail.component.scss"],
})
export class ContractDetailComponent implements OnInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  IMAGE_DEFAULT = [{ link: IMAGE_DEFAULT }];

  DATE_FORMAT = DATE_FORMAT;

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  statuses: { label: string; value: number }[];

  contractId: string;

  contract: Contract;

  isShowBroker = false;

  isShowProperty = false;

  isShowBrand = false;

  isShowOwner = false;

  media: Media[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private contractServices: ContractService,
    private reloadService: ReloadRouteService,
    private brokerService: BrokerService,
    private ownerService: OwnerService
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
          return this.contractServices.getContractById(params["id"]);
        }),
        switchMap((contract) => {
          this.contract = contract;
          this.media = this.contract.media;
          return this.brokerService.getBrokerById(contract.brokerId);
        }),
        switchMap((broker) => {
          this.contract.broker = broker;
          return this.ownerService.getOwnerById(this.contract.ownerId);
        })
      )
      .subscribe((owner) => {
        this.contract.owner = owner;
        this.isShowSkeleton = false;
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingNotReload("/thuong-hieu/hop-dong", null);
  }

  openBrokerDetail() {
    this.isShowBroker = true;
  }

  statusBrokerDialog() {
    this.isShowBroker = false;
  }

  openBrandDetail() {
    this.isShowBrand = true;
  }

  statusBrandDialog() {
    this.isShowBrand = false;
  }

  openOwnerDetail() {
    this.isShowOwner = true;
  }

  statusOwnerDialog() {
    this.isShowOwner = false;
  }

  openPropertyDetail() {
    this.isShowProperty = true;
  }

  statusPropertyDialog() {
    this.isShowProperty = false;
  }

  downTemplate() {
    this.contractServices
      .downFile(this.contract.id)
      .subscribe((response: any) => {
        const data: Blob = new Blob([response]);
        FileSaver.saveAs(data, "hop-dong.doc");
      });
  }
}
