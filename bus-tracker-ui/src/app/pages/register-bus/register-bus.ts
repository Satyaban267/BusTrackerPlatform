import { Component, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RegistrationApiService } from '../../core/services/registration-api.service';

@Component({
  selector: 'app-register-bus',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register-bus.html',
  styleUrl: './register-bus.css',
})
export class RegisterBus {
  // Form fields
  serviceName = '';
  contactNumber = '';
  origin = '';
  destination = '';
  viaPointsList: { name: string, time: string }[] = [{ name: '', time: '' }];
  departureTime = '';
  returnTime = '';
  ownerName = '';
  ownerEmail = '';

  // State
  loading = false;
  submitted = false;
  errorMessage = '';

  constructor(
    private registrationApi: RegistrationApiService,
    private cdr: ChangeDetectorRef
  ) {}

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

  submit() {
    this.errorMessage = '';

    if (!this.serviceName.trim() || !this.origin.trim() || !this.destination.trim()
        || !this.departureTime.trim() || !this.ownerName.trim() || !this.ownerEmail.trim()) {
      this.errorMessage = 'Please fill in all required fields.';
      return;
    }

    const formattedVia = this.viaPointsList
      .map(v => v.name.trim() + (v.time.trim() ? ` (${v.time.trim()})` : ''))
      .filter(v => v !== '')
      .join(', ');

    this.loading = true;

    this.registrationApi.submit({
      serviceName: this.serviceName.trim(),
      contactNumber: this.contactNumber.trim() || null,
      origin: this.origin.trim(),
      destination: this.destination.trim(),
      viaPoints: formattedVia || null,
      departureTime: this.departureTime.trim(),
      returnTime: this.returnTime.trim() || null,
      submittedByName: this.ownerName.trim(),
      submittedByEmail: this.ownerEmail.trim(),
    }).subscribe({
      next: () => {
        this.loading = false;
        this.submitted = true;
        this.cdr.markForCheck();
      },
      error: (err) => {
        this.loading = false;
        this.errorMessage = err.error?.message || 'Failed to submit registration. Please try again.';
        this.cdr.markForCheck();
      }
    });
  }

  resetForm() {
    this.serviceName = '';
    this.contactNumber = '';
    this.origin = '';
    this.destination = '';
    this.viaPointsList = [{ name: '', time: '' }];
    this.departureTime = '';
    this.returnTime = '';
    this.ownerName = '';
    this.ownerEmail = '';
    this.submitted = false;
    this.errorMessage = '';
  }
}
