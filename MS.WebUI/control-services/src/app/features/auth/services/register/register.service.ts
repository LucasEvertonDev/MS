import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { AppClient, ResponseDto } from 'src/app/core/api';
import { CreateUserRequest } from 'src/app/core/api/requests/auth/register/create-user-request.model';
import { CreateUserResponse } from 'src/app/core/api/responses/auth/register/create-user-response.model';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(protected httpClient: AppClient) {
    httpClient.SetBaseUrl(httpClient.AUTH_API_BASE_URL);
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
