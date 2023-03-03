import { Component, OnDestroy, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Owner } from "../owner.model";
import { OwnerService } from "../owner.service";
import { Column } from "src/app/core/models/table.model";
import swal from "sweetalert2";
import { Subject } from "rxjs";
import { TABLE_CONFIG } from "src/app/shared/constants/common.const";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";

@Component({
  selector: "app-owner-list",
  templateUrl: "./owner-list.component.html",
  styleUrls: ["./owner-list.component.scss"],
})
export class OwnerListComponent implements OnInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

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

  isShowSpin: boolean;

  loading: boolean;

  statuses: { label: string; value: number }[];

  //owner
  owner: Owner;

  owners: Owner[] = [];

  selectedOwners: Owner[] = [];

  index: number;

  constructor(
    private ownerServices: OwnerService,
    private router: Router,
    private reloadServices: ReloadRouteService
  ) {
    this.isShowSpin = true;
    this.loading = false;
    this.statuses = [
      { label: "Đã xoá", value: 0 },
      { label: "Hoạt động", value: 1 },
    ];

    this.owners = [];

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
        field: "name",
        header: "Tên",
        width: "12rem",
        disabled: true,
        visible: true,
        headerAlign: "left",
        textAlign: "left",
      },
      {
        field: "email",
        header: "Địa chỉ email",
        width: "15rem",
        disabled: false,
        visible: false,
        headerAlign: "left",
        textAlign: "left",
      },
      {
        field: "phoneNumber",
        header: "Số điện thoại",
        width: "12rem",
        disabled: false,
        visible: true,
        headerAlign: "right",
        textAlign: "right",
      },
      {
        field: "gender",
        header: "Giới tính",
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
    this.getOwners();
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  //data functions
  getOwner(owner: Owner): Owner {
    for (let i = 0; i < this.owners.length; i++) {
      if (owner.id === this.owners[i].id) {
        return this.owners[i];
      }
    }
    return;
  }
  getOwners() {
    this.ownerServices.getOwners().subscribe((res) => {
      this.isShowSpin = false;
      this.owners = res;
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
    return this.owners
      ? this.first === this.owners.length - this.TABLE_CONFIG.ROWS
      : true;
  }
  isFirstPage(): boolean {
    return this.owners ? this.first === 0 : true;
  }

  //table functions
  deleteSelectedOwners() {
    swal
      .fire({
        title: "Bạn có chắc muốn xoá các thông tin người sở hữu đã chọn?",
        text: "Những thông tin người sở hữu này sẽ bị xoá!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Có, xoá nó!",
        cancelButtonText: "Không, giữ nguyên",
        customClass: {
          confirmButton: "btn btn-danger animation-on-hover mr-1",
          cancelButton: "btn btn-default animation-on-hover",
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          this.selectedOwners.forEach((owner, index) => {
            this.ownerServices.deleteOwner(owner).subscribe((res) => {
              if (res) {
                this.selectedOwners[index] = res;
              }
            });
          });
          //Update table
          this.owners.forEach((ownerItem) => {
            this.selectedOwners.forEach((selectedOwnerItem) => {
              if (ownerItem.id === selectedOwnerItem.id) {
                ownerItem = selectedOwnerItem;
              }
            });
          });
          swal.fire({
            title: "Xoá thành công!",
            text: "Các thông tin người sở hữu được chọn đã bị xoá.",
            icon: "success",
            customClass: {
              confirmButton: "btn btn-success animation-on-hover",
            },
            buttonsStyling: false,
            timer: 2000,
          });
        }
      });
  }
  deleteOwner(owner) {
    swal
      .fire({
        title: "Bạn có chắc muốn xoá?",
        text: "'Nhân viên môi giới'" + owner.name + "' sẽ bị xoá!!'",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Có, xoá nó!",
        cancelButtonText: "Không, giữ nguyên",
        customClass: {
          confirmButton: "btn btn-danger animation-on-hover mr-1",
          cancelButton: "btn btn-default animation-on-hover",
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          this.ownerServices.deleteOwner(owner).subscribe((res) => {
            if (res) {
              owner.status = 0;
              swal.fire({
                title: "Xoá thành công!",
                text: "Người sở hữu " + owner.name + " đã bị xoá.",
                icon: "success",
                customClass: {
                  confirmButton: "btn btn-success animation-on-hover",
                },
                buttonsStyling: false,
                timer: 2000,
              });
            }
          });
        }
      });
  }
  openDetailOwner(owner) {
    this.reloadServices.routingNotReload(
      "/bat-dong-san/nguoi-so-huu/chi-tiet",
      owner.id
    );
  }
  openEditOwner(owner) {
    this.reloadServices.routingNotReload(
      "/bat-dong-san/nguoi-so-huu/chinh-sua",
      owner.id
    );
  }
  openNewOwner() {
    this.reloadServices.routingNotReload(
      "/bat-dong-san/nguoi-so-huu/tao-moi",
      null
    );
  }
}
