import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ComponentsModule } from "src/app/components/components.module";
import { StoreComponent } from "./store.component";
import { StoreDetailComponent } from "./store-detail/store-detail.component";
import { StoreListComponent } from "./store-list/store-list.component";
import { ContractModule } from "../contract/contract.module";

@NgModule({
  declarations: [
    StoreComponent,
    StoreDetailComponent,
    StoreListComponent,
  ],
  imports: [CommonModule, ComponentsModule, ContractModule],
  exports: [
    StoreComponent,
    StoreDetailComponent,
    StoreListComponent,
  ],
})
export class StoreModule {}
