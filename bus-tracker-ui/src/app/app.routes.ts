import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { AdminLogin } from './pages/admin-login/admin-login';
import { AdminDashboard } from './pages/admin-dashboard/admin-dashboard';
import { Search } from './pages/search/search';
import { BusDetails } from './pages/bus-details/bus-details';
import { RegisterBus } from './pages/register-bus/register-bus';
import { SuggestBus } from './pages/suggest-bus/suggest-bus';
import { BusListComponent } from './features/bus-list/bus-list';
import { MainLayout } from './core/layouts/main-layout';
import { AdminLayout } from './core/layouts/admin-layout';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    component: MainLayout,
    children: [
      { path: '', component: Home, title: 'Gramin Path - Your Way Home' },
      { path: 'search', component: Search, title: 'Search Buses' },
      { path: 'buses', component: BusListComponent, title: 'All Buses' },
      { path: 'bus/:id', component: BusDetails, title: 'Bus Live Tracking' },
      { path: 'register', component: RegisterBus, title: 'Register Bus Service' },
      { path: 'suggest', component: SuggestBus, title: 'Suggest a Route' },
    ]
  },
  {
    path: 'admin',
    children: [
      { path: 'login', component: AdminLogin, title: 'Admin Login' },
      {
        path: '',
        component: AdminLayout,
        canActivate: [authGuard],
        children: [
          { path: 'dashboard', component: AdminDashboard, title: 'Service Approvals' },
        ]
      }
    ]
  },
  { path: '**', redirectTo: '' }
];