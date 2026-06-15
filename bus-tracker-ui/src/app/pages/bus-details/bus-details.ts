import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { finalize } from 'rxjs';
import { BusApiService, Bus } from '../../core/services/bus-api';

@Component({
  selector: 'app-bus-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './bus-details.html',
  styleUrl: './bus-details.css',
})
export class BusDetails implements OnInit {
  bus: Bus | null = null;
  loading = true;
  error = false;
  errorMessage = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private busApi: BusApiService,
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

  /** Split ViaPoints string into array for display */
  getViaList(viaPoints: string | null | undefined): string[] {
    if (!viaPoints) return [];
    return viaPoints.split(',').map(v => v.trim()).filter(Boolean);
  }

  /** Format contact number as a callable tel: link value */
  getCallableNumber(contact: string | null | undefined): string {
    if (!contact) return '';
    return contact.replace(/\s/g, '');
  }
}
