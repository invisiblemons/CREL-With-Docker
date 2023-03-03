import {
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChildren,
} from "@angular/core";
import Chart from "chart.js/auto";
import { Subject } from "rxjs";
import { switchMap, takeUntil } from "rxjs/operators";
import { Column } from "../../core/models/table.model";
import { AreaManager } from "../area-manager/area-manager.model";
import { AreaManagerService } from "../area-manager/area-manager.service";
import { Brand } from "../brand/brand.model";
import { BrandService } from "../brand/brand.service";
import { Broker } from "../broker/broker.model";
import { BrokerService } from "../broker/broker.service";
import { Contract } from "../contract/contract.model";
import { ContractService } from "../contract/contract.service";
import { OwnerService } from "../owner/owner.service";
import { Property } from "../property/property.model";
import { PropertyService } from "../property/property.service";
import { DATE_FORMAT, TABLE_CONFIG } from "../../shared/constants/common.const";
import { ReloadRouteService } from "../../shared/services/reload-route.service";
import swal from "sweetalert2";
import {
  FormBuilder,
  FormControlName,
  FormGroup,
  Validators,
} from "@angular/forms";
import { GenericValidator } from "../../shared/validator/generic-validator";
import { Router } from "@angular/router";

@Component({
  selector: "app-home",
  templateUrl: "home.component.html",
  styleUrls: ["home.component.scss"],
})
export class HomeComponent implements OnInit, OnDestroy {
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
  isShowSkeleton: boolean = true;

  properties: Property[];
  brands: Brand[] = [];
  rawBrands: Brand[];
  brokers: Broker[];
  areaManagers: AreaManager[];

  brand: Brand;

  unverifyModal: boolean;

  rejectMessage: string = "";

  reasons: string[];

  /* 
  fields for object
  */
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = "";

  unverifyForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  constructor(
    private propertyService: PropertyService,
    private brandService: BrandService,
    private brokerService: BrokerService,
    private areaManagerService: AreaManagerService,
    private reloadRouteService: ReloadRouteService,
    private contractServices: ContractService,
    private ownerService: OwnerService,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.reasons = [
      "thông tin thương hiệu không tồn tại",
      "mã số doanh nghiệp không xác định",
    ];

    // validate
    this.validationMessages = {
      rejectMessage: {
        required: "Lý do từ chối xét duyệt không được để trống.",
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
    this.unverifyForm = this.fb.group({
      rejectMessage: ["", [Validators.required]],
    });

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
        field: "name",
        header: "Tên",
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
    this.propertyService
      .getProperties()
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((properties) => {
          this.properties = properties;
          return this.brandService.getBrands();
        }),
        switchMap((brands) => {
          this.rawBrands = brands;
          brands.forEach((brand) => {
            if (brand.status === 1) {
              this.brands.push(brand);
            }
          });

          return this.areaManagerService.getAreaManagers();
        }),
        switchMap((areaManagers) => {
          this.areaManagers = areaManagers;
          return this.brokerService.getBrokers();
        })
      )
      .subscribe((brokers) => {
        this.brokers = brokers;
        this.isShowSkeleton = false;
        this.isShowSpin = false;
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  openProperties() {
    this.reloadRouteService.routingReload("/bat-dong-san/danh-sach", null);
  }

  openBrands() {
    this.reloadRouteService.routingReload("/thuong-hieu/danh-sach", null);
  }

  openAreaManagers() {
    this.reloadRouteService.routingReload(
      "/nhan-su/nguoi-quan-ly-khu-vuc",
      null
    );
  }

  openBrokers() {
    this.reloadRouteService.routingReload("/nhan-su/nguoi-moi-gioi", null);
  }

  verify(brand) {
    swal
      .fire({
        title: "Xét duyệt thương hiệu",
        text: "Bạn có duyệt thương hiệu này?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Duyệt",
        cancelButtonText: "Không duyệt",
        customClass: {
          cancelButton: "btn btn-warning animation-on-hover mr-1 w-42",
          confirmButton: "btn btn-info animation-on-hover w-42",
        },
        buttonsStyling: false,
        allowOutsideClick: false,
        reverseButtons: true,
      })
      .then((result) => {
        if (result.value) {
          brand.status = 2;
          this.brandService.updateBrand(brand).subscribe(
            (brandRes) => {
              swal.fire({
                
                title: "Thành công!",
                text: "Xét duyệt thương hiệu thành công.",
                icon: "success",
                customClass: {
                  confirmButton: "btn btn-success animation-on-hover",
                },
                buttonsStyling: false,
                timer: 2000,
              });

              this.brands = this.brands.filter(
                (brandItem) => brandRes.id !== brandItem.id
              );
            },
            (err) => {
              swal.fire({
                title: "Thất bại!",
                text: "Lỗi xét duyệt. Vui lòng thử lại sau.",
                icon: "error",
                customClass: {
                  confirmButton: "btn btn-danger animation-on-hover",
                },
                buttonsStyling: false,
                timer: 2000,
              });
            }
          );
        } else {
          this.openUnverifyModal(brand);
        }
      });
  }

  openUnverifyModal(brand) {
    this.brand = brand;
    this.unverifyModal = true;
  }
  hideUnverifyModal() {
    this.unverifyModal = false;
  }

  unverify() {
    this.brand.status = 3;
    this.brand.rejectMessage = this.rejectMessage;
    this.brandService.updateBrand(this.brand).subscribe(
      (brandRes) => {
        this.brands = this.brands.filter((brand) => brand.id !== this.brand.id);
        swal.fire({
          title: "Thành công!",
          text: "Đã từ chối duyệt thương hiệu.",
          icon: "success",
          customClass: {
            confirmButton: "btn btn-success animation-on-hover",
          },
          buttonsStyling: false,
          timer: 2000,
        });

        this.brands = this.brands.filter(
          (brandItem) => brandItem.id !== brandRes.id
        );
        this.brand = null;
        this.unverifyForm.reset();
        this.hideUnverifyModal();
      },
      (err) => {
        swal.fire({
          title: "Thất bại!",
          text: "Lỗi xét duyệt. Vui lòng thử lại sau.",
          icon: "error",
          customClass: {
            confirmButton: "btn btn-danger animation-on-hover",
          },
          buttonsStyling: false,
          timer: 2000,
        });
      }
    );
  }

  openDetailBrand(brand) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return true;
    };
    this.router.onSameUrlNavigation = "reload";
    this.router.navigate(["/tong-quan/chi-tiet-thuong-hieu"], {
      queryParams: { id: brand.id, isFromHome: true },
      skipLocationChange: true,
    });
  }
}
