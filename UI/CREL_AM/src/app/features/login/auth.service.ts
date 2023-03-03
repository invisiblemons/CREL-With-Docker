import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/switchMap';
import { environment } from 'src/environments/environment';
import { AreaManager } from '../area-manager/area-manager.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseURL: string = environment.apiUrl + '/area-managers/authenticate';

  constructor(private httpClient: HttpClient) {}
  login(account): Observable<any> {
    return this.httpClient.post<AreaManager>(`${this.baseURL}`, account);
  }
}
