import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AdminEditComponent } from "./admin-edit/admin-edit.component";
import { AdminEditGuard } from "./admin-edit/admin-edit.guard";
import { AdminComponent } from "./admin.component";
import { NewAdminComponent } from "./new-admin/new-admin.component";
import { PasswordChangeComponent } from "./password-change/password-change.component";

const routes: Routes = [
  {
    path: "",
    component: AdminComponent,
    data: {
      breadcrumb: "Hồ sơ",
    },
    children: [
      {
        path: 'chinh-sua',
        component: AdminEditComponent,
        canDeactivate: [AdminEditGuard],
      },
      {
        path: 'doi-mat-khau',
        component: PasswordChangeComponent
      }
    ]
  },
  {
    path: "them-quan-tri",
    component: NewAdminComponent,
    data: {
      breadcrumb: "Thêm quản trị",
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
