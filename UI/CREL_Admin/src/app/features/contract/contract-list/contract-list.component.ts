import { Component, OnDestroy, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Contract } from "../contract.model";
import { ContractService } from "../contract.service";
import { Column } from "src/app/core/models/table.model";
import swal from "sweetalert2";
import { Subject } from "rxjs";
import {
  AVATAR_DEFAULT,
  DATE_FORMAT,
  DATE_TIME_FORMAT,
  TABLE_CONFIG,
} from "src/app/shared/constants/common.const";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { BrokerService } from "../../broker/broker.service";
import { switchMap, takeUntil } from "rxjs/operators";
import { OwnerService } from "../../owner/owner.service";

@Component({
  selector: "app-contract-list",
  templateUrl: "./contract-list.component.html",
  styleUrls: ["./contract-list.component.scss"],
})
export class ContractListComponent implements OnInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();
  DATE_FORMAT = DATE_FORMAT;
  TABLE_CONFIG = TABLE_CONFIG;
  AVATAR_DEFAULT = AVATAR_DEFAULT;

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

  isShowSpin: boolean;

  loading: boolean;

  statuses: { label: string; value: number }[];

  selectedStatus: { label: string; value: number };

  //contract
  contract: Contract;

  contracts: Contract[] = [];

  selectedContracts: Contract[] = [];

  index: number;

  constructor(
    private contractServices: ContractService,
    private router: Router,
    private reloadServices: ReloadRouteService,
    private brokerService: BrokerService,
    private ownerService: OwnerService
  ) {
    this.isShowSpin = true;
    this.loading = false;
    this.statuses = [
      { label: "Đã xoá", value: 0 },
      { label: "Cần xác thực", value: 1 },
      { label: "Hoạt động", value: 2 },
    ];
    this.contracts = [];

    this.cols = [
      {
        field: "cbb",
        header: "",
        width: "3rem",
        disabled: true,
        visible: true,
        headerAlign: "center",
        textAlign: "center",
      },
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
        disabled: true,
        visible: true,
        headerAlign: "center",
        textAlign: "center",
      },
      {
        field: "endDate",
        header: "Ngày kết thúc",
        width: "12rem",
        disabled: true,
        visible: true,
        headerAlign: "center",
        textAlign: "center",
      },
      {
        field: "brandName",
        header: "Tên thương hiệu",
        width: "12rem",
        disabled: false,
        visible: true,
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
        field: "status",
        header: "Trạng thái",
        width: "10rem",
        disabled: true,
        visible: true,
        headerAlign: "center",
        textAlign: "center",
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
  }

  ngOnInit() {
    this.getContracts();
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  //data functions
  getContract(contract: Contract): Contract {
    for (let i = 0; i < this.contracts.length; i++) {
      if (contract.id === this.contracts[i].id) {
        return this.contracts[i];
      }
    }
    return;
  }
  getContracts() {
    this.contractServices.getContracts().subscribe((res) => {
      if (res && res.length > 0) {
        res.forEach((contract, index) => {
          this.brokerService
            .getBrokerById(contract.brokerId)
            .pipe(
              takeUntil(this.destroySubs$),
              switchMap((broker) => {
                contract.broker = broker;
                contract.brokerName = contract.broker.name;
                return this.ownerService.getOwnerById(contract.ownerId);
              })
            )
            .subscribe((owner) => {
              contract.owner = owner;
              contract.ownerName = contract.owner.name;
              contract.brandName = contract.brand.name;
              if (res.length - 1 === index) {
                this.contracts = res;
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

  // paging
  next() {
    this.first = this.first + this.TABLE_CONFIG.ROWS;
  }
  prev() {
    this.first = this.first - this.TABLE_CONFIG.ROWS;
  }
  reset() {
    this.first = 0;
  }
  isLastPage(): boolean {
    return this.contracts
      ? this.first === this.contracts.length - this.TABLE_CONFIG.ROWS
      : true;
  }
  isFirstPage(): boolean {
    return this.contracts ? this.first === 0 : true;
  }

  //table functions
  openDetailContract(contract) {
    this.reloadServices.routingNotReload(
      "/thuong-hieu/hop-dong/chi-tiet",
      contract.id
    );
  }
  openEditContract(contract) {
    this.reloadServices.routingNotReload(
      "/thuong-hieu/hop-dong/chinh-sua",
      contract.id
    );
  }
  openNewContract() {
    this.reloadServices.routingNotReload("/thuong-hieu/hop-dong/tao-moi", null);
  }
  openBrandDetail(contract) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return true;
    };
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate(['/thuong-hieu/danh-sach/chi-tiet'], {
      queryParams: { id: contract.brandId, fromContract: true },
      skipLocationChange: true,
    });
  }
}
