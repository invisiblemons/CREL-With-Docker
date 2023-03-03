import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { IndustryEditComponent } from './industry-edit.component';


@Injectable({
  providedIn: 'root'
})
export class IndustryEditGuard implements CanDeactivate<IndustryEditComponent> {
  canDeactivate(component: IndustryEditComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.industryForm.dirty) {
      const name = component.industryForm.get('name')?.value || 'New Industry';
      return confirm(`bạn có chắc muốn thoát khỏi chỉnh sửa ngành kinh doanh ?`);
    }
    return true;
  }
}
