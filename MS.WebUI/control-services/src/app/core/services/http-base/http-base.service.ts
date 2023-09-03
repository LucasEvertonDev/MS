import { HttpClient, HttpContext, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpBaseService {
  protected readonly httpClient!: HttpClient;
  public urlApiBase = 'https://localhost:44368/api/v1/';
  public urlAuth = 'https://localhost:44368/api/v1/';

  constructor(protected readonly injector: Injector) {
    if(injector == null || injector == undefined){
      throw new Error('Injector não pode ser nulo');
    }

    this.httpClient = injector.get(HttpClient);
  }

  protected httpGet(endpoint: string): Observable<any> {
    return this.httpClient.get(`${this.urlApiBase}${endpoint}`);
  }

  protected httpPost(endpoint: string, dados:any): Observable<any> {
    return this.httpClient.post(`${this.urlApiBase}${endpoint}`, dados);
  }

  protected httpPut(endpoint: string, dados:any): Observable<any> {
    return this.httpClient.put(`${this.urlApiBase}${endpoint}`, dados);
  }

  protected httpDelete(endpoint: string): Observable<any> {
    return this.httpClient.delete(`${this.urlApiBase}${endpoint}`);
  }

  protected httpPost2<T>(endpoint: string, body: any, options?: optionsHttp): Observable<T>{
    return this.httpClient.post<T>(`${this.urlApiBase}${endpoint}`, body);
  }
}

export interface optionsHttp 
{
  headers?: HttpHeaders | {
      [header: string]: string | string[];
  };
  context?: HttpContext;
  observe?: 'body';
  params?: HttpParams | {
      [param: string]: string | number | boolean | ReadonlyArray<string | number | boolean>;
  };
  reportProgress?: boolean;
  responseType?: 'json';
  withCredentials?: boolean;
}