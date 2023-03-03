import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AngularFireAuth } from "@angular/fire/auth";
import { Router } from "@angular/router";
import * as firebase from "firebase/app";
import { Observable } from "rxjs";
import "rxjs/add/operator/switchMap";
import { environment } from "src/environments/environment";
import { Admin } from "../admin/admin.model";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  baseURL: string = environment.apiUrl + "/admins/authenticate";

  constructor(private httpClient: HttpClient) {}
  login(account): Observable<any> {
    return this.httpClient.post<Admin>(`${this.baseURL}`, account);
  }
}
