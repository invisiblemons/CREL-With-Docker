import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HandlingRequestComponent } from '../brand-request/handling-request/handling-request.component';
import { BrandDetailComponent } from '../brand/brand-detail/brand-detail.component';
import { PropertyDetailComponent } from '../property/property-detail/property-detail.component';
import { HomeComponent } from './home.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    data: {
      breadcrumb: 'Tổng quan'
    },
    children: [
      {
        path: 'xu-ly-de-xuat',
        component: HandlingRequestComponent,
        children: [
          {
            path: 'chi-tiet-mat-bang',
            component: PropertyDetailComponent
          },
          {
            path: 'chi-tiet-thuong-hieu',
            component: BrandDetailComponent
          }
        ]
      },
      {
        path: 'chi-tiet-thuong-hieu',
        component: BrandDetailComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
