import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';

import { BrandNewComponent } from './brand-new.component';

@Injectable({
  providedIn: 'root'
})
export class BrandNewGuard implements CanDeactivate<BrandNewComponent> {
  canDeactivate(component: BrandNewComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.brandForm.dirty) {
      const name = component.brandForm.get('name')?.value || 'New Brand';
      return confirm(`bạn có chắc muốn thoát khỏi tạo mới thương hiệu ?`);
    }
    return true;
  }
}
