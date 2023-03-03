import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { switchMap, takeUntil } from 'rxjs/operators';
import { Column } from 'src/app/core/models/table.model';
import {
  AVATAR_DEFAULT,
  TABLE_CONFIG,
} from 'src/app/shared/constants/common.const';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';
import { Brand } from '../brand/brand.model';
import { BrandService } from '../brand/brand.service';
import { BrandRequest } from './brand-request.model';
import { BrandRequestService } from './brand-request.service';

@Component({
  selector: 'app-brand-request',
  templateUrl: './brand-request.component.html',
  styleUrls: ['./brand-request.component.scss'],
})
export class BrandRequestComponent implements OnInit, OnDestroy {
  AVATAR_DEFAULT = AVATAR_DEFAULT;
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
  //brandRequest
  brandRequest: BrandRequest;

  brandRequests: BrandRequest[] = [];

  selectedBrandRequests: BrandRequest[] = [];

  brands: Brand[];

  constructor(
    private brandRequestServices: BrandRequestService,
    private reloadServices: ReloadRouteService,
    private brandService: BrandService,
    private router: Router
  ) {
    this.isShowSpin = true;
    this.loading = false;
    this.cols = [
      {
        field: 'id',
        header: 'Mã',
        width: '5rem',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: 'area',
        header: 'Quận/Huyện',
        width: '12rem',
        disabled: false,
        visible: true,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'money',
        header: 'Khoảng tiền',
        width: '12rem',
        disabled: false,
        visible: true,
        headerAlign: 'right',
        textAlign: 'right',
      },
      {
        field: 'floorArea',
        header: 'Khoảng diện tích',
        width: '12rem',
        disabled: false,
        visible: false,
        headerAlign: 'right',
        textAlign: 'right',
      },
      {
        field: 'action',
        header: 'Xử lý',
        width: '15rem',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
    ];
    this.displayCols = this.cols.filter((element) => !element.disabled);
    this._selectedColumns = this.cols.filter((element) => element.visible);
  }

  ngOnInit(): void {
    this.brandService
      .getBrands()
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((brands) => {
          this.brands = brands;
          return this.brandRequestServices.getRequests();
        })
      )
      .subscribe((requestsRes) => {
        let requestTemp = [];
        if (requestsRes.length > 0) {
          requestsRes.forEach((request, index) => {
            this.brands.forEach((brand) => {
              if (request.brandId === brand.id) {
                request.brand = brand;
              }
            });
            if (request.status === 1 && request.amount === 0) {
              requestTemp.push(request);
            }
          });
          this.brandRequests = requestTemp;
          this.isShowSpin = false;
        } else {
          this.brandRequests = [];
          this.isShowSpin = false;
        }
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
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
    return this.brandRequests
      ? this.first === this.brandRequests.length - this.TABLE_CONFIG.ROWS
      : true;
  }
  isFirstPage(): boolean {
    return this.brandRequests ? this.first === 0 : true;
  }

  handleRequest(brandRequest) {
    this.reloadServices.routingNotReload(
      '/tong-quan/xu-ly-de-xuat',
      brandRequest.id
    );
  }

  openBrandDetail(brandRequest) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return true;
    };
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate(['/tong-quan/chi-tiet-thuong-hieu'], {
      queryParams: { id: brandRequest.brandId, fromHome: true },
      skipLocationChange: true,
    });
  }
}
