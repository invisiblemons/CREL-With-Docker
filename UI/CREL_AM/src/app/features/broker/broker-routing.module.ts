import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BrokerDetailComponent } from '../broker/broker-detail/broker-detail.component';
import { BrokerEditComponent } from '../broker/broker-edit/broker-edit.component';
import { BrokerEditGuard } from '../broker/broker-edit/broker-edit.guard';
import { BrokerListComponent } from '../broker/broker-list/broker-list.component';
import { BrokerNewComponent } from '../broker/broker-new/broker-new.component';
import { BrokerNewGuard } from '../broker/broker-new/broker-new.guard';
import { BrokerComponent } from './broker.component';
const routes: Routes = [
  {
    path: '',
    component: BrokerComponent,
    children: [
      {
        path: "",
        redirectTo: "danh-sach",
        pathMatch: "full",
      },
      {
        path: 'danh-sach',
        component: BrokerListComponent,
        children: [
          {
            path: 'chi-tiet',
            component: BrokerDetailComponent,
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
export class BrokerRoutingModule {}
