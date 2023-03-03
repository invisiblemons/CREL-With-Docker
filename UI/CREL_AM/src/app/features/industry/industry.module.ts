import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ComponentsModule } from 'src/app/components/components.module';
import { IndustryComponent } from './industry.component';
import { IndustryDetailComponent } from './industry-detail/industry-detail.component';
import { IndustryListComponent } from './industry-list/industry-list.component';
import { IndustryEditComponent } from './industry-edit/industry-edit.component';
import { IndustryNewComponent } from './industry-new/industry-new.component';

@NgModule({
  declarations: [
    IndustryComponent,
    IndustryDetailComponent,
    IndustryListComponent,
    IndustryEditComponent,
    IndustryNewComponent,
  ],
  imports: [CommonModule, ComponentsModule],
  exports: [
    IndustryComponent,
    IndustryDetailComponent,
    IndustryListComponent,
    IndustryEditComponent,
    IndustryNewComponent,
  ],
})
export class IndustryModule {}
