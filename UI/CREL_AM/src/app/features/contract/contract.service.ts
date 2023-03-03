import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Contract } from '../contract/contract.model';

@Injectable({
  providedIn: 'root'
})
export class ContractService {
  contractURL: string = environment.apiUrl + '/contracts';

  constructor(private httpClient: HttpClient) { }

  getContracts(): Observable<any> {
    return this.httpClient.get<Contract[]>(`${this.contractURL}?OrderBy=2&PageSize=100`);
  }

  getActiveContracts(): Observable<any> {
    return this.httpClient.get<Contract[]>(`${this.contractURL}?Status=2&OrderBy=2&PageSize=100`);
  }

  getMaxEndDateContracts(maxDate: string): Observable<any> {
    return this.httpClient.get<Contract[]>(`${this.contractURL}?MaxEndDate=${maxDate}&Status=2&OrderBy=2&PageSize=100`);
  }

  getContractById(id): Observable<Contract> {
    return this.httpClient.get<Contract>(`${this.contractURL}/${id}`);
  }

  deleteContract(contract:Contract): Observable<Contract> {
    contract.status = 0;
    return this.httpClient.put<Contract>(`${this.contractURL}`, contract);
  }

  insertContract(contract: Contract): Observable<Contract>  {
    return this.httpClient.post<Contract>(`${this.contractURL}`, contract);
  }

  updateContract(contract: Contract): Observable<Contract>  {
    return this.httpClient.put<Contract>(`${this.contractURL}`, contract);
  }

  downFile(id): Observable<any> {
    return this.httpClient.get<any>(`${this.contractURL}/${id}/word-file`, {
      responseType: "blob" as "json",
    });
  }
}
