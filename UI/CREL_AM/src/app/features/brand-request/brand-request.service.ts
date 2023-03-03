import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BrandRequest } from './brand-request.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BrandRequestService {
  requestURL: string = environment.apiUrl + '/brand-requests';

  constructor(private httpClient: HttpClient) { }

  getRequests(): Observable<any> {
    return this.httpClient.get<BrandRequest[]>(`${this.requestURL}?MinStatus=1&OrderBy=2&PageSize=100`);
  }
  getRequestById(id): Observable<BrandRequest> {
    return this.httpClient.get<BrandRequest>(`${this.requestURL}/${id}`);
  }

  insertRequest(brandRequest: BrandRequest): Observable<BrandRequest>  {
    return this.httpClient.post<BrandRequest>(`${this.requestURL}`, brandRequest);
  }

  updateRequest(request): Observable<any> {
    return this.httpClient.put<Request>(
      `${this.requestURL}`, request
    );
  }

  handlingRequest(requestId, propertiesID): Observable<BrandRequest> {
    return this.httpClient.post<BrandRequest>(`${this.requestURL}/${requestId}/properties`, propertiesID);
  }
}
