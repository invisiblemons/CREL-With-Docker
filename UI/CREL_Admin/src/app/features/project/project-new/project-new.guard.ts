import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';

import { ProjectNewComponent } from './project-new.component';

@Injectable({
  providedIn: 'root'
})
export class ProjectNewGuard implements CanDeactivate<ProjectNewComponent> {
  canDeactivate(component: ProjectNewComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.projectForm.dirty) {
      const name = component.projectForm.get('name')?.value || 'New Project';
      return confirm(`bạn có chắc muốn thoát khỏi tạo mới dự án bất động sản thương mại ?`);
    }
    return true;
  }
}
