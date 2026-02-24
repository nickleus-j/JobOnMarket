import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment.development';
@Injectable({ providedIn: 'root' })
export class JobFeedService {
  private http = inject(HttpClient); // Modern injection style
  private readonly API_URL = environment.apiUrl;

  getJobs(): Observable<[]> {
    return this.http.get<[]>(this.API_URL + '/Job');
  }
  getJobsPage(page:number,pageSize:number): Observable<[]> {
    return this.http.get<[]>(this.API_URL + '/Job?page='+page+'&pageSize='+pageSize);
  }
}