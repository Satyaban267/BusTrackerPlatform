import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RegistrationApiService, BusRegistration } from '../../core/services/registration-api.service';
import { SuggestionApiService, RouteSuggestion } from '../../core/services/suggestion-api.service';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.css',
})
export class AdminDashboard implements OnInit {
  activeTab: 'registrations' | 'suggestions' = 'registrations';

  registrations: BusRegistration[] = [];
  suggestions: RouteSuggestion[] = [];

  loadingRegistrations = false;
  loadingSuggestions = false;
  actionLoading: { [key: string]: boolean } = {};
  errorMessage = '';

  // Remarks modal
  showRemarksModal = false;
  remarksTargetId = 0;
  remarksTargetAction = '';
  adminRemarks = '';

  constructor(
    private registrationApi: RegistrationApiService,
    private suggestionApi: SuggestionApiService,
    public authService: AuthService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.loadRegistrations();
    this.loadSuggestions();
  }

  switchTab(tab: 'registrations' | 'suggestions') {
    this.activeTab = tab;
  }

  loadRegistrations() {
    this.loadingRegistrations = true;
    this.registrationApi.getAll().subscribe({
      next: (data) => {
        this.registrations = data;
        this.loadingRegistrations = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.loadingRegistrations = false;
        this.errorMessage = 'Failed to load registrations.';
        this.cdr.markForCheck();
      }
    });
  }

  loadSuggestions() {
    this.loadingSuggestions = true;
    this.suggestionApi.getAll().subscribe({
      next: (data) => {
        this.suggestions = data;
        this.loadingSuggestions = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.loadingSuggestions = false;
        this.errorMessage = 'Failed to load suggestions.';
        this.cdr.markForCheck();
      }
    });
  }

  get pendingRegistrations() {
    return this.registrations.filter(r => r.status === 'Pending');
  }

  get pendingSuggestions() {
    return this.suggestions.filter(s => s.status === 'Pending');
  }

  get totalPending() {
    return this.pendingRegistrations.length + this.pendingSuggestions.length;
  }

  openRemarksModal(id: number, action: string) {
    this.remarksTargetId = id;
    this.remarksTargetAction = action;
    this.adminRemarks = '';
    this.showRemarksModal = true;
  }

  confirmAction() {
    this.showRemarksModal = false;
    this.updateRegistrationStatus(this.remarksTargetId, this.remarksTargetAction, this.adminRemarks);
  }

  updateRegistrationStatus(id: number, status: string, remarks?: string) {
    const key = `reg-${id}`;
    this.actionLoading[key] = true;
    this.registrationApi.updateStatus(id, status, remarks).subscribe({
      next: () => {
        this.actionLoading[key] = false;
        this.loadRegistrations();
        this.cdr.markForCheck();
      },
      error: () => {
        this.actionLoading[key] = false;
        this.errorMessage = `Failed to ${status.toLowerCase()} registration.`;
        this.cdr.markForCheck();
      }
    });
  }

  updateSuggestionStatus(id: number, status: string) {
    const key = `sug-${id}`;
    this.actionLoading[key] = true;
    this.suggestionApi.updateStatus(id, status).subscribe({
      next: () => {
        this.actionLoading[key] = false;
        this.loadSuggestions();
        this.cdr.markForCheck();
      },
      error: () => {
        this.actionLoading[key] = false;
        this.errorMessage = `Failed to ${status.toLowerCase()} suggestion.`;
        this.cdr.markForCheck();
      }
    });
  }

  logout() {
    this.authService.logout();
  }
}
