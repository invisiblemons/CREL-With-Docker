import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouteReuseStrategy, RouterModule } from "@angular/router";
import { CommonModule, DatePipe } from "@angular/common";
import { ToastrModule } from "ngx-toastr";

import { AppComponent } from "./app.component";

import { AppRoutingModule } from "./app-routing.module";
import { ComponentsModule } from "./components/components.module";
import { AuthLayoutComponent } from "./layouts/auth-layout/auth-layout.component";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { AuthService } from "./features/login/auth.service";
import { AngularFireModule } from "@angular/fire";
import { environment } from "src/environments/environment";
import { AngularFireAuthModule } from "@angular/fire/auth";
import { JwtIntercepter } from "./features/login/jwt.interceptor";
import { ConfirmationService, MessageService } from "primeng/api";
import { AngularFireStorageModule } from "@angular/fire/storage";

import {
  NgxUiLoaderModule,
  NgxUiLoaderConfig,
  SPINNER,
  POSITION,
  PB_DIRECTION,
  NgxUiLoaderRouterModule,
} from "ngx-ui-loader";
import { UserLayoutComponent } from "./layouts/user-layout/user-layout.component";

const ngxUiLoaderConfig: NgxUiLoaderConfig = {
  fgsColor: "#0078D7",
  bgsPosition: POSITION.centerCenter,
  bgsSize: 40,
  fgsSize: 60,
  bgsType: SPINNER.ballSpinFadeRotating, // background spinner type
  fgsType: SPINNER.ballSpinFadeRotating, // foreground spinner type
  pbDirection: PB_DIRECTION.leftToRight, // progress bar direction
  pbThickness: 5, // progress bar thickness
  textColor: "#0067D4",
  textPosition: POSITION.centerCenter,
  logoPosition: POSITION.centerCenter,
  logoSize: 180,
  gap: 0,
  logoUrl: "../assets/img/Logo/crel-color.png",
  overlayColor: "rgba(0,0,0,0.9)",
};

@NgModule({
  declarations: [AppComponent, UserLayoutComponent, AuthLayoutComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    RouterModule,
    AppRoutingModule,
    ToastrModule.forRoot(),
    ComponentsModule,
    HttpClientModule,
    AngularFireModule.initializeApp(environment.firebaseConfig),
    AngularFireAuthModule,
    AngularFireStorageModule,

    // Import NgxUiLoaderModule with custom configuration globally
    NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
    NgxUiLoaderRouterModule.forRoot({ showForeground: true }),
  ],
  providers: [
    AuthService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtIntercepter, multi: true },
    MessageService,
    ConfirmationService,
    DatePipe,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
