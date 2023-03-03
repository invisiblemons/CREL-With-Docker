import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContractTemplateComponent } from "./contract-template.component";

const routes: Routes = [
  {
    path: '',
    component: ContractTemplateComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ContractTemplateRoutingModule { }
