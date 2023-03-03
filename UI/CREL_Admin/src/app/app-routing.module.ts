import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { AdminLayoutComponent } from "./layouts/admin-layout/admin-layout.component";
import { AuthGuardService } from "./features/login/auth-guard.service";
import { AuthLayoutComponent } from "./layouts/auth-layout/auth-layout.component";

const routes: Routes = [
  {
    path: "",
    component: AdminLayoutComponent,
    canActivate: [AuthGuardService],
    children: [
      {
        path: "",
        redirectTo: "tong-quan",
        pathMatch: "full",
      },
      {
        path: "tong-quan",
        loadChildren: () =>
          import("./features/home/home.module").then((m) => m.HomeModule),
      },
      {
        path: "thuong-hieu",
        loadChildren: () =>
          import("./features/brand/brand.module").then((m) => m.BrandModule),
      },
      {
        path: "bat-dong-san",
        loadChildren: () =>
          import("./features/property/property.module").then(
            (m) => m.PropertyModule
          ),
      },
      {
        path: "nhan-su",
        loadChildren: () =>
          import("./features/staff/staff.module").then((m) => m.StaffModule),
      },
      {
        path: "ho-so",
        loadChildren: () =>
          import("./features/admin/admin.module").then((m) => m.AdminModule),
      },
      {
        path: "cau-hinh",
        loadChildren: () =>
          import("./features/config/config.module").then((m) => m.ConfigModule),
      },
    ],
  },
  {
    path: "",
    component: AuthLayoutComponent,
    children: [
      {
        path: "",
        redirectTo: "dang-nhap",
        pathMatch: "full",
      },
      {
        path: "dang-nhap",
        loadChildren: () =>
          import("./features/login/auth.module").then((m) => m.AuthModule),
      },
      {
        path: "quen-mat-khau",
        loadChildren: () =>
          import("./features/reset-password/reset-password.module").then(
            (m) => m.ResetPasswordModule
          ),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    scrollPositionRestoration: 'enabled',
})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
