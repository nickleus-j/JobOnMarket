import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment.development';
import { CurrencyDto } from '../Currency/CurrencyDto';
@Injectable({ providedIn: 'root' })
export class CurrencyService {
  private http = inject(HttpClient); // Modern injection style
  private readonly API_URL = environment.apiUrl;

  getAll(): Observable<CurrencyDto[]> {
    return this.http.get<CurrencyDto[]>(this.API_URL + '/Currency');
  }
}