import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';

import { BrokerNewComponent } from './broker-new.component';

@Injectable({
  providedIn: 'root'
})
export class BrokerNewGuard implements CanDeactivate<BrokerNewComponent> {
  canDeactivate(component: BrokerNewComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.brokerForm.dirty) {
      const name = component.brokerForm.get('name')?.value || 'New Broker';
      return confirm(`bạn có chắc muốn thoát khỏi tạo mới thương hiệu ?`);
    }
    return true;
  }
}
