import { Injectable } from '@angular/core';
import { environment } from '../environment/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GenericHttpClient {
  private readonly APIUrl = environment.apiUrl;

  constructor(protected httpClient: HttpClient) {}

  getList<T>(requestRoute: string): Observable<T[]> {
    return this.httpClient.get<T[]>(this.APIUrl + requestRoute);
  }
}
