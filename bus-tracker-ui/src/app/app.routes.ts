import { Routes } from '@angular/router';
import { BusListComponent } from './features/bus-list/bus-list';

export const routes: Routes = [
  // When the URL is exactly the root (e.g., localhost:4200/)
  { 
    path: '', 
    component: BusListComponent,
    title: 'Home - Bus Tracker' // Optional: Changes the browser tab name
  },
  
  // Later, you can add more pages here! For example:
  // { path: 'details/:id', component: BusDetailsComponent },
];