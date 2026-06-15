import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface BusStop {
  id: number;
  stationName: string;
  arrivalTime: string | null;
  departureTime: string | null;
  stopOrder: number;
}

export interface Bus {
  id: number;
  serviceName: string;
  contactNumber: string | null;
  origin: string;
  destination: string;
  viaPoints: string | null;
  departureTime: string;
  returnTime: string | null;
  isActive: boolean;
  stops: BusStop[];
}

@Injectable({
  providedIn: 'root',
})
export class BusApiService {
  private apiUrl = `${environment.apiBaseUrl}/buses`;

  constructor(private http: HttpClient) {}

  getBuses(from?: string, to?: string): Observable<Bus[]> {
    let url = this.apiUrl;
    const params: string[] = [];
    if (from) params.push(`from=${encodeURIComponent(from)}`);
    if (to) params.push(`to=${encodeURIComponent(to)}`);
    if (params.length) url += '?' + params.join('&');
    return this.http.get<Bus[]>(url);
  }

  getBusById(id: number): Observable<Bus> {
    return this.http.get<Bus>(`${this.apiUrl}/${id}`);
  }
}
