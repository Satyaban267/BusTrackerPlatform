import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
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
  isLoading = true;
  error = '';

  constructor(private busService: BusApiService) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.busService.getBuses().subscribe({
      next: (data) => {
        this.buses = data;
        this.filteredBuses = [...data];
        this.isLoading = false;
        this.error = '';
      },
      error: (err) => {
        this.error = 'Unable to load bus routes. Please try again later.';
        console.error('Error fetching buses', err);
        this.isLoading = false;
      }
    });
  }

  onSearch(): void {
    const term = this.searchTerm.trim().toLowerCase();
    if (!term) {
      this.filteredBuses = [...this.buses];
      return;
    }

    this.filteredBuses = this.buses.filter((bus) => {
      return (
        bus.operatorName.toLowerCase().includes(term) ||
        bus.route.toLowerCase().includes(term) ||
        bus.id.toString().includes(term)
      );
    });
  }

  onViewDetails(selectedBus: Bus): void {
    alert(`
      ${selectedBus.operatorName}
      Route: ${selectedBus.route}
      Price: $${selectedBus.generalPrice.toFixed(2)}
    `);
  }

  trackById(index: number, bus: Bus): number {
    return bus.id;
  }
}


