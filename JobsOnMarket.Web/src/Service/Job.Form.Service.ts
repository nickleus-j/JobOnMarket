
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JobDto } from '../Job/JobDto';
import { environment } from '../environments/environment'; 

@Injectable({
  providedIn: 'root'
})
export class JobFormService {
  private readonly API_URL = environment.apiUrl; // Replace with your actual API URL

  constructor(private httpClient: HttpClient) {}

  /**
   * Create a new job
   * @param job JobDto object to create
   * @returns Observable of the created JobDto with assigned ID
   */
  create(job: JobDto): Observable<JobDto> {
    var token = localStorage.getItem(environment.Authentication_token_Name);
    return this.httpClient.post<JobDto>(
      `${this.API_URL}/Job`,
      job,{headers: {'Content-Type': 'application/json',Authorization: `Bearer ${token}`}}
    );
  }

  /**
   * Update an existing job
   * @param job JobDto object to update (must have id)
   * @returns Observable of the updated JobDto
   */
  update(job: JobDto): Observable<JobDto> {
    return this.httpClient.put<JobDto>(
      `${this.API_URL}/Job/${job.id}`,
      job
    );
  }

  /**
   * Get a job by ID
   * @param id Job ID
   * @returns Observable of the JobDto
   */
  getById(id: number): Observable<JobDto> {
    return this.httpClient.get<JobDto>(
      `${this.API_URL}/Job/${id}`
    );
  }

  /**
   * Get all jobs
   * @returns Observable array of JobDto
   */
  getAll(): Observable<JobDto[]> {
    return this.httpClient.get<JobDto[]>(
      `${this.API_URL}/Job`
    );
  }

  /**
   * Delete a job
   * @param id Job ID
   * @returns Observable of void
   */
  delete(id: number): Observable<void> {
    return this.httpClient.delete<void>(
      `${this.API_URL}/Job/${id}`
    );
  }
}