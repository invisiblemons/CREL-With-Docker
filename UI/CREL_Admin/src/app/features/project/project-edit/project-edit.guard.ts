import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { ProjectEditComponent } from './project-edit.component';


@Injectable({
  providedIn: 'root'
})
export class ProjectEditGuard implements CanDeactivate<ProjectEditComponent> {
  canDeactivate(component: ProjectEditComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.projectForm.dirty) {
      const name = component.projectForm.get('name')?.value || 'New Project';
      return confirm(`bạn có chắc muốn thoát khỏi chỉnh sửa dự án bất động sản thương mại ?`);
    }
    return true;
  }
}
