import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { OwnerEditComponent } from './owner-edit.component';


@Injectable({
  providedIn: 'root'
})
export class OwnerEditGuard implements CanDeactivate<OwnerEditComponent> {
  canDeactivate(component: OwnerEditComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.ownerForm.dirty) {
      const name = component.ownerForm.get('name')?.value || 'New Owner';
      return confirm(`bạn có chắc muốn thoát khỏi chỉnh sửa thông tin người sở hữu ?`);
    }
    return true;
  }
}
