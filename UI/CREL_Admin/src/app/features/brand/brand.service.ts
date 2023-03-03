import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Brand } from "./brand.model";

@Injectable({
  providedIn: "root",
})
export class BrandService {
  brandURL: string = environment.apiUrl + "/brands";

  constructor(private httpClient: HttpClient) {}

  getBrands(): Observable<any> {
    return this.httpClient.get<Brand[]>(
      `${this.brandURL}?OrderBy=2&PageSize=100`
    );
  }

  getActiveBrands(): Observable<any> {
    return this.httpClient.get<Brand[]>(
      `${this.brandURL}?OrderBy=2&PageSize=100&Status=2`
    );
  }

  getBrandById(id): Observable<Brand> {
    return this.httpClient.get<Brand>(`${this.brandURL}/${id}`);
  }

  getBrandByUserName(userName): Observable<Brand[]> {
    return this.httpClient.get<Brand[]>(
      `${this.brandURL}?UserName=${userName}&OrderBy=2`
    );
  }

  deleteBrand(brand: Brand): Observable<Brand> {
    brand.status = 0;
    return this.httpClient.put<Brand>(`${this.brandURL}`, brand);
  }

  // deleteBrand(brand:Brand) {
  //   return this.httpClient.delete<Brand>(`${this.brandURL}/${brand.id}`);
  // }

  insertBrand(brand: Brand): Observable<Brand> {
    return this.httpClient.post<Brand>(`${this.brandURL}`, brand);
  }

  updateBrand(brand: Brand): Observable<Brand> {
    return this.httpClient.put<Brand>(`${this.brandURL}`, brand);
  }

  updateBrandAvatar(id, file): Observable<Brand> {
    return this.httpClient.put<Brand>(`${this.brandURL}/${id}/avatar`, file);
  }

  updateBrandPassword(id, password): Observable<any> {
    const headers = new HttpHeaders({ "Content-Type": "application/json" });
    return this.httpClient.put<Brand>(
      `${this.brandURL}/${id}/password`,
      JSON.stringify(password),
      { headers }
    );
  }

  resetPassword(email): Observable<Brand> {
    const headers = new HttpHeaders({ "Content-Type": "application/json" });
    return this.httpClient.put<Brand>(
      `${this.brandURL}/reset-password`,
      JSON.stringify(email),
      { headers }
    );
  }
}
