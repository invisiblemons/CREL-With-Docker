import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { ComponentsModule } from 'src/app/components/components.module';
import { HomeComponent } from './home.component';
import { BrandRequestModule } from '../brand-request/brand-request.module';
import { BrandModule } from '../brand/brand.module';
import { PropertyModule } from '../property/property.module';


@NgModule({
  declarations: [HomeComponent],
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
