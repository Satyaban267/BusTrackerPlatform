import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface RouteSuggestionCreate {
  suggestedFrom: string;
  suggestedTo: string;
  viaPoints: string | null;
  reason: string | null;
  submittedByName: string;
  submittedByEmail: string | null;
}

export interface RouteSuggestion {
  id: number;
  suggestedFrom: string;
  suggestedTo: string;
  viaPoints: string | null;
  reason: string | null;
  submittedByName: string;
  submittedByEmail: string | null;
  status: string;
  submittedAt: string;
}

@Injectable({
  providedIn: 'root',
})
export class SuggestionApiService {
  private apiUrl = `${environment.apiBaseUrl}/suggestions`;

  constructor(private http: HttpClient) {}

  submit(data: RouteSuggestionCreate): Observable<RouteSuggestion> {
    return this.http.post<RouteSuggestion>(this.apiUrl, data);
  }

  getAll(): Observable<RouteSuggestion[]> {
    return this.http.get<RouteSuggestion[]>(this.apiUrl);
  }

  getById(id: number): Observable<RouteSuggestion> {
    return this.http.get<RouteSuggestion>(`${this.apiUrl}/${id}`);
  }

  updateStatus(id: number, status: string): Observable<RouteSuggestion> {
    return this.http.put<RouteSuggestion>(`${this.apiUrl}/${id}/status`, {
      status,
    });
  }
}
