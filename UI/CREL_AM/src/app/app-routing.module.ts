import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { AuthGuardService } from "./features/login/auth-guard.service";
import { AuthLayoutComponent } from "./layouts/auth-layout/auth-layout.component";
import { UserLayoutComponent } from "./layouts/user-layout/user-layout.component";

const routes: Routes = [
  {
    path: "",
    component: UserLayoutComponent,
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
        path: "",
        redirectTo: "bat-dong-san",
        pathMatch: "full",
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
        path: "nguoi-moi-gioi",
        loadChildren: () =>
          import("./features/broker/broker.module").then((m) => m.BrokerModule),
      },
      {
        path: "nhom",
        loadChildren: () =>
          import("./features/team/team.module").then((m) => m.TeamModule),
      },
      {
        path: "ho-so",
        loadChildren: () =>
          import("./features/area-manager/area-manager.module").then((m) => m.AreaManagerModule),
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
