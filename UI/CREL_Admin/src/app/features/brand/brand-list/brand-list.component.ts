import {
  ChangeDetectorRef,
  Component,
  Input,
  OnDestroy,
  OnInit,
  SimpleChanges,
} from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Brand } from "../brand.model";
import { BrandService } from "../brand.service";
import { Column } from "src/app/core/models/table.model";
import swal from "sweetalert2";
import { Subject } from "rxjs";
import { IndustryService } from "../../industry/industry.service";
import { Industry } from "../../industry/industry.model";
import { TABLE_CONFIG } from "src/app/shared/constants/common.const";
import { finalize, switchMap, takeUntil } from "rxjs/operators";
import { Broker } from "../../broker/broker.model";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";

@Component({
  selector: "app-brand-list",
  templateUrl: "./brand-list.component.html",
  styleUrls: ["./brand-list.component.scss"],
})
export class BrandListComponent implements OnInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  // Table Fields
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
  tableTitle: string;
  isShowTableAction: boolean;

  // Handle Input
  @Input() brokerFromBrokerDetail: Broker;

  statuses: { label: string; value: number }[];

  selectedStatus: { label: string; value: number };

  //brand
  brand: Brand;

  brands: Brand[] = [];

  selectedBrands: Brand[] = [];

  index: number;

  //industry
  industry: Industry;

  industries: Industry[] = [];

  constructor(
    private brandServices: BrandService,
    private industryServices: IndustryService,
    private router: Router,
    private route: ActivatedRoute,
    private ref: ChangeDetectorRef,
    private reloadServices: ReloadRouteService
  ) {
    this.isShowSpin = true;
    this.loading = false;
    this.tableTitle = "Quản lý thương hiệu kinh doanh";
    this.isShowTableAction = true;

    this.statuses = [
      { label: "Đã xoá", value: 0 },
      { label: "Đợi xét duyệt", value: 1 },
      { label: "Được duyệt", value: 2 },
      { label: "Không được duyệt", value: 3 },
    ];
    this.brands = [];

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
        header: "Tên thương hiệu",
        width: "12rem",
        disabled: true,
        visible: true,
        headerAlign: "left",
        textAlign: "left",
      },
      {
        field: "userName",
        header: "Tên tài khoản",
        width: "12rem",
        disabled: false,
        visible: false,
        headerAlign: "left",
        textAlign: "left",
      },
      {
        field: "email",
        header: "Địa chỉ email",
        width: "20rem",
        disabled: false,
        visible: false,
        headerAlign: "left",
        textAlign: "left",
      },
      {
        field: "registrationNumber",
        header: "Mã số doanh nghiệp",
        width: "9rem",
        disabled: false,
        visible: true,
        headerAlign: "right",
        textAlign: "right",
      },
      {
        field: "phoneNumber",
        header: "Số điện thoại",
        width: "12rem",
        disabled: false,
        visible: false,
        headerAlign: "right",
        textAlign: "right",
      },
      {
        field: "industryName",
        header: "Ngành kinh doanh",
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
    // Load Data
    this.getIndustries();
    // get brands
    this.getBrands();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes["brokerFromBrokerDetail"].currentValue) {
      this.tableTitle = "Thương hiệu đã hỗ trợ";
      this.isShowTableAction = false;
    }
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  //data functions
  getBrands() {
    this.industryServices
      .getIndustries()
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((industries) => {
          this.industries = industries;
          return this.brandServices.getBrands();
        }),
        finalize(() => (this.isShowSpin = false))
      )
      .subscribe((brands) => {
        this.brands = brands;
        this.brands.forEach((brand) => {
          let industry: Industry = this.getIndustry(brand);
          if (industry) {
            brand.industry = industry;
            brand.industryName = industry.name;
          }
        });
      });
  }
  getIndustry(brand: Brand): Industry {
    for (let i = 0; i < this.industries.length; i++) {
      if (brand.industryId === this.industries[i].id) {
        return this.industries[i];
      }
    }
    return;
  }
  updateIndustry(brand: Brand): Brand {
    let industry = this.getIndustry(brand);
    brand.industry = industry;
    brand.industryName = industry.name;
    return brand;
  }
  getIndustries() {
    this.industryServices.getIndustries().subscribe((res) => {
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
    return this.brands
      ? this.first === this.brands.length - this.TABLE_CONFIG.ROWS
      : true;
  }
  isFirstPage(): boolean {
    return this.brands ? this.first === 0 : true;
  }

  //table functions
  deleteSelectedBrands() {
    swal
      .fire({
        title: "Bạn có chắc muốn xoá các thương hiệu đã chọn?",
        text: "Những thương hiệu này sẽ bị xoá!",
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
          this.selectedBrands.forEach((brand, index) => {
            this.brandServices.deleteBrand(brand).subscribe((res) => {
              if (res) {
                this.selectedBrands[index] = res;
              }
            });
          });
          //Update table
          this.brands.forEach((brandItem) => {
            this.selectedBrands.forEach((selectedBrandItem) => {
              if (brandItem.id === selectedBrandItem.id) {
                brandItem = selectedBrandItem;
                brandItem = this.updateIndustry(brandItem);
              }
            });
          });
          swal.fire({
            title: "Đã xoá!",
            text: "Thương hiệu đã xoá.",
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
  deleteBrand(brand) {
    swal
      .fire({
        title: "Bạn có chắc muốn xoá?",
        text: "'Thương hiệu '" + brand.name + "' sẽ bị xoá!'",
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
          this.brandServices.deleteBrand(brand).subscribe((res) => {
            if (res) {
              brand.status = 0;
              swal.fire({
                title: "Xoá thành công!",
                text: "Thương hiệu " + brand.name +" đã bị xoá.",
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
  openDetailBrand(brand) {
    this.reloadServices.routingNotReload(
      "/thuong-hieu/danh-sach/chi-tiet",
      brand.id
    );
  }
  openEditBrand(brand) {
    this.reloadServices.routingNotReload(
      "/thuong-hieu/danh-sach/chinh-sua",
      brand.id
    );
  }
  openNewBrand() {
    this.reloadServices.routingNotReload(
      "/thuong-hieu/danh-sach/tao-moi",
      null
    );
  }
}
