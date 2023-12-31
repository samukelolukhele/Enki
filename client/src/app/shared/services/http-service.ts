import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GenericHttpService {
  private readonly APIUrl = environment.apiUrl;
  private readonly AuthHeaders: HttpHeaders = new HttpHeaders({
    Authorization: localStorage.getItem('token') || '',
  });
  constructor(protected httpClient: HttpClient) {}

  /**
   *
   *
   * @template T
   * @param {string} requestRoute - Appends the request route with the value of this parameter.
   * @param {Object} [headers] - The headers to be included in the request (optional).
   * @return returns an Observable array of requested data. Use subscribe method to extract the response.
   * @memberof GenericHttpClient
   */
  getList<T>(requestRoute: string): Observable<T[]> {
    return this.httpClient.get<T[]>(this.APIUrl + requestRoute, {
      headers: this.AuthHeaders,
    });
  }

  /**
   *
   *
   * @template T
   * @param {string} requestRoute - Appends the request route with the value of this parameter.
   * @param {Object} [headers] - The headers to be included in the request (optional).
   * @return {*}  returns an Observable containing the response. Use subscribe method to extract the response.
   * @memberof GenericHttpClient
   */
  get<T>(requestRoute: string): Observable<T> {
    return this.httpClient.get<T>(this.APIUrl + requestRoute, {
      headers: this.AuthHeaders,
    });
  }

  /**
   *
   *
   * @template T
   * @param {string} requestRoute - Appends the request route with the value of this parameter.
   * @param {T} body - The body of the request
   * @return {*} returns an Observable containing the result of the request. Use subscribe method to extract the response.
   * @memberof GenericHttpService
   */
  post<T>(requestRoute: string, body: T): Observable<T> {
    return this.httpClient.post<T>(this.APIUrl + requestRoute, body, {
      headers: this.AuthHeaders,
    });
  }

  /**
   *
   *
   * @template T
   * @param {string} requestRoute - Appends the request route with the value of this parameter.
   * @param {string} id - The id of the entity to be updated.
   * @param {T} body - The body of the request
   * @param {Object} [headers]
   * @return {*} returns an Observable containing the result of the request. Use subscribe method to extract the response.
   * @memberof GenericHttpService
   */
  put<T>(
    requestRoute: string,
    id: string,
    body: T,
    headers?: Object
  ): Observable<T> {
    let params = new HttpParams().set('id', id.toString());

    return this.httpClient.put<T>(
      this.APIUrl + requestRoute + `?${params.toString()}`,
      body,
      headers
    );
  }

  /**
   *
   *
   * @template T
   * @param {string} requestRoute - Appends the request route with the value of this parameter.
   * @param {string} id - The id of the entity to be updated.
   * @return {*} returns an Observable containing the result of the request. Use subscribe method to extract the response.
   * @memberof GenericHttpService
   */
  delete<T>(requestRoute: string, id: string) {
    let params = new HttpParams().set('id', id.toString());

    return this.httpClient.delete<T>(
      this.APIUrl + requestRoute + `?${params.toString()}`
    );
  }
}
