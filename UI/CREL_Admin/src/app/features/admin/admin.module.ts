import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { ComponentsModule } from 'src/app/components/components.module';
import { AdminComponent } from './admin.component';
import { NewAdminComponent } from './new-admin/new-admin.component';
import { AdminEditComponent } from './admin-edit/admin-edit.component';
import { PasswordChangeComponent } from './password-change/password-change.component';


@NgModule({
  declarations: [AdminComponent,NewAdminComponent, AdminEditComponent, PasswordChangeComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
    ComponentsModule
  ]
})
export class AdminModule { }
