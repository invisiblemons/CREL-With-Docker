import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';

import { IndustryNewComponent } from './industry-new.component';

@Injectable({
  providedIn: 'root'
})
export class IndustryNewGuard implements CanDeactivate<IndustryNewComponent> {
  canDeactivate(component: IndustryNewComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.industryForm.dirty) {
      const name = component.industryForm.get('name')?.value || 'New Industry';
      return confirm(`bạn có chắc muốn thoát khỏi tạo mới ngành kinh doanh ?`);
    }
    return true;
  }
}
