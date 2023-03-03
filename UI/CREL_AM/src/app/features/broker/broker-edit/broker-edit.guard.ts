import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { BrokerEditComponent } from './broker-edit.component';


@Injectable({
  providedIn: 'root'
})
export class BrokerEditGuard implements CanDeactivate<BrokerEditComponent> {
  canDeactivate(component: BrokerEditComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.brokerForm.dirty) {
      const name = component.brokerForm.get('name')?.value || 'New Broker';
      return confirm(`bạn có chắc muốn thoát khỏi chỉnh sửa thương hiệu ?`);
    }
    return true;
  }
}
