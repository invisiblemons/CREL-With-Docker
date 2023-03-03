import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { PropertyRoutingModule } from "./property-routing.module";
import { ComponentsModule } from "src/app/components/components.module";
import { ProjectModule } from "../project/project.module";
import { PropertyNewComponent } from "./property-new/property-new.component";
import { PropertyEditComponent } from "./property-edit/property-edit.component";
import { PropertyDetailComponent } from "./property-detail/property-detail.component";
import { PropertyListComponent } from "./property-list/property-list.component";
import { PropertyComponent } from "./property.component";
import { OwnerModule } from "../owner/owner.module";
import { BrokerNewInPropertyComponent } from "./broker-new/broker-new.component";
import { OwnerNewInPropertyComponent } from "./owner-new/owner-new.component";
import { ProjectNewInPropertyComponent } from "./project-new/project-new.component";
import { SuggestionModule } from "../suggestion/suggestion.module";
import { BrandModule } from "../brand/brand.module";

@NgModule({
  declarations: [
    PropertyComponent,
    PropertyListComponent,
    PropertyNewComponent,
    PropertyEditComponent,
    PropertyDetailComponent,
    BrokerNewInPropertyComponent,
    OwnerNewInPropertyComponent,
    ProjectNewInPropertyComponent
  ],
  imports: [
    CommonModule,
    PropertyRoutingModule,
    ComponentsModule,
    ProjectModule,
    OwnerModule,
    SuggestionModule,
    BrandModule
  ],
  exports: [
    PropertyComponent,
    PropertyListComponent,
    PropertyNewComponent,
    PropertyEditComponent,
    PropertyDetailComponent,
  ]
})
export class PropertyModule {}
