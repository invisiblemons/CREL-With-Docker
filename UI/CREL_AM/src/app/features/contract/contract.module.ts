import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { ContractRoutingModule } from "./contract-routing.module";
import { ComponentsModule } from "src/app/components/components.module";
import { ContractComponent } from "./contract.component";
import { ContractListComponent } from "./contract-list/contract-list.component";
import { ContractDetailComponent } from "./contract-detail/contract-detail.component";
import { BrandDetailComponent } from "./brand-detail/brand-detail.component";
import { PropertyDetailComponent } from "./property-detail/property-detail.component";
import { BrokerDetailComponent } from "./broker-detail/broker-detail.component";
import { OwnerDetailComponent } from "./owner-detail/owner-detail.component";

@NgModule({
  declarations: [
    ContractComponent,
    ContractListComponent,
    ContractDetailComponent,
    PropertyDetailComponent,
    BrokerDetailComponent,
    BrandDetailComponent,
    OwnerDetailComponent
  ],
  imports: [CommonModule, ContractRoutingModule, ComponentsModule],
  exports: [
    ContractComponent,
    ContractListComponent,
    ContractDetailComponent
  ],
})
export class ContractModule {}
