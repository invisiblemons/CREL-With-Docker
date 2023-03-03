import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { Company } from "../models/brand-verification.model";

@Injectable({
  providedIn: "root",
})
export class BrandVerificationService {
  options = {
    headers: new HttpHeaders(),
  };
  baseURL: string = "https://thongtindoanhnghiep.co/api/company";

  constructor(private httpClient: HttpClient) {
    this.options.headers.append(
      "accept",
      "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9"
    );
  }

  getCompany(registerNumber): Observable<Company> {
    return this.httpClient.get<Company>(
      `${this.baseURL}/${registerNumber}`,
      this.options
    );
  }
}
