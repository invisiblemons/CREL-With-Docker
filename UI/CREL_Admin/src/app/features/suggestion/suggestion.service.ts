import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Suggestion } from './suggestion.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SuggestionService {
  suggestionURL: string = environment.apiUrl + '/brands';

  constructor(
    private httpClient: HttpClient,
  ) {}

  
  insertSuggestion(propertyIdList: number[], brandId): Observable<Suggestion> {
    return this.httpClient.post<Suggestion>(
      `${this.suggestionURL}/${brandId}/recommend-properties`,
      propertyIdList
    );
  }

}
