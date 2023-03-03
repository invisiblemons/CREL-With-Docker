import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Industry } from './industry.model';

@Injectable({
  providedIn: 'root',
})
export class IndustryService {
  industryURL: string = environment.apiUrl + '/industries';

  constructor(private httpClient: HttpClient) {}

  getIndustries(): Observable<any> {
    return this.httpClient.get<Industry[]>(
      `${this.industryURL}?OrderBy=2&PageSize=100&MinStatus=1`
    );
  }

  getIndustryById(id): Observable<Industry> {
    return this.httpClient.get<Industry>(`${this.industryURL}/${id}`);
  }

  deleteIndustry(industry: Industry): Observable<Industry> {
    industry.status = 0;
    return this.httpClient.put<Industry>(`${this.industryURL}`, industry);
  }

  insertIndustry(industry: Industry): Observable<Industry> {
    return this.httpClient.post<Industry>(`${this.industryURL}`, industry);
  }

  updateIndustry(industry: Industry): Observable<Industry> {
    return this.httpClient.put<Industry>(`${this.industryURL}`, industry);
  }
}
