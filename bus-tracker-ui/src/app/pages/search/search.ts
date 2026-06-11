import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BusApiService, Bus } from '../../core/services/bus-api';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './search.html',
  styleUrl: './search.css',
})
export class Search implements OnInit {
  buses: Bus[] = [];
  loading = true;
  from = '';
  to = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private busApi: BusApiService
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.from = params['from'] || '';
      this.to = params['to'] || '';
      this.loadBuses();
    });
  }

  loadBuses() {
    this.loading = true;
    this.busApi.getBuses(this.from, this.to).subscribe({
      next: (buses) => { this.buses = buses; this.loading = false; },
      error: () => { this.buses = []; this.loading = false; }
    });
  }

  goToBusDetail(id: number) {
    this.router.navigate(['/bus', id]);
  }

  goHome() {
    this.router.navigate(['/']);
  }

  getViaList(viaPoints: string | null | undefined): string[] {
    if (!viaPoints) return [];
    return viaPoints.split(',').map(v => v.trim()).filter(Boolean);
  }

  getCallableNumber(contact: string | null | undefined): string {
    if (!contact) return '';
    return contact.replace(/\s/g, '');
  }
}
