import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';

import { PropertyNewComponent } from './property-new.component';

@Injectable({
  providedIn: 'root'
})
export class PropertyNewGuard implements CanDeactivate<PropertyNewComponent> {
  canDeactivate(component: PropertyNewComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.propertyForm.dirty) {
      const name = component.propertyForm.get('name')?.value || 'New Property';
      return confirm(`bạn có chắc muốn thoát khỏi tạo mới bất động sản thương mại ?`);
    }
    return true;
  }
}
