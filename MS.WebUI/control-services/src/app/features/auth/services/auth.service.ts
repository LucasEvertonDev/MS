import { Injectable, Injector } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { HttpBaseService } from 'src/app/core/services/http-base/http-base.service';
import { HttpClient } from '@angular/common/http';
import { Login } from '../models/login.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private subjectUsuario: BehaviorSubject<any> = new BehaviorSubject(null);
  private subjectLogin: BehaviorSubject<any> = new BehaviorSubject(false);

  protected readonly httpClient!: HttpClient;
  protected urlApiBase = 'http://localhost:4000/api/v1/';

  constructor(protected readonly injector: Injector) {
    if(injector == null || injector == undefined){
      throw new Error('Injector não pode ser nulo');
    }

    this.httpClient = injector.get(HttpClient);
  }

  public login(login: Login): Observable<any> {
    return this.efetuarLogin(login);
  }

  public logOut(): void {
    sessionStorage.removeItem('token');
    this.subjectLogin.next(false);
  }

  public usuarioEstaLogado(): Observable<any> {
    const token = sessionStorage.getItem('token');
    
    if (token) {
      this.subjectLogin.next(true);
    }

    return this.subjectLogin.asObservable();
  }

  private efetuarLogin(login: Login) : Observable<any> {
    return this.httpClient.post<any>(`${this.urlApiBase}${"auth/login"}`, login, {
      headers: {
        client_id: '7064bbbf-5d11-4782-9009-95e5a6fd6822',
        client_secret: 'dff0bcb8dad7ea803e8d28bf566bcd354b5ec4e96ff4576a1b71ec4a21d56910'
      }
    }).pipe(
      map(response => {
        sessionStorage.setItem('token', response.content.tokenJWT);
        this.subjectLogin.next(true);

        return response.content;
      }),
    );
  }
}
