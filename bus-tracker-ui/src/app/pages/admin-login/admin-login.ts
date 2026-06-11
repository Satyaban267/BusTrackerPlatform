import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-admin-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './admin-login.html',
  styleUrl: './admin-login.css',
})
export class AdminLogin {
  identifier = '';   // accepts username OR email
  password = '';
  showPassword = false;
  loading = false;
  errorMessage = '';

  constructor(private router: Router, private http: HttpClient) {}

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  login() {
    this.errorMessage = '';

    if (!this.identifier.trim() || !this.password.trim()) {
      this.errorMessage = 'Please enter your username/email and password.';
      return;
    }

    this.loading = true;

    // The backend accepts "username" field — send whatever the user typed
    this.http.post<{ token: string; expiresAt: string; username: string }>(
      'http://localhost:5000/api/auth/login',
      { username: this.identifier.trim(), password: this.password }
    ).subscribe({
      next: (res) => {
        localStorage.setItem('admin_token', res.token);
        localStorage.setItem('admin_user', res.username);
        this.loading = false;
        this.router.navigate(['/admin/dashboard']);
      },
      error: (err) => {
        this.loading = false;
        this.errorMessage = err.status === 401
          ? 'Invalid username or password. Please try again.'
          : 'Unable to connect to server. Please try later.';
      }
    });
  }
}
