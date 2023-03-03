import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Admin } from "./admin.model";

@Injectable({
  providedIn: "root",
})
export class AdminService {
  baseUrl: string = environment.apiUrl + "/admins";
  constructor(private httpClient: HttpClient) {}

  createAdmin(admin: Admin): Observable<any> {
    return this.httpClient.post<Admin>(`${this.baseUrl}`, admin);
  }
  updateAdmin(admin: Admin): Observable<any> {
    return this.httpClient.put<Admin>(`${this.baseUrl}`, admin);
  }

  updateAvatarAdmin(id, file): Observable<any> {
    return this.httpClient.put<Admin>(`${this.baseUrl}/${id}/avatar`, file);
  }
  updateAdminPassword(id, password): Observable<any> {
    return this.httpClient.put<Admin>(
      `${this.baseUrl}/${id}/password`,
      JSON.stringify(password)
    );
  }

  getAdminById(id): Observable<Admin> {
    return this.httpClient.get<Admin>(`${this.baseUrl}/${id}`);
  }

  resetPassword(email): Observable<Admin> {
    return this.httpClient.put<Admin>(
      `${this.baseUrl}/reset-password`,
      JSON.stringify(email)
    );
  }
}
