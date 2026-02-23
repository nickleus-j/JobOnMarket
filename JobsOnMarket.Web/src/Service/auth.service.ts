// auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment'; 

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly API_URL = environment.apiUrl;
  public readonly AuthTokenName=environment.Authentication_token_Name;
  private authStatus = new BehaviorSubject<boolean>(this.hasToken());
    authStatus$ = this.authStatus.asObservable();
  constructor(private http: HttpClient) {}

  // Registration method
  register(userData: any): Observable<any> {
    return this.http.post(`${this.API_URL}/Auth/register`, userData).pipe(
      tap((res: any) => {
        if (res.token) {
          localStorage.setItem(this.AuthTokenName, res.token);
          this.authStatus.next(true);
        }
      })
    );
  }

  isAuthenticated(): boolean {
    return this.authStatus.value;
  }

  private hasToken(): boolean {
    return !!localStorage.getItem(this.AuthTokenName);
  }
  login(credentials: { userName: string; unhashedPassword: string }) {
    return this.http.post<{ token: string }>(`${this.API_URL}/Auth/login`, credentials).pipe(
      tap(res => {
        if (res.token) {
          localStorage.setItem(this.AuthTokenName, res.token); // Store JWT
        }
      })
    );
  }
  logout() {
    localStorage.removeItem(this.AuthTokenName);
    this.authStatus.next(false);
  }
  // Call this in your login/register 'next' block
  setLoggedIn(token: string) {
    localStorage.setItem('auth_token', token);
    this.authStatus.next(true);
  }
}
