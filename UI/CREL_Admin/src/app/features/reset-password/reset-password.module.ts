import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ResetPasswordRoutingModule } from './reset-password-routing.module';
import { ComponentsModule } from 'src/app/components/components.module';
import { ResetPasswordComponent } from './reset-password.component';


@NgModule({
  declarations: [ResetPasswordComponent],
  imports: [
    CommonModule,
    ResetPasswordRoutingModule,
    ComponentsModule
  ]
})
export class ResetPasswordModule { }
