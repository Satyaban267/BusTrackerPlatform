import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { BusApiService, Bus } from '../../core/services/bus-api';

@Component({
  selector: 'app-bus-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './bus-list.html',
  styleUrls: ['./bus-list.css']
})
export class BusListComponent implements OnInit {
  buses: Bus[] = [];
  filteredBuses: Bus[] = [];
  searchTerm = '';
  searchMessage = '';
  isLoading = true;
  error = '';

  constructor(
    private busService: BusApiService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.busService.getBuses().subscribe({
      next: (data) => {
        this.buses = data;
        this.filteredBuses = [...data];
        this.isLoading = false;
        this.error = '';
        this.cdr.markForCheck();
      },
      error: (err) => {
        this.error = 'Unable to load bus routes. Please try again later.';
        console.error('Error fetching buses', err);
        this.isLoading = false;
        this.cdr.markForCheck();
      }
    });
  }

  onSearch(): void {
    const term = this.searchTerm.trim().toLowerCase();
    if (!term) {
      if (this.searchTerm.length === 0) {
        this.filteredBuses = [...this.buses];
        this.searchMessage = '';
      } else {
        this.filteredBuses = [];
        this.searchMessage = 'Please enter a valid search term rather than whitespace.';
      }
      return;
    }

    this.searchMessage = '';
    this.filteredBuses = this.buses.filter((bus) =>
      bus.serviceName.toLowerCase().includes(term) ||
      bus.origin.toLowerCase().includes(term) ||
      bus.destination.toLowerCase().includes(term) ||
      (bus.viaPoints?.toLowerCase().includes(term) ?? false)
    );
  }

  clearSearch(): void {
    this.searchTerm = '';
    this.searchMessage = '';
    this.filteredBuses = [...this.buses];
  }

  onViewDetails(bus: Bus): void {
    this.router.navigate(['/bus', bus.id]);
  }

  trackById(index: number, bus: Bus): number {
    return bus.id;
  }
}
