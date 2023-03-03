import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AreaManager } from './area-manager.model';

@Injectable({
  providedIn: 'root'
})
export class AreaManagerService {

  areaManagerURL: string = environment.apiUrl + '/area-managers';

  constructor(private httpClient: HttpClient) { }

  getAreaManagers(): Observable<any> {
    return this.httpClient.get<AreaManager[]>(`${this.areaManagerURL}?OrderBy=2&PageSize=100`);
  }

  getAreaManagerById(id): Observable<AreaManager> {
    return this.httpClient.get<AreaManager>(`${this.areaManagerURL}/${id}`);
  }

  getAreaManagerByUserName(userName): Observable<AreaManager[]> {
    return this.httpClient.get<AreaManager[]>(`${this.areaManagerURL}?UserName=${userName}&OrderBy=2`);
  }

  deleteAreaManager(areaManager:AreaManager): Observable<AreaManager> {
    areaManager.status = 0;
    return this.httpClient.put<AreaManager>(`${this.areaManagerURL}`, areaManager);
  }

  insertAreaManager(areaManager: AreaManager): Observable<AreaManager>  {
    return this.httpClient.post<AreaManager>(`${this.areaManagerURL}`, areaManager);
  }

  updateAreaManager(areaManager: AreaManager): Observable<AreaManager>  {
    return this.httpClient.put<AreaManager>(`${this.areaManagerURL}`, areaManager);
  }

  updateAreaManagerAvatar(id,file): Observable<AreaManager> {
    return this.httpClient.put<AreaManager>(`${this.areaManagerURL}/${id}/avatar`, file);
  }

  updateAreaManagerPassword(id, password): Observable<any> {
    const headers = new HttpHeaders({ "Content-Type": "application/json" });
    return this.httpClient.put<AreaManager>(
      `${this.areaManagerURL}/${id}/password`,
      JSON.stringify(password),
      { headers }
    );
  }

  resetPassword(email): Observable<any> {
    const headers = new HttpHeaders({ "Content-Type": "application/json" });
    return this.httpClient.put<AreaManager>(
      `${this.areaManagerURL}/reset-password`,
      JSON.stringify(email),
      { headers }
    );
  }
}
