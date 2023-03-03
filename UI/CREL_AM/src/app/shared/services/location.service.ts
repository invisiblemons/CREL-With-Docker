import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { District } from "../models/district.model";
import { Location } from "../models/location.model";
import { StreetSegment } from "../models/streetSegment.model";
import { Ward } from "../models/ward.model";

@Injectable({
  providedIn: "root",
})
export class LocationService {
  districtURL: string = environment.apiUrl + "/districts";
  wardURL: string = environment.apiUrl + "/wards";
  streetURL: string = environment.apiUrl + "/street-segments";
  locationURL: string = environment.apiUrl + "/locations";

  constructor(private httpClient: HttpClient) {}

  //location
  insertLocation(location: Location): Observable<Location> {
    return this.httpClient.post<Location>(`${this.locationURL}`, location);
  }

  getLocationById(id): Observable<Location> {
    return this.httpClient.get<Location>(`${this.locationURL}/${id}`);
  }

  getLocationByLatLng(lat,lng): Observable<Location[]> {
    return this.httpClient.get<Location[]>(`${this.locationURL}?MinStatus=1&Latitude=${lat}&Longtitude=${lng}`);
  }

  //district
  getDistricts(): Observable<any> {
    return this.httpClient.get<District[]>(
      `${this.districtURL}?OrderBy=8&PageSize=1000`
    );
  }

  getDistrictById(id): Observable<District> {
    return this.httpClient.get<District>(`${this.districtURL}/${id}`);
  }

  deleteDistrict(district: District): Observable<District> {
    district.status = 0;
    return this.httpClient.put<District>(`${this.districtURL}`, district);
  }

  insertDistrict(district: District): Observable<District> {
    return this.httpClient.post<District>(`${this.districtURL}`, district);
  }

  updateDistrict(district: District): Observable<District> {
    return this.httpClient.put<District>(`${this.districtURL}`, district);
  }

  //ward
  getWards(districtId): Observable<Ward[]> {
    return this.httpClient.get<Ward[]>(
      `${this.wardURL}?DistrictId=${districtId}&OrderBy=8&PageSize=1000`
    );
  }

  getActiveWards(districtId): Observable<Ward[]> {
    return this.httpClient.get<Ward[]>(
      `${this.wardURL}?DistrictId=${districtId}&Status=1&OrderBy=8&PageSize=1000`
    );
  }

  getWardById(id): Observable<Ward> {
    return this.httpClient.get<Ward>(`${this.wardURL}/${id}`);
  }

  getWardsByTeamId(teamId): Observable<Ward[]> {
    return this.httpClient.get<Ward[]>(`${this.wardURL}?TeamId=${teamId}&PageSize=10000`);
  }

  deleteWard(Ward: Ward): Observable<Ward> {
    Ward.status = 0;
    return this.httpClient.put<Ward>(`${this.wardURL}`, Ward);
  }

  insertWard(Ward: Ward): Observable<Ward> {
    return this.httpClient.post<Ward>(`${this.wardURL}`, Ward);
  }

  updateWard(Ward: Ward): Observable<Ward> {
    return this.httpClient.put<Ward>(`${this.wardURL}`, Ward);
  }

  //street
  getStreetSegment(districtId): Observable<StreetSegment[]> {
    return this.httpClient.get<StreetSegment[]>(
      `${this.streetURL}?DistrictId=${districtId}&OrderBy=8&PageSize=1000`
    );
  }

  getStreetSegmentById(id): Observable<StreetSegment> {
    return this.httpClient.get<StreetSegment>(`${this.streetURL}/${id}`);
  }

  deleteStreetSegment(StreetSegment: StreetSegment){
    StreetSegment.status = 0;
    return this.httpClient.put<StreetSegment>(
      `${this.streetURL}`,
      StreetSegment
    );
  }

  insertStreetSegment(StreetSegment: StreetSegment): Observable<District> {
    return this.httpClient.post<District>(`${this.streetURL}`, StreetSegment);
  }

  updateStreetSegment(StreetSegment: StreetSegment): Observable<District> {
    return this.httpClient.put<District>(`${this.streetURL}`, StreetSegment);
  }

}
