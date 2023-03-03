import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndustryDetailComponent } from '../industry/industry-detail/industry-detail.component';
import { IndustryEditComponent } from '../industry/industry-edit/industry-edit.component';
import { IndustryEditGuard } from '../industry/industry-edit/industry-edit.guard';
import { IndustryListComponent } from '../industry/industry-list/industry-list.component';
import { IndustryNewComponent } from '../industry/industry-new/industry-new.component';
import { IndustryNewGuard } from '../industry/industry-new/industry-new.guard';
import { IndustryComponent } from '../industry/industry.component';
import { ContractTemplateComponent } from '../contract-template/contract-template.component';
import { ConfigComponent } from './config.component';

const routes: Routes = [
  {
    path: '',
    component: ConfigComponent,
    children: [
      {
        path: '',
        redirectTo: 'nganh',
        pathMatch: 'full'
      },
      {
        path: 'nganh',
        component: IndustryListComponent,
        data: {
          breadcrumb: 'Ngành kinh doanh',
        },
        children: [
          {
            path: 'chinh-sua',
            component: IndustryEditComponent,
            canDeactivate: [IndustryEditGuard],
          },
          {
            path: 'tao-moi',
            component: IndustryNewComponent,
            canDeactivate: [IndustryNewGuard],
          },
          {
            path: 'chi-tiet',
            component: IndustryDetailComponent,
          }
        ],
      },
      {
        path: 'mau-hop-dong',
        component: ContractTemplateComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConfigRoutingModule { }
