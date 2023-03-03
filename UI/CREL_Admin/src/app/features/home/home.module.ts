import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { ComponentsModule } from 'src/app/components/components.module';
import { HomeComponent } from './home.component';
import { BrandModule } from '../brand/brand.module';
import { BrandRequestModule } from '../brand-request/brand-request.module';
import { PropertyModule } from '../property/property.module';
import { ContractComingComponent } from './contract-coming/contract-coming.component';


@NgModule({
  declarations: [HomeComponent, ContractComingComponent],
  imports: [
    CommonModule,
    HomeRoutingModule,
    ComponentsModule,
    BrandRequestModule,
    BrandModule,
    PropertyModule
  ],
})
export class HomeModule { }
