import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';

import { AppointmentNewComponent } from './appointment-new.component';

@Injectable({
  providedIn: 'root'
})
export class AppointmentNewGuard implements CanDeactivate<AppointmentNewComponent> {
  canDeactivate(component: AppointmentNewComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.appointmentForm.dirty) {
      const name = component.appointmentForm.get('name')?.value || 'New Appointment';
      return confirm(`bạn có chắc muốn thoát khỏi tạo mới cuộc hẹn ?`);
    }
    return true;
  }
}
