import { Injectable, Injector } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { LoginRequest, LoginResponse, ResponseDto } from 'src/app/core/api';
import { AuthorizationService } from 'src/app/core/services/authorization.services';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  constructor(protected authService: AuthorizationService) { }

  public login(login: LoginRequest): Observable<ResponseDto<LoginResponse>> {
    return this.authService.login(login);
  }
}
