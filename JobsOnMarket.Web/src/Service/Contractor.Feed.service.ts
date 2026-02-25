import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment.development';
import { ContractorDto} from '../Contractor/ContractorDto';
@Injectable({ providedIn: 'root' })
export class ContractorFeedService {
  private http = inject(HttpClient); // Modern injection style
  private readonly API_URL = environment.apiUrl;

  get(): Observable<ContractorDto[]> {
    return this.http.get<ContractorDto[]>(this.API_URL + '/Contractor?page=1&pageSize=10');
  }
}