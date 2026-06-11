import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
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

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private busApi: BusApiService
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (!id) { this.error = true; this.loading = false; return; }

    this.busApi.getBusById(id).subscribe({
      next: (bus) => { this.bus = bus; this.loading = false; },
      error: () => { this.error = true; this.loading = false; }
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
