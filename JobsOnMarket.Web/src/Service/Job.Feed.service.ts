import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment.development';
import { JobDetailDto } from '../Job/JobDetailDto';
@Injectable({ providedIn: 'root' })
export class JobFeedService {
  private http = inject(HttpClient); // Modern injection style
  private readonly API_URL = environment.apiUrl;

  getJobs(): Observable<[]> {
    return this.http.get<[]>(this.API_URL + '/Job');
  }
  getJob(id:number): Observable<JobDetailDto> {
    return this.http.get<JobDetailDto>(this.API_URL + '/Job/'+id);
  }
  getJobsPage(page:number,pageSize:number): Observable<[]> {
    return this.http.get<[]>(this.API_URL + '/Job?page='+page+'&pageSize='+pageSize);
  }
}