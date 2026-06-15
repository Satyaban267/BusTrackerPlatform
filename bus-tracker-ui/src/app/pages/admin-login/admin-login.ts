import { Component, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../core/services/auth.service';

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

  constructor(
    private router: Router,
    private authService: AuthService,
    private cdr: ChangeDetectorRef
  ) {}

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

    this.authService.login(this.identifier.trim(), this.password).subscribe({
      next: () => {
        this.loading = false;
        this.cdr.markForCheck();
        this.router.navigate(['/admin/dashboard']);
      },
      error: (err) => {
        this.loading = false;
        this.errorMessage = err.status === 401
          ? 'Invalid username or password. Please try again.'
          : 'Unable to connect to server. Please try later.';
        this.cdr.markForCheck();
      }
    });
  }
}
