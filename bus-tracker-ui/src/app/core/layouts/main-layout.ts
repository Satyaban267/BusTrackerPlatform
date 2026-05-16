import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { Header } from '../../shared/header/header';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, Header],
  template: `
    <app-header></app-header>
    <main class="animate-fade">
      <router-outlet></router-outlet>
    </main>
    <footer class="main-footer container py-xl mt-xxl border-t">
      <div class="flex justify-between items-center">
        <p class="text-muted text-small">© 2026 Gramin Path. Your way home.</p>
        <div class="flex gap-md">
          <a href="#" class="text-muted text-small">Privacy</a>
          <a href="#" class="text-muted text-small">Terms</a>
        </div>
      </div>
    </footer>
  `,
  styles: [`
    :host { display: block; min-height: 100vh; }
    main { min-height: 80vh; }
    .border-t { border-top: 1px solid var(--color-border); }
    .py-xl { padding-top: var(--spacing-xl); padding-bottom: var(--spacing-xl); }
    .mt-xxl { margin-top: var(--spacing-xxl); }
  `]
})
export class MainLayout {}
