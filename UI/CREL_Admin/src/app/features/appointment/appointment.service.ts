import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Appointment } from './appointment.model';

@Injectable({
  providedIn: 'root',
})
export class AppointmentService {
  appointmentURL: string = environment.apiUrl + '/appointments';

  constructor(private httpClient: HttpClient) {}

  getAppointmentById(id): Observable<Appointment> {
    return this.httpClient.get<Appointment>(`${this.appointmentURL}/${id}`);
  }

  getAppointments(): Observable<Appointment[]> {
    return this.httpClient.get<Appointment[]>(
      `${this.appointmentURL}?OrderBy=2&PageSize=100`
    );
  }

  getActiveAppointments(): Observable<Appointment[]> {
    return this.httpClient.get<Appointment[]>(
      `${this.appointmentURL}?ListStatus=1&ListStatus=2&OrderBy=2&PageSize=100`
    );
  }

  getAppointmentByBrandId(brandId): Observable<Appointment[]> {
    return this.httpClient.get<Appointment[]>(
      `${this.appointmentURL}?BrandId=${brandId}&OrderBy=2&PageSize=100`
    );
  }

  getAppointmentByDate(minDate,maxDate): Observable<Appointment[]> {
    return this.httpClient.get<Appointment[]>(
      `${this.appointmentURL}?MinOnDateTime=${minDate}&MaxOnDateTime=${maxDate}&PageSize=100&OrderBy=2`
    );
  }

  insertAppointment(appointment: Appointment): Observable<Appointment> {
    return this.httpClient.post<Appointment>(
      `${this.appointmentURL}`,
      appointment
    );
  }

  addPropertiesIntoAppointment(id, propertiesIdList): Observable<Appointment> {
    return this.httpClient.post<Appointment>(
      `${this.appointmentURL}/${id}/properties`,
      propertiesIdList
    );
  }

  updateAppointment(appointment: Appointment): Observable<Appointment> {
    return this.httpClient.put<Appointment>(
      `${this.appointmentURL}`,
      appointment
    );
  }

  deleteAppointment(appointment: Appointment): Observable<Appointment> {
    appointment.status = 0;
    return this.httpClient.put<Appointment>(
      `${this.appointmentURL}`,
      appointment
    );
  }
}
