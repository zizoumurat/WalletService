import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment'
import { ILocalStorageService } from 'src/app/core/services/ILocal.storage.service';
import { AUTH_KEY } from 'src/app/core/constants/storage.keys';
import { AuthModel } from 'src/app/core/common/auth.model';

@Injectable()
export class ApiService {
  constructor(private http: HttpClient, private localService: ILocalStorageService) { }

  get<T>(url: string, params: HttpParams = new HttpParams()): Observable<T> {

    return this.http.get<T>(`${environment.apiUrl}${url}`, {
      headers: this.headers,
      params,
    });
  }

  post<T, D>(url: string, data: D): Observable<T> {
    return this.http.post<T>(`${environment.apiUrl}${url}`, JSON.stringify(data), {
      headers: this.headers
    });
  }

  postForm<T, D>(url: string, data: D): Observable<T> {
    return this.http.post<T>(`${environment.apiUrl}${url}`, data, {
      headers: this.formHeaders
    });
  }

  put<T, D>(url: string, data: D): Observable<T> {
    return this.http.put<T>(`${environment.apiUrl}${url}`, JSON.stringify(data), {
      headers: this.headers
    });
  }

  putForm<T, D>(url: string, data: D): Observable<T> {
    return this.http.put<T>(`${environment.apiUrl}${url}`, data, {
      headers: this.formHeaders
    });
  }

  delete<T>(url: string): Observable<T> {
    return this.http.delete<T>(`${environment.apiUrl}${url}`, {
      headers: this.headers
    });
  }

  get headers(): HttpHeaders {
    const headersConfig = {
      'Content-Type': 'application/json',
      Accept: 'application/json',
      'Authorization': ''
    };

    var auth = this.localService.getData(AUTH_KEY) as AuthModel;

    if (auth && auth.token)
      headersConfig["Authorization"] = `Bearer ${auth.token}`;

    return new HttpHeaders(headersConfig);
  }

  get formHeaders(): HttpHeaders {
    const headersConfig = {};

    return new HttpHeaders(headersConfig);
  }

}