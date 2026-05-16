import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterLink } from '@angular/router';

@Component({
  selector: 'app-admin-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink],
  template: `
    <div class="admin-container flex">
      <aside class="admin-sidebar glass-dark p-xl">
        <div class="logo mb-xxl flex items-center gap-sm">
          <span class="material-symbols-rounded icon-filled" style="color: var(--color-primary); font-size: 32px;">dashboard</span>
          <h2 class="h3" style="color: white; margin: 0;">Admin Portal</h2>
        </div>
        <nav class="flex flex-col gap-md">
          <a routerLink="/admin/dashboard" class="admin-nav-link" routerLinkActive="active">
            <span class="material-symbols-rounded">analytics</span> Dashboard
          </a>
          <a routerLink="/admin/services" class="admin-nav-link" routerLinkActive="active">
            <span class="material-symbols-rounded">bus_alert</span> Service Approvals
          </a>
          <a routerLink="/admin/users" class="admin-nav-link" routerLinkActive="active">
            <span class="material-symbols-rounded">group</span> User Management
          </a>
          <div class="flex-1"></div>
          <a routerLink="/" class="admin-nav-link mt-auto">
            <span class="material-symbols-rounded">logout</span> Exit Admin
          </a>
        </nav>
      </aside>
      <main class="admin-main flex-1 p-xxl animate-slide-up">
        <router-outlet></router-outlet>
      </main>
    </div>
  `,
  styles: [`
    .admin-container { min-height: 100vh; background: #fdf2ef; }
    .admin-sidebar { width: 300px; min-height: 100vh; position: sticky; top: 0; }
    .admin-nav-link {
      display: flex;
      items-center: center;
      gap: var(--spacing-md);
      padding: 1rem;
      border-radius: var(--radius-md);
      color: rgba(255, 255, 255, 0.7);
      text-decoration: none;
      font-weight: 600;
      transition: var(--transition-base);
    }
    .admin-nav-link:hover, .admin-nav-link.active {
      color: white;
      background: rgba(255, 255, 255, 0.1);
    }
    .admin-nav-link.active {
      background: var(--color-primary);
      box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
    }
    .logo h2 { font-family: var(--font-family-display); }
  `]
})
export class AdminLayout {}
