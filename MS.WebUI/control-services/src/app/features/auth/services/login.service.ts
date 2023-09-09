import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { AppClientAuth, LoginRequest, LoginResponse, ResponseDto } from 'src/app/core/api';
import { CreateUserRequest } from 'src/app/core/api/requests/auth/register/create-user-request.model';
import { CreateUserResponse } from 'src/app/core/api/responses/auth/register/create-user-response.model';
import { AuthorizationService } from 'src/app/core/services/authorization.services';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  constructor(protected authService: AuthorizationService,
    protected httpClient: AppClientAuth) { }

  public login(login: LoginRequest): Observable<ResponseDto<LoginResponse>> {
    return this.authService.login(login);
  }

  public registerUser(login: CreateUserRequest): Observable<ResponseDto<CreateUserResponse>> {
    return this.httpClient.HttpPost<CreateUserResponse>("users", login)
      .pipe(
        map(response => {
          return response;
        }),
      );
  }
}
