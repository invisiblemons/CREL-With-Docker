import { Component, OnDestroy, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Industry } from "../industry.model";
import { Column } from "src/app/core/models/table.model";
import swal from "sweetalert2";
import { Subject } from "rxjs";
import { TABLE_CONFIG } from "src/app/shared/constants/common.const";
import { IndustryService } from "../industry.service";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";

@Component({
  selector: "app-industry-list",
  templateUrl: "./industry-list.component.html",
  styleUrls: ["./industry-list.component.scss"],
})
export class IndustryListComponent implements OnInit, OnDestroy {
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

  selectedStatus: { label: string; value: number };

  //industry
  industry: Industry;

  industries: Industry[] = [];

  selectedIndustries: Industry[] = [];

  index: number;

  constructor(
    private industryServices: IndustryService,
    private router: Router,
    private reloadServices: ReloadRouteService
  ) {
    this.isShowSpin = true;
    this.loading = false;
    this.statuses = [
      { label: "Đã xoá", value: 0 },
      { label: "Hoạt động", value: 1 },
    ];
    this.industries = [];

    this.cols = [
      {
        field: "cbb",
        header: "",
        width: "3rem",
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
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
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: "status",
        header: "Trạng thái",
        width: "10rem",
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: "action",
        header: "Thao tác",
        width: "15rem",
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
    ];
    this.displayCols = this.cols.filter((element) => !element.disabled);
    this._selectedColumns = this.cols.filter((element) => element.visible);
  }

  ngOnInit() {
    this.getIndustries();
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  //data functions
  getIndustry(industry: Industry): Industry {
    for (let i = 0; i < this.industries.length; i++) {
      if (industry.id === this.industries[i].id) {
        return this.industries[i];
      }
    }
    return;
  }
  getIndustries() {
    this.industryServices.getIndustries().subscribe((res) => {
      this.isShowSpin = false;
      this.industries = res;
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
    return this.industries
      ? this.first === this.industries.length - this.TABLE_CONFIG.ROWS
      : true;
  }
  isFirstPage(): boolean {
    return this.industries ? this.first === 0 : true;
  }

  //table functions
  deleteSelectedIndustries() {
    swal
      .fire({
        title: "Bạn có chắc muốn xoá các ngành kinh doanh đã chọn?",
        text: "Những ngành kinh doanh này sẽ bị xoá!",
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
          this.selectedIndustries.forEach((industry, index) => {
            this.industryServices.deleteIndustry(industry).subscribe((res) => {
              if (res) {
                this.selectedIndustries[index] = res;
              }
            });
          });
          //Update table
          this.industries.forEach((industryItem) => {
            this.selectedIndustries.forEach((selectedIndustryItem) => {
              if (industryItem.id === selectedIndustryItem.id) {
                industryItem = selectedIndustryItem;
              }
            });
          });
          swal.fire({
            title: "Xoá thành công!",
            text: "Ngành kinh doanh đã xoá.",
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
  deleteIndustry(industry) {
    swal
      .fire({
        title: "Bạn có chắc muốn xoá?",
        text: "'Ngành kinh doanh '" + industry.name + "' sẽ bị xoá!'",
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
          this.industryServices.deleteIndustry(industry).subscribe((res) => {
            if (res) {
              industry.status = 0;
              swal.fire({
                title: "Xoá thành công!",
                text: "'Ngành kinh doanh '" + industry.name + "' đã bị xoá!'",
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
  openDetailIndustry(industry) {
    this.reloadServices.routingNotReload(
      '/thuong-hieu/nganh/chi-tiet',
      industry.id
    );
  }
  openEditIndustry(industry) {
    this.reloadServices.routingNotReload(
      '/thuong-hieu/nganh/chinh-sua',
      industry.id
    );
  }
  openNewIndustry() {
    this.reloadServices.routingNotReload(
      '/thuong-hieu/nganh/tao-moi',
      null
    );
  }
}
