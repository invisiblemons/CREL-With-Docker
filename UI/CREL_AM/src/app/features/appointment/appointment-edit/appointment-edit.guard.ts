import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { AppointmentEditComponent } from './appointment-edit.component';


@Injectable({
  providedIn: 'root'
})
export class AppointmentEditGuard implements CanDeactivate<AppointmentEditComponent> {
  canDeactivate(component: AppointmentEditComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.appointmentForm.dirty) {
      const name = component.appointmentForm.get('name')?.value || 'New Appointment';
      return confirm(`bạn có chắc muốn thoát khỏi chỉnh sửa cuộc hẹn ?`);
    }
    return true;
  }
}
