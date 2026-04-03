import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Bus {
  id: number;
  operatorName: string;
  route: string;
  generalPrice: number;
}

@Injectable({
  providedIn: 'root',
})
export class BusApiService {
  private apiUrl = 'http://localhost:5000/api/buses';

  constructor(private http: HttpClient) {}

  getBuses(): Observable<Bus[]> {
    return this.http.get<Bus[]>(this.apiUrl);
  }
}
