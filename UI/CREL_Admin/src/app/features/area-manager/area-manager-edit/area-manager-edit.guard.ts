import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { AreaManagerEditComponent } from './area-manager-edit.component';


@Injectable({
  providedIn: 'root'
})
export class AreaManagerEditGuard implements CanDeactivate<AreaManagerEditComponent> {
  canDeactivate(component: AreaManagerEditComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.areaManagerForm.dirty) {
      const name = component.areaManagerForm.get('name')?.value || 'New AreaManager';
      return confirm(`bạn có chắc muốn thoát khỏi chỉnh sửa người quản lý khu vực ?`);
    }
    return true;
  }
}

