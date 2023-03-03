import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { BrandRoutingModule } from "./brand-routing.module";
import { ComponentsModule } from "src/app/components/components.module";
import { BrandComponent } from "./brand.component";
import { BrandDetailComponent } from "./brand-detail/brand-detail.component";
import { BrandListComponent } from "./brand-list/brand-list.component";
import { BrandNewComponent } from "./brand-new/brand-new.component";
import { BrandEditComponent } from "./brand-edit/brand-edit.component";
import { IndustryModule } from "../industry/industry.module";
import { StoreModule } from "../store/store.module";
import { AppointmentModule } from "../appointment/appointment.module";

@NgModule({
  declarations: [
    BrandComponent,
    BrandDetailComponent,
    BrandListComponent,
    BrandNewComponent,
    BrandEditComponent,
  ],
  imports: [
    CommonModule,
    BrandRoutingModule,
    ComponentsModule,
    IndustryModule,
    StoreModule,
    AppointmentModule
  ],
  exports: [
    BrandComponent,
    BrandDetailComponent,
    BrandListComponent,
    BrandNewComponent,
    BrandEditComponent,
  ],
})
export class BrandModule {}
