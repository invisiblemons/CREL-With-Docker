import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Owner } from './owner.model';

@Injectable({
  providedIn: 'root',
})
export class OwnerService {
  ownerURL: string = environment.apiUrl + '/owners';

  constructor(private httpClient: HttpClient) {}

  getOwners(): Observable<any> {
    return this.httpClient.get<Owner[]>(
      `${this.ownerURL}?OrderBy=2&PageSize=100&MinStatus=1`
    );
  }

  getOwnerById(id): Observable<Owner> {
    return this.httpClient.get<Owner>(`${this.ownerURL}/${id}`);
  }

  deleteOwner(owner: Owner): Observable<Owner> {
    owner.status = 0;
    return this.httpClient.put<Owner>(`${this.ownerURL}`, owner);
  }

  insertOwner(owner: Owner): Observable<Owner> {
    return this.httpClient.post<Owner>(`${this.ownerURL}`, owner);
  }

  updateOwner(owner: Owner): Observable<Owner> {
    return this.httpClient.put<Owner>(`${this.ownerURL}`, owner);
  }
}
