import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Media } from "../models/media.model";

@Injectable({
  providedIn: "root",
})
export class MediaService {
  mediaURL: string = environment.apiUrl + "/media";
  constructor(private httpClient: HttpClient) {}

  deleteMedia(idArray: number[]): Observable<any> {
    const options = {
      headers: new HttpHeaders(),
      body: idArray
    };
    return this.httpClient.delete<any>(`${this.mediaURL}`, options);
  }
}
