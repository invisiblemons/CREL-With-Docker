import { Component, OnInit } from "@angular/core";
import moment from "moment";
import { Subject } from "rxjs";
import { switchMap, takeUntil } from "rxjs/operators";
import { Column } from "src/app/core/models/table.model";
import {
  DATE_FORMAT,
  TABLE_CONFIG,
} from "src/app/shared/constants/common.const";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { BrokerService } from "../../broker/broker.service";
import { Contract } from "../../contract/contract.model";
import { ContractService } from "../../contract/contract.service";
import { OwnerService } from "../../owner/owner.service";

@Component({
  selector: "app-contract-coming",
  templateUrl: "./contract-coming.component.html",
  styleUrls: ["./contract-coming.component.scss"],
})
export class ContractComingComponent implements OnInit {
  destroySubs$: Subject<boolean> = new Subject<boolean>();
  DATE_FORMAT = DATE_FORMAT;
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

  isShowSpin: boolean = true;

  contracts: Contract[] = [];

  maxDate: Date = new Date();

  constructor(
    private reloadRouteService: ReloadRouteService,
    private contractServices: ContractService,
    private brokerService: BrokerService,
    private ownerService: OwnerService
  ) {
    this.cols = [
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
        field: "startDate",
        header: "Ngày bắt đầu",
        width: "12rem",
        disabled: false,
        visible: false,
        headerAlign: "center",
        textAlign: "center",
      },
      {
        field: "endDate",
        header: "Ngày kết thúc",
        width: "12rem",
        disabled: false,
        visible: true,
        headerAlign: "center",
        textAlign: "center",
      },
      {
        field: "brandName",
        header: "Tên thương hiệu",
        width: "12rem",
        disabled: false,
        visible: false,
        headerAlign: "left",
        textAlign: "left",
      },
      {
        field: "ownerName",
        header: "Người sở hữu BĐS",
        width: "12rem",
        disabled: false,
        visible: false,
        headerAlign: "left",
        textAlign: "left",
      },
      {
        field: "brokerName",
        header: "Nhân viên môi giới hỗ trợ",
        width: "12rem",
        disabled: false,
        visible: false,
        headerAlign: "left",
        textAlign: "left",
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
    this.maxDate = new Date(this.maxDate.setMonth(this.maxDate.getMonth() + 1));
  }

  ngOnInit(): void {
    this.contractServices
      .getMaxEndDateContracts(
        moment(this.maxDate).format("YYYY-MM-DDThh:mm:ss") + "Z"
      )
      .subscribe((contracts: Contract[]) => {
        if (contracts && contracts.length > 0) {
          contracts.forEach((childContract, childIndex) => {
            this.brokerService
              .getBrokerById(childContract.brokerId)
              .pipe(
                takeUntil(this.destroySubs$),
                switchMap((broker) => {
                  childContract.broker = broker;
                  childContract.brokerName = childContract.broker.name;
                  return this.ownerService.getOwnerById(childContract.ownerId);
                })
              )
              .subscribe((owner) => {
                childContract.owner = owner;
                childContract.ownerName = childContract.owner.name;
                childContract.brandName = childContract.brand.name;
                if (childIndex === contracts.length - 1) {
                  this.contracts = contracts;
                  this.isShowSpin = false;
                }
              });
          });
        } else {
          this.contracts = [];
          this.isShowSpin = false;
        }
      });
  }

  openDetailContract(contract) {
    this.reloadRouteService.routingReload(
      '/thuong-hieu/hop-dong/chi-tiet',
      contract.id
    );
  }
}
