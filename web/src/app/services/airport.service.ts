import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Airport } from '../models/airport';


@Injectable({
  providedIn: 'root'
})
export class AirportService {

  readonly baseUrl = "https://localhost:5001/api/";

  constructor(private http: HttpClient) { }

  getAirport(address: string, distance: number, typeAirport: number): Observable<Airport[]> {

    const body = {
      "Address": address,
      "Distance": distance,
      "TypeAirport": typeAirport,
    }

    return this.http.post<Airport[]>(`${this.baseUrl}airport`, body)
      .pipe(
        map(data => {
          return data.map(a => {
            a.latitude = parseFloat(a.latitude.toString());
            a.longitude = parseFloat(a.longitude.toString());

            return a;
          })
        })
      );
  }
}
