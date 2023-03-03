import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { PropertyEditComponent } from './property-edit.component';


@Injectable({
  providedIn: 'root'
})
export class PropertyEditGuard implements CanDeactivate<PropertyEditComponent> {
  canDeactivate(component: PropertyEditComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.propertyForm.dirty) {
      const name = component.propertyForm.get('name')?.value || 'New Property';
      return confirm(`bạn có chắc muốn thoát khỏi chỉnh sửa bất động sản thương mại ?`);
    }
    return true;
  }
}
