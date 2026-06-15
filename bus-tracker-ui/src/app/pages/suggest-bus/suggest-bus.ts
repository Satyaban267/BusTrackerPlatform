import { Component, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SuggestionApiService } from '../../core/services/suggestion-api.service';

@Component({
  selector: 'app-suggest-bus',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './suggest-bus.html',
  styleUrl: './suggest-bus.css',
})
export class SuggestBus {
  // Form fields
  submittedByName = '';
  suggestedFrom = '';
  suggestedTo = '';
  viaPoints = '';
  reason = '';
  submittedByEmail = '';

  // State
  loading = false;
  submitted = false;
  errorMessage = '';

  constructor(
    private suggestionApi: SuggestionApiService,
    private cdr: ChangeDetectorRef
  ) {}

  submit() {
    this.errorMessage = '';

    if (!this.submittedByName.trim() || !this.suggestedFrom.trim() || !this.suggestedTo.trim()) {
      this.errorMessage = 'Please fill in your name, source, and destination.';
      return;
    }

    this.loading = true;

    this.suggestionApi.submit({
      suggestedFrom: this.suggestedFrom.trim(),
      suggestedTo: this.suggestedTo.trim(),
      viaPoints: this.viaPoints.trim() || null,
      reason: this.reason.trim() || null,
      submittedByName: this.submittedByName.trim(),
      submittedByEmail: this.submittedByEmail.trim() || null,
    }).subscribe({
      next: () => {
        this.loading = false;
        this.submitted = true;
        this.cdr.markForCheck();
      },
      error: (err) => {
        this.loading = false;
        this.errorMessage = err.error?.message || 'Failed to submit suggestion. Please try again.';
        this.cdr.markForCheck();
      }
    });
  }

  resetForm() {
    this.submittedByName = '';
    this.suggestedFrom = '';
    this.suggestedTo = '';
    this.viaPoints = '';
    this.reason = '';
    this.submittedByEmail = '';
    this.submitted = false;
    this.errorMessage = '';
  }
}
