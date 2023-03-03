import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';

import { AreaManagerNewComponent } from './area-manager-new.component';

@Injectable({
  providedIn: 'root'
})
export class AreaManagerNewGuard implements CanDeactivate<AreaManagerNewComponent> {
  canDeactivate(component: AreaManagerNewComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.areaManagerForm.dirty) {
      const name = component.areaManagerForm.get('name')?.value || 'New AreaManager';
      return confirm(`bạn có chắc muốn thoát khỏi tạo mới người quản lý khu vực ?`);
    }
    return true;
  }
}
