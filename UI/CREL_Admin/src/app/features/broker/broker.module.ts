import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ComponentsModule } from "src/app/components/components.module";
import { BrokerComponent } from "./broker.component";
import { BrokerDetailComponent } from "./broker-detail/broker-detail.component";
import { BrokerNewComponent } from "./broker-new/broker-new.component";
import { BrokerEditComponent } from "./broker-edit/broker-edit.component";
import { BrokerListComponent } from "./broker-list/broker-list.component";
import { PropertyModule } from "../property/property.module";
import { BrandModule } from "../brand/brand.module";
import { StoreModule } from "../store/store.module";

@NgModule({
  declarations: [
    BrokerComponent,
    BrokerDetailComponent,
    BrokerNewComponent,
    BrokerEditComponent,
    BrokerListComponent,
  ],
  imports: [
    CommonModule,
    ComponentsModule,
    PropertyModule,
    BrandModule,
    StoreModule,
  ],
  exports: [
    BrokerComponent,
    BrokerDetailComponent,
    BrokerNewComponent,
    BrokerEditComponent,
    BrokerListComponent,
  ],
})
export class BrokerModule {}
