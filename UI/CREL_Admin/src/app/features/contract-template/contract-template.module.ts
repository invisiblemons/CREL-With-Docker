import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { ContractTemplateRoutingModule } from "./contract-template-routing.module";

import { ComponentsModule } from 'src/app/components/components.module';
import { ContractTemplateComponent } from "./contract-template.component";

@NgModule({
  declarations: [ContractTemplateComponent],
  imports: [CommonModule, ContractTemplateRoutingModule, ComponentsModule],
})
export class ContractTemplateModule {}
