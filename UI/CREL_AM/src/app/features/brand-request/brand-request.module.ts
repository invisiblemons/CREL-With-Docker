import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BrandRequestRoutingModule } from './brand-request-routing.module';
import { HandlingRequestComponent } from './handling-request/handling-request.component';
import { BrandRequestComponent } from './brand-request.component';
import { ComponentsModule } from 'src/app/components/components.module';


@NgModule({
  declarations: [
    HandlingRequestComponent,
    BrandRequestComponent
  ],
  imports: [
    CommonModule,
    BrandRequestRoutingModule,
    ComponentsModule
  ],
  exports: [
    HandlingRequestComponent,
    BrandRequestComponent
  ]
})
export class BrandRequestModule { }
