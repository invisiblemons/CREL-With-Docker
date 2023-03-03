import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { AreaManagerTeam, Team } from "./team.model";

@Injectable({
  providedIn: "root",
})
export class TeamService {
  teamURL: string = environment.apiUrl + "/teams";

  constructor(private httpClient: HttpClient) {}

  getTeams(): Observable<any> {
    return this.httpClient.get<Team[]>(
      `${this.teamURL}?OrderBy=2&PageSize=100`
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

  insertAreaManagerIntoTeam(
    amIdList: number[],
    teamId
  ): Observable<AreaManagerTeam[]> {
    return this.httpClient.post<AreaManagerTeam[]>(
      `${this.teamURL}/${teamId}/area-managers`,
      amIdList
    );
  }

  updateTeam(team: Team): Observable<Team> {
    return this.httpClient.put<Team>(`${this.teamURL}`, team);
  }

  deleteAreaManagerIntoTeam(amIdList, teamId) {
    const options = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
      }),
      body: amIdList,
    };
    return this.httpClient.delete<any>(
      `${this.teamURL}/${teamId}/area-managers`,
      options
    );
  }
}
