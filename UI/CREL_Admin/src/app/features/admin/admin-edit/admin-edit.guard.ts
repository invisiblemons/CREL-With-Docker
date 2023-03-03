import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { AdminEditComponent } from './admin-edit.component';


@Injectable({
  providedIn: 'root'
})
export class AdminEditGuard implements CanDeactivate<AdminEditComponent> {
  canDeactivate(component: AdminEditComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.adminForm.dirty) {
      const name = component.adminForm.get('name')?.value || 'New Admin';
      return confirm(`bạn có chắc muốn thoát khỏi chỉnh sửa người quản trị ?`);
    }
    return true;
  }
}

