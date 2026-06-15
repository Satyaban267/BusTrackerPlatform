import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface LoginResponse {
  token: string;
  expiresAt: string;
  username: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly TOKEN_KEY = 'admin_token';
  private readonly USER_KEY = 'admin_user';
  private readonly EXPIRY_KEY = 'admin_token_expiry';

  constructor(private http: HttpClient, private router: Router) {}

  login(username: string, password: string): Observable<LoginResponse> {
    return this.http
      .post<LoginResponse>(`${environment.apiBaseUrl}/auth/login`, {
        username,
        password,
      })
      .pipe(
        tap((res) => {
          localStorage.setItem(this.TOKEN_KEY, res.token);
          localStorage.setItem(this.USER_KEY, res.username);
          localStorage.setItem(this.EXPIRY_KEY, res.expiresAt);
        })
      );
  }

  logout(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.USER_KEY);
    localStorage.removeItem(this.EXPIRY_KEY);
    this.router.navigate(['/admin/login']);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  getUsername(): string | null {
    return localStorage.getItem(this.USER_KEY);
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    const expiry = localStorage.getItem(this.EXPIRY_KEY);
    if (!token || !expiry) return false;
    return new Date(expiry) > new Date();
  }
}
