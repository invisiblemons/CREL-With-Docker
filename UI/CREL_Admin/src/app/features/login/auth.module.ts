import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthRoutingModule } from './auth-routing.module';
import { ComponentsModule } from '../../components/components.module';


@NgModule({
  imports: [
    CommonModule,
    AuthRoutingModule,
    ComponentsModule,
  ],
  declarations: []
})
export class AuthModule { }
