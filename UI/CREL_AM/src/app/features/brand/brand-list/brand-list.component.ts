import { Component, Input, OnDestroy, OnInit, SimpleChanges } from "@angular/core";
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
  @Input() brokerFromDetailScreen: Broker;

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
    private reloadServices: ReloadRouteService,
  ) {
    this.isShowSpin = true;
    this.loading = false;
    this.tableTitle = "Qu???n l?? th????ng hi???u kinh doanh";
    this.isShowTableAction = true;
    
    this.statuses = [
      { label: "???? xo??", value: 0 },
      { label: "?????i x??t duy???t", value: 1 },
      { label: "???????c duy???t", value: 2 },
      { label: "Kh??ng ???????c duy???t", value: 3 },
    ];
    this.brands = [];

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
        header: "M??",
        width: "5rem",
        disabled: true,
        visible: true,
        headerAlign: "center",
        textAlign: "center",
      },
      {
        field: "name",
        header: "T??n th????ng hi???u",
        width: "12rem",
        disabled: true,
        visible: true,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: "userName",
        header: "T??n t??i kho???n",
        width: "12rem",
        disabled: false,
        visible: false,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: "email",
        header: "?????a ch??? email",
        width: "20rem",
        disabled: false,
        visible: false,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: "registrationNumber",
        header: "M?? s??? doanh nghi???p",
        width: "9rem",
        disabled: false,
        visible: true,
        headerAlign: 'right',
        textAlign: 'right',
      },
      {
        field: "phoneNumber",
        header: "S??? ??i???n tho???i",
        width: "12rem",
        disabled: false,
        visible: false,
        headerAlign: 'right',
        textAlign: 'right',
      },
      {
        field: "industryName",
        header: "Ng??nh kinh doanh",
        width: "12rem",
        disabled: false,
        visible: true,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: "status",
        header: "Tr???ng th??i",
        width: "10rem",
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: "action",
        header: "Thao t??c",
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
    // get brands
    this.getBrands();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['brokerFromDetailScreen'].currentValue) {
      this.tableTitle = "Th????ng hi???u ???? h??? tr???";
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
    this.industryServices.getIndustries().pipe(
      takeUntil(this.destroySubs$),
      switchMap((industries) => {
        this.industries = industries;
        return this.brandServices.getBrands();
      }),
      finalize(() => (this.isShowSpin = false))
    ).subscribe((brands) => {
      this.brands = brands;
      this.brands.forEach((brand) => {
        let industry: Industry = this.getIndustry(brand);
        if (industry) {
          brand.industry = industry;
          brand.industryName = industry.name;
        }
      });
    })
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
    return this.brands ? this.first === this.brands.length - this.TABLE_CONFIG.ROWS : true;
  }
  isFirstPage(): boolean {
    return this.brands ? this.first === 0 : true;
  }

  //table functions
  deleteSelectedBrands() {
    swal
      .fire({
        title: "B???n c?? ch???c mu???n xo?? c??c th????ng hi???u ???? ch???n?",
        text: "Nh???ng th????ng hi???u n??y s??? b??? xo??!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "C??, xo?? n??!",
        cancelButtonText: "Kh??ng, gi??? nguy??n",
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
            title: "Xo?? th??nh c??ng!",
            text: "C??c th????ng hi???u ???????c ch???n ???? b??? xo??.",
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
        title: "B???n c?? ch???c mu???n xo???",
        text: "'Th????ng hi???u '" + brand.name + "' s??? b??? xo??!'",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "C??, xo?? n??!",
        cancelButtonText: "Kh??ng, gi??? nguy??n",
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
                title: "Xo?? th??nh c??ng!",
                text: "'Th????ng hi???u '" + brand.name +"'???? b??? xo??.'",
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
      '/thuong-hieu/danh-sach/chi-tiet',
      brand.id
    );
  }
  openEditBrand(brand) {
    this.reloadServices.routingNotReload(
      '/thuong-hieu/danh-sach/chinh-sua',
      brand.id
    );
  }
  openNewBrand() {
    this.reloadServices.routingNotReload(
      '/thuong-hieu/danh-sach/tao-moi',
      null
    );
  }
}
