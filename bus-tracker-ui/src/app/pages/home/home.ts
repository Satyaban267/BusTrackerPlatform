import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  from = '';
  to = '';

  constructor(private router: Router) {}

  /** Swaps the From and To field values */
  swapLocations() {
    const temp = this.from;
    this.from = this.to;
    this.to = temp;
  }

  /** Navigates to the search page with query params */
  findBus() {
    const from = this.from.trim();
    const to = this.to.trim();

    if (!from && !to) {
      // Shake the card to indicate empty fields
      const card = document.querySelector('.search-card') as HTMLElement;
      if (card) {
        card.classList.add('shake');
        setTimeout(() => card.classList.remove('shake'), 600);
      }
      return;
    }

    this.router.navigate(['/search'], {
      queryParams: { from, to }
    });
  }
}
