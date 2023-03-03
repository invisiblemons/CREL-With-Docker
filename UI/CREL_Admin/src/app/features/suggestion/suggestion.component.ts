import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import CustomStore from 'devextreme/data/custom_store';
import { Subject } from 'rxjs';
import { finalize, switchMap, takeUntil } from 'rxjs/operators';
import { AVATAR_DEFAULT } from 'src/app/shared/constants/common.const';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';
import { Brand } from '../brand/brand.model';
import { BrandService } from '../brand/brand.service';
import { Industry } from '../industry/industry.model';
import { IndustryService } from '../industry/industry.service';
import { Property } from '../property/property.model';
import { SuggestionService } from './suggestion.service';
import swal from 'sweetalert2';
import { PropertyService } from '../property/property.service';

@Component({
  selector: 'app-suggestion',
  templateUrl: './suggestion.component.html',
  styleUrls: ['./suggestion.component.scss'],
})
export class SuggestionComponent implements OnInit, OnDestroy {
  AVATAR_DEFAULT = AVATAR_DEFAULT;
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  //Field of Objects
  propertiesId: number[] = [];

  statuses: { label: string; value: number }[];

  brand: Brand;

  selectedBrands: Brand[] = [];

  isSelectedBrand: boolean;

  brands: Brand[] = [];

  brandGridDataSource: any;

  brandGridColumns: any = [
    {
      caption: 'Tên',
      dataField: 'name',
      dataType: 'string',
    },
    {
      caption: 'Số điện thoại',
      dataField: 'phoneNumber',
      dataType: 'string',
    },
    {
      caption: 'Mã số doanh nghiệp',
      dataField: 'registrationNumber',
      dataType: 'string',
    },
  ];

  brandGridBoxValue: number[];

  isBrandGridBoxOpened: boolean = false;

  //industry
  industry: Industry;

  industries: Industry[] = [];

  constructor(
    private ref: ChangeDetectorRef,
    private brandServices: BrandService,
    private industryServices: IndustryService,
    private router: Router,
    private route: ActivatedRoute,
    private reloadRouteServices: ReloadRouteService,
    private suggestionService: SuggestionService,
    private propertyService: PropertyService
  ) {
    this.isShowSkeleton = true;
    this.isShowDialog = true;
    this.isSelectedBrand = false;

    this.statuses = [
      { label: 'Đã xoá', value: 0 },
      { label: 'Đợi xét duyệt', value: 1 },
      { label: 'Đang hoạt động', value: 2 },
      { label: 'Không Đang hoạt động', value: 3 },
    ];
    this.brands = [];
  }

  ngOnInit(): void {
    // Get properties
    this.route.queryParams.subscribe((params) => {
      params['propertyIdList'].forEach((id) => {
        this.propertiesId.push(parseInt(id));
      });
    });

    // Get brands
    this.loadData();
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  //data functions
  loadData() {
    this.industryServices
      .getIndustries()
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((industries) => {
          this.industries = industries;
          return this.brandServices.getActiveBrands();
        }),
        finalize(() => (this.isShowSkeleton = false))
      )
      .subscribe((brands) => {
        this.brands = brands;

        //gridbox devex
        this.brandGridDataSource = this.makeAsyncDataSource(this.brands);

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

  //dev-extreme
  makeAsyncDataSource(items) {
    return new CustomStore({
      loadMode: 'raw',
      key: 'id',
      load() {
        return items;
      },
    });
  }
  brandGridBox_displayExpr(item) {
    return (
      item && `${item.name} <${item.phoneNumber}> <${item.registrationNumber}>`
    );
  }

  onBrandGridBoxOptionChanged(e) {
    if (e.name === 'value') {
      this.isBrandGridBoxOpened = false;

      let searchedBrand = this.brands.filter((res) => res.id === e.value[0])[0];

      if (this.selectedBrands) {
        //move selected item to top
        this.brands = this.brands.filter(
          (item) => item.id !== searchedBrand.id
        );
        this.brands.unshift(searchedBrand);

        this.isSelectedBrand = true;
      }

      //click searched item
      document.getElementById(searchedBrand.id.toString()).click();
      if (!e.previousValue) {
        this.ref.detectChanges();
      }
    }
  }

  // functions of list
  openDetailBrand(brand) {
    this.router.navigate(['/bat-dong-san/danh-sach/de-xuat/chi-tiet'], {
      queryParams: { id: brand.id, fromSuggestion: true },
    });
  }
  hideDialog() {
    this.reloadRouteServices.routingNotReload('/bat-dong-san/danh-sach', null);
  }

  suggestBrand() {
    this.selectedBrands.forEach((brand, index) => {
      this.suggestionService
        .insertSuggestion(this.propertiesId, brand.id)
        .subscribe((suggestion) => {
          if (index === this.selectedBrands.length - 1) {
            swal.fire({
              title: 'Thành công!',
              text: 'Đề xuất thành công.',
              icon: 'success',
              customClass: {
                confirmButton: 'btn btn-success animation-on-hover',
              },
              buttonsStyling: false,
              timer: 2000,
            });
            this.reloadRouteServices.routingReload(
              '/bat-dong-san/danh-sach',
              null
            );
          }
        });
    });
  }
}
