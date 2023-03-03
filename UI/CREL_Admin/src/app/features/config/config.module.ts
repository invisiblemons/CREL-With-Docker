import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ConfigRoutingModule } from './config-routing.module';
import { ComponentsModule } from 'src/app/components/components.module';
import { IndustryModule } from '../industry/industry.module';
import { ContractTemplateModule } from '../contract-template/contract-template.module';
import { ConfigComponent } from './config.component';


@NgModule({
  declarations: [ConfigComponent],
  imports: [
    CommonModule,
    ConfigRoutingModule,
    ComponentsModule,
    IndustryModule,
    ContractTemplateModule
  ]
})
export class ConfigModule { }
