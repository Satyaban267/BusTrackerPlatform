import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface BusRegistrationCreate {
  serviceName: string;
  contactNumber: string | null;
  origin: string;
  destination: string;
  viaPoints: string | null;
  departureTime: string;
  returnTime: string | null;
  submittedByName: string;
  submittedByEmail: string;
}

export interface BusRegistration {
  id: number;
  serviceName: string;
  contactNumber: string | null;
  origin: string;
  destination: string;
  viaPoints: string | null;
  departureTime: string;
  returnTime: string | null;
  submittedByName: string;
  submittedByEmail: string;
  status: string;
  submittedAt: string;
  adminRemarks: string | null;
}

@Injectable({
  providedIn: 'root',
})
export class RegistrationApiService {
  private apiUrl = `${environment.apiBaseUrl}/registrations`;

  constructor(private http: HttpClient) {}

  submit(data: BusRegistrationCreate): Observable<BusRegistration> {
    return this.http.post<BusRegistration>(this.apiUrl, data);
  }

  getAll(): Observable<BusRegistration[]> {
    return this.http.get<BusRegistration[]>(this.apiUrl);
  }

  getById(id: number): Observable<BusRegistration> {
    return this.http.get<BusRegistration>(`${this.apiUrl}/${id}`);
  }

  updateStatus(id: number, status: string, adminRemarks?: string): Observable<BusRegistration> {
    return this.http.put<BusRegistration>(`${this.apiUrl}/${id}/status`, {
      status,
      adminRemarks: adminRemarks ?? null,
    });
  }
}
