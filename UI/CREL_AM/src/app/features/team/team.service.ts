import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Team } from './team.model';

@Injectable({
  providedIn: 'root',
})
export class TeamService {
  teamURL: string = environment.apiUrl + '/teams';

  constructor(private httpClient: HttpClient) {}

  getTeams(): Observable<any> {
    return this.httpClient.get<Team[]>(
      `${this.teamURL}?OrderBy=2&PageSize=100&MinStatus=1`
    );
  }

  getTeamById(id): Observable<Team> {
    return this.httpClient.get<Team>(`${this.teamURL}/${id}`);
  }

  deleteTeam(team: Team): Observable<Team> {
    team.status = 0;
    return this.httpClient.put<Team>(`${this.teamURL}`, team);
  }

  insertTeam(team: Team): Observable<Team> {
    return this.httpClient.post<Team>(`${this.teamURL}`, team);
  }

  updateTeam(team: Team): Observable<Team> {
    return this.httpClient.put<Team>(`${this.teamURL}`, team);
  }
}
