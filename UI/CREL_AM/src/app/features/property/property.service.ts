import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Project } from '../project/project.model';
import { Property } from './property.model';

@Injectable({
  providedIn: 'root',
})
export class PropertyService {
  propertyURL: string = environment.apiUrl + '/properties';

  constructor(private httpClient: HttpClient) {}

  getProperties(): Observable<any> {
    return this.httpClient.get<Property[]>(
      `${this.propertyURL}?OrderBy=10&PageSize=100&MinStatus=1`
    );
  }

  getPropertiesByLocation(id): Observable<any> {
    return this.httpClient.get<Property[]>(
      `${this.propertyURL}?OrderBy=10&PageSize=100&LocationId=${id}`
    );
  }

  getActiveProperties(): Observable<any> {
    return this.httpClient.get<Property[]>(
      `${this.propertyURL}?OrderBy=10&PageSize=100&Status=2`
    );
  }

  getPropertyById(id): Observable<Property> {
    return this.httpClient.get<Property>(`${this.propertyURL}/${id}`);
  }

  deleteProperty(property: Property): Observable<Property> {
    property.status = 0;
    return this.httpClient.put<Property>(`${this.propertyURL}`, property);
  }

  insertProperty(property: Property): Observable<Property> {
    return this.httpClient.post<Property>(`${this.propertyURL}`, property);
  }

  insertMediaForProperty(id, imgArray): Observable<Property> {
    return this.httpClient.post<Property>(
      `${this.propertyURL}/${id}/Media`,
      imgArray
    );
  }

  insertCertificationForProperty(id, imgArray): Observable<Property> {
    return this.httpClient.post<Property>(
      `${this.propertyURL}/${id}/certification`,
      imgArray
    );
  }

  insertOwnersForProperty(id, ownerIds): Observable<Property> {
    return this.httpClient.post<Property>(
      `${this.propertyURL}/${id}/Owners`,
      ownerIds
    );
  }

  updateProperty(property: Property): Observable<Property> {
    return this.httpClient.put<Property>(`${this.propertyURL}`, property);
  }
}
