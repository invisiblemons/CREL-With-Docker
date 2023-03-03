import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AreaManagerComponent } from './area-manager.component';
import { ComponentsModule } from 'src/app/components/components.module';
import { AreaManagerEditComponent } from './area-manager-edit/area-manager-edit.component';
import { AreaManagerRoutingModule } from './area-manager-routing.module';
import { PasswordChangeComponent } from './password-change/password-change.component';

@NgModule({
  declarations: [
    AreaManagerComponent,
    AreaManagerEditComponent,
    PasswordChangeComponent,
  ],
  imports: [CommonModule, ComponentsModule, AreaManagerRoutingModule],
  exports: [AreaManagerComponent],
})
export class AreaManagerModule {}
