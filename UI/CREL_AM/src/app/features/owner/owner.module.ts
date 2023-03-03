import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ComponentsModule } from 'src/app/components/components.module';
import { OwnerComponent } from './owner.component';
import { OwnerNewComponent } from './owner-new/owner-new.component';
import { OwnerDetailComponent } from './owner-detail/owner-detail.component';
import { OwnerEditComponent } from './owner-edit/owner-edit.component';
import { OwnerListComponent } from './owner-list/owner-list.component';

@NgModule({
  declarations: [
    OwnerComponent,
    OwnerListComponent,
    OwnerNewComponent,
    OwnerDetailComponent,
    OwnerEditComponent,
  ],
  imports: [CommonModule, ComponentsModule],
  exports: [
    OwnerComponent,
    OwnerListComponent,
    OwnerNewComponent,
    OwnerDetailComponent,
    OwnerEditComponent,
  ],
})
export class OwnerModule {}
