import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AreaManagerDetailComponent } from "../area-manager/area-manager-detail/area-manager-detail.component";
import { AreaManagerEditComponent } from "../area-manager/area-manager-edit/area-manager-edit.component";
import { AreaManagerEditGuard } from "../area-manager/area-manager-edit/area-manager-edit.guard";
import { AreaManagerListComponent } from "../area-manager/area-manager-list/area-manager-list.component";
import { AreaManagerNewComponent } from "../area-manager/area-manager-new/area-manager-new.component";
import { AreaManagerNewGuard } from "../area-manager/area-manager-new/area-manager-new.guard";
import { AreaManagerComponent } from "../area-manager/area-manager.component";
import { BrokerDetailComponent } from "../broker/broker-detail/broker-detail.component";
import { BrokerEditComponent } from "../broker/broker-edit/broker-edit.component";
import { BrokerEditGuard } from "../broker/broker-edit/broker-edit.guard";
import { BrokerListComponent } from "../broker/broker-list/broker-list.component";
import { BrokerNewComponent } from "../broker/broker-new/broker-new.component";
import { BrokerNewGuard } from "../broker/broker-new/broker-new.guard";
import { BrokerComponent } from "../broker/broker.component";
import { TeamDetailComponent } from "../team/team-detail/team-detail.component";
import { TeamEditComponent } from "../team/team-edit/team-edit.component";
import { TeamEditGuard } from "../team/team-edit/team-edit.guard";
import { TeamListComponent } from "../team/team-list/team-list.component";
import { TeamNewComponent } from "../team/team-new/team-new.component";
import { TeamNewGuard } from "../team/team-new/team-new.guard";
import { StaffComponent } from "./staff.component";

const routes: Routes = [
  {
    path: "",
    component: StaffComponent,
    data: {
      breadcrumb: "Nhân sự",
    },
    children: [
      {
        path: "nguoi-moi-gioi",
        component: BrokerListComponent,
        data: {
          breadcrumb: "Nhân viên môi giới",
        },
        children: [
          {
            path: "chi-tiet",
            component: BrokerDetailComponent,
          },
          {
            path: "chinh-sua",
            component: BrokerEditComponent,
            canDeactivate: [BrokerEditGuard],
          },
          {
            path: "tao-moi",
            component: BrokerNewComponent,
            canDeactivate: [BrokerNewGuard],
          },
        ],
      },
      {
        path: "nguoi-quan-ly-khu-vuc",
        component: AreaManagerListComponent,
        data: {
          breadcrumb: "Quản lý khu vực",
        },
        children: [
          {
            path: "chi-tiet",
            component: AreaManagerDetailComponent,
          },
          {
            path: "chinh-sua",
            component: AreaManagerEditComponent,
            canDeactivate: [AreaManagerEditGuard],
          },
          {
            path: "tao-moi",
            component: AreaManagerNewComponent,
            canDeactivate: [AreaManagerNewGuard],
          },
        ],
      },
      {
        path: "nhom",
        component: TeamListComponent,
        data: {
          breadcrumb: "Nhóm nhân sự",
        },
        children: [
          {
            path: "chi-tiet",
            component: TeamDetailComponent,
          },
          {
            path: "chinh-sua",
            component: TeamEditComponent,
            canDeactivate: [TeamEditGuard],
          },
          {
            path: "tao-moi",
            component: TeamNewComponent,
            canDeactivate: [TeamNewGuard],
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
export class StaffRoutingModule {}
