import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ComponentsModule } from 'src/app/components/components.module';
import { ProjectComponent } from './project.component';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { ProjectNewComponent } from './project-new/project-new.component';
import { ProjectEditComponent } from './project-edit/project-edit.component';
import { ProjectListComponent } from './project-list/project-list.component';

@NgModule({
  declarations: [
    ProjectComponent,
    ProjectDetailComponent,
    ProjectNewComponent,
    ProjectEditComponent,
    ProjectListComponent,
  ],
  imports: [CommonModule, ComponentsModule],
  exports: [
    ProjectComponent,
    ProjectDetailComponent,
    ProjectNewComponent,
    ProjectEditComponent,
    ProjectListComponent,
  ],
})
export class ProjectModule {}
