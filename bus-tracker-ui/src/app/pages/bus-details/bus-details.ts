import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';
import { BusApiService, Bus } from '../../core/services/bus-api';
import { SuggestionApiService } from '../../core/services/suggestion-api.service';

@Component({
  selector: 'app-bus-details',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './bus-details.html',
  styleUrl: './bus-details.css',
})
export class BusDetails implements OnInit {
  bus: Bus | null = null;
  loading = true;
  error = false;
  errorMessage = '';

  // Return trip view state
  isReturnActive = false;

  // Form fields for suggesting via points
  submittedByName = '';
  submittedByEmail = '';
  viaPointsList: { name: string, time: string }[] = [{ name: '', time: '' }];
  suggestingLoading = false;
  suggestingSuccess = false;
  suggestingError = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private busApi: BusApiService,
    private suggestionApi: SuggestionApiService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    const idParam = this.route.snapshot.paramMap.get('id');
    const id = Number(idParam);
    if (!idParam || isNaN(id) || id <= 0) {
      this.error = true;
      this.loading = false;
      this.errorMessage = 'Invalid bus ID. Please return to the search page.';
      return;
    }

    this.busApi.getBusById(id)
      .pipe(finalize(() => {
        this.loading = false;
        this.cdr.markForCheck();
      }))
      .subscribe({
        next: (bus) => {
          this.bus = bus;
          this.cdr.markForCheck();
        },
        error: (err) => {
          console.error('Bus details error', err);
          this.error = true;
          this.errorMessage = err.status === 404
            ? 'This bus could not be found.'
            : 'Unable to load bus details. Please try again.';
          this.cdr.markForCheck();
        }
      });
  }

  goBack() {
    this.router.navigate(['/search']);
  }

  /** Split ViaPoints string into array for display, optionally in reverse order */
  getViaList(viaPoints: string | null | undefined, reverse: boolean = false): string[] {
    if (!viaPoints) return [];
    const list = viaPoints.split(',').map(v => v.trim()).filter(Boolean);
    return reverse ? list.reverse() : list;
  }

  /** Format contact number as a callable tel: link value */
  getCallableNumber(contact: string | null | undefined): string {
    if (!contact) return '';
    return contact.replace(/\s/g, '');
  }

  toggleDirection() {
    if (this.bus?.returnTime) {
      this.isReturnActive = !this.isReturnActive;
      this.cdr.markForCheck();
    }
  }

  addViaPoint() {
    this.viaPointsList.push({ name: '', time: '' });
  }

  removeViaPoint(index: number) {
    if (this.viaPointsList.length > 1) {
      this.viaPointsList.splice(index, 1);
    } else {
      this.viaPointsList[0] = { name: '', time: '' };
    }
  }

  submitViaSuggestion() {
    this.suggestingError = '';
    if (!this.bus) return;

    if (!this.submittedByName.trim()) {
      this.suggestingError = 'Please enter your name.';
      return;
    }

    const formattedVia = this.viaPointsList
      .map(v => v.name.trim() + (v.time.trim() ? ` (${v.time.trim()})` : ''))
      .filter(v => v !== '')
      .join(', ');

    if (!formattedVia) {
      this.suggestingError = 'Please add at least one via point.';
      return;
    }

    this.suggestingLoading = true;
    this.cdr.markForCheck();

    this.suggestionApi.submit({
      suggestedFrom: this.bus.origin,
      suggestedTo: this.bus.destination,
      viaPoints: formattedVia,
      reason: `Community via points suggestion for Bus: ${this.bus.serviceName} (ID: ${this.bus.id})`,
      submittedByName: this.submittedByName.trim(),
      submittedByEmail: this.submittedByEmail.trim() || null
    }).subscribe({
      next: () => {
        this.suggestingLoading = false;
        this.suggestingSuccess = true;
        this.submittedByName = '';
        this.submittedByEmail = '';
        this.viaPointsList = [{ name: '', time: '' }];
        this.cdr.markForCheck();
      },
      error: (err) => {
        this.suggestingLoading = false;
        this.suggestingError = err.error?.message || 'Failed to submit suggestion. Please try again.';
        this.cdr.markForCheck();
      }
    });
  }
}
