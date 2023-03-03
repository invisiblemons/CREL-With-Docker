import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Broker } from './broker.model';

@Injectable({
  providedIn: 'root'
})
export class BrokerService {

  brokerURL: string = environment.apiUrl + '/brokers';

  constructor(private httpClient: HttpClient) { }

  getBrokers(): Observable<any> {
    return this.httpClient.get<Broker[]>(`${this.brokerURL}?OrderBy=2&PageSize=100&MinStatus=1`);
  }

  getBrokersByTeamId(id): Observable<any> {
    return this.httpClient.get<Broker[]>(`${this.brokerURL}?OrderBy=2&PageSize=100&MinStatus=1&TeamId=${id}`);
  }

  getActiveBrokers(): Observable<any> {
    return this.httpClient.get<Broker[]>(`${this.brokerURL}?MinStatus=1&OrderBy=2&PageSize=100`);
  }

  getBrokersOfTeam(teamId): Observable<any> {
    return this.httpClient.get<Broker[]>(`${this.brokerURL}?Mintatus=1&OrderBy=2&PageSize=100&TeamId=${teamId}`);
  }

  getBrokerById(id): Observable<Broker> {
    return this.httpClient.get<Broker>(`${this.brokerURL}/${id}`);
  }

  getBrokerByUserName(userName): Observable<Broker[]> {
    return this.httpClient.get<Broker[]>(`${this.brokerURL}?UserName=${userName}&OrderBy=2&MinStatus=1`);
  }

  deleteBroker(broker:Broker): Observable<Broker> {
    broker.status = 0;
    return this.httpClient.put<Broker>(`${this.brokerURL}`, broker);
  }

  insertBroker(broker: Broker): Observable<Broker>  {
    return this.httpClient.post<Broker>(`${this.brokerURL}`, broker);
  }

  updateBroker(broker: Broker): Observable<Broker>  {
    return this.httpClient.put<Broker>(`${this.brokerURL}`, broker);
  }

  updateBrokerAvatar(id,file): Observable<Broker> {
    return this.httpClient.put<Broker>(`${this.brokerURL}/${id}/avatar`, file);
  }

  updateBrokerPassword(id, password): Observable<any> {
    const headers = new HttpHeaders({ "Content-Type": "application/json" });
    return this.httpClient.put<Broker>(
      `${this.brokerURL}/${id}/password`,
      JSON.stringify(password),
      { headers }
    );
  }

  resetPassword(email): Observable<any> {
    const headers = new HttpHeaders({ "Content-Type": "application/json" });
    return this.httpClient.put<Broker>(
      `${this.brokerURL}/reset-password`,
      JSON.stringify(email),
      { headers }
    );
  }
}
