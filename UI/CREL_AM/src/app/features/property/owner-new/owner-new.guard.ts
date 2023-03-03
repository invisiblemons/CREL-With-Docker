import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { OwnerNewInPropertyComponent } from './owner-new.component';


@Injectable({
  providedIn: 'root'
})
export class OwnerNewGuard implements CanDeactivate<OwnerNewInPropertyComponent> {
  canDeactivate(component: OwnerNewInPropertyComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.ownerForm.dirty) {
      const name = component.ownerForm.get('name')?.value || 'New Owner';
      return confirm(`bạn có chắc muốn thoát khỏi tạo mới thông tin người sở hữu ?`);
    }
    return true;
  }
}
