import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AreaManager } from './area-manager.model';

@Injectable({
  providedIn: 'root',
})
export class AreaManagerService {
  areaManagerURL: string = environment.apiUrl + '/area-managers';

  constructor(private httpClient: HttpClient) {}

  getAreaManagers(): Observable<any> {
    return this.httpClient.get<AreaManager[]>(
      `${this.areaManagerURL}?OrderBy=2&PageSize=100&MinStatus=1`
    );
  }

  getAreaManagerById(id): Observable<AreaManager> {
    return this.httpClient.get<AreaManager>(`${this.areaManagerURL}/${id}`);
  }

  deleteAreaManager(areaManager: AreaManager): Observable<AreaManager> {
    areaManager.status = 0;
    return this.httpClient.put<AreaManager>(
      `${this.areaManagerURL}`,
      areaManager
    );
  }

  insertAreaManager(areaManager: AreaManager): Observable<AreaManager> {
    return this.httpClient.post<AreaManager>(
      `${this.areaManagerURL}`,
      areaManager
    );
  }

  updateAreaManager(areaManager: AreaManager): Observable<AreaManager> {
    return this.httpClient.put<AreaManager>(
      `${this.areaManagerURL}`,
      areaManager
    );
  }

  updateAreaManagerAvatar(file): Observable<AreaManager> {
    return this.httpClient.put<AreaManager>(
      `${this.areaManagerURL}/profile/avatar`,
      file
    );
  }

  updateAreaManagerPassword(password): Observable<any> {
    const headers = new HttpHeaders({ "Content-Type": "application/json" });
    return this.httpClient.put<AreaManager>(
      `${this.areaManagerURL}/profile/password`,
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
