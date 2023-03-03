import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AreaManagerEditComponent } from "../area-manager/area-manager-edit/area-manager-edit.component";
import { AreaManagerEditGuard } from "../area-manager/area-manager-edit/area-manager-edit.guard";
import { AreaManagerComponent } from "./area-manager.component";
import { PasswordChangeComponent } from "./password-change/password-change.component";

const routes: Routes = [
  {
    path: "",
    component: AreaManagerComponent,
    data: {
      breadcrumb: "Hồ sơ",
    },
    children: [
      {
        path: "chinh-sua",
        component: AreaManagerEditComponent,
        canDeactivate: [AreaManagerEditGuard],
      },
      {
        path: 'doi-mat-khau',
        component: PasswordChangeComponent
      }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AreaManagerRoutingModule {}
