import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppointmentDetailComponent } from '../appointment/appointment-detail/appointment-detail.component';
import { AppointmentListComponent } from '../appointment/appointment-list/appointment-list.component';
import { ContractDetailComponent } from '../contract/contract-detail/contract-detail.component';
import { ContractListComponent } from '../contract/contract-list/contract-list.component';
import { ContractComponent } from '../contract/contract.component';
import { IndustryDetailComponent } from '../industry/industry-detail/industry-detail.component';
import { IndustryEditComponent } from '../industry/industry-edit/industry-edit.component';
import { IndustryEditGuard } from '../industry/industry-edit/industry-edit.guard';
import { IndustryListComponent } from '../industry/industry-list/industry-list.component';
import { IndustryNewComponent } from '../industry/industry-new/industry-new.component';
import { IndustryNewGuard } from '../industry/industry-new/industry-new.guard';
import { StoreDetailComponent } from '../store/store-detail/store-detail.component';
import { StoreListComponent } from '../store/store-list/store-list.component';
import { BrandDetailComponent } from './brand-detail/brand-detail.component';
import { PropertyDetailComponent } from './brand-detail/property-suggestion/property-detail/property-detail.component';
import { BrandEditComponent } from './brand-edit/brand-edit.component';
import { BrandEditGuard } from './brand-edit/brand-edit.guard';
import { BrandListComponent } from './brand-list/brand-list.component';
import { BrandNewComponent } from './brand-new/brand-new.component';
import { BrandNewGuard } from './brand-new/brand-new.guard';
import { BrandComponent } from './brand.component';
const routes: Routes = [
  {
    path: '',
    component: BrandComponent,
    children: [
      {
        path: '',
        redirectTo: 'danh-sach',
        pathMatch: 'full',
      },
      {
        path: 'danh-sach',
        component: BrandListComponent,
        children: [
          {
            path: 'chinh-sua',
            component: BrandEditComponent,
            canDeactivate: [BrandEditGuard],
          },
          {
            path: 'tao-moi',
            component: BrandNewComponent,
            canDeactivate: [BrandNewGuard],
          },
          {
            path: 'chi-tiet',
            component: BrandDetailComponent,
            children: [
              {
                path: 'chi-tiet-mat-bang',
                component: PropertyDetailComponent,
              },
            ],
          },
        ],
      },
      {
        path: 'cuoc-hen',
        component: AppointmentListComponent,
        children: [
          {
            path: 'chi-tiet',
            component: AppointmentDetailComponent,
          },
        ],
      },
      {
        path: 'hop-dong',
        component: ContractListComponent,
        children: [
          {
            path: 'chi-tiet',
            component: ContractDetailComponent,
          },
        ],
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BrandRoutingModule {}
