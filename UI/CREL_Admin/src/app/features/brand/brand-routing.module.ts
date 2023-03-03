import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppointmentEditComponent } from '../appointment/appointment-edit/appointment-edit.component';
import { AppointmentListComponent } from '../appointment/appointment-list/appointment-list.component';
import { AppointmentNewComponent } from '../appointment/appointment-new/appointment-new.component';
import { AppointmentDetailComponent } from '../appointment/appointment-detail/appointment-detail.component';
import { AppointmentEditGuard } from '../appointment/appointment-edit/appointment-edit.guard';
import { AppointmentNewGuard } from '../appointment/appointment-new/appointment-new.guard';
import { StoreDetailComponent } from '../store/store-detail/store-detail.component';
import { StoreListComponent } from '../store/store-list/store-list.component';
import { BrandDetailComponent } from './brand-detail/brand-detail.component';
import { BrandEditComponent } from './brand-edit/brand-edit.component';
import { BrandEditGuard } from './brand-edit/brand-edit.guard';
import { BrandListComponent } from './brand-list/brand-list.component';
import { BrandNewComponent } from './brand-new/brand-new.component';
import { BrandNewGuard } from './brand-new/brand-new.guard';
import { BrandComponent } from './brand.component';
import { ContractListComponent } from '../contract/contract-list/contract-list.component';
import { ContractDetailComponent } from '../contract/contract-detail/contract-detail.component';
const routes: Routes = [
  {
    path: '',
    component: BrandComponent,
    children: [
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
          }
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
          {
            path: 'chinh-sua',
            component: AppointmentEditComponent,
            canDeactivate: [AppointmentEditGuard],
          },
          {
            path: 'tao-moi',
            component: AppointmentNewComponent,
            canDeactivate: [AppointmentNewGuard],
          }
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
