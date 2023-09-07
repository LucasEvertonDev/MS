import { Injectable, Injector } from "@angular/core";
import { BehaviorSubject, Observable, map } from "rxjs";
import { AppClient, LoginRequest, LoginResponse, ResponseDto } from "../api";
import { environment } from "src/environments/environment";
@Injectable({
    providedIn: 'root'
})
export class AuthorizationService {
    private subjectUsuario: BehaviorSubject<any> = new BehaviorSubject(null);
    private subjectLogin: BehaviorSubject<any> = new BehaviorSubject(false);

    constructor(protected httpClient: AppClient) { 
        httpClient.SetBaseUrl(httpClient.AUTH_API_BASE_URL);
    }

    public login(login: LoginRequest): Observable<ResponseDto<LoginResponse>> {
        return this.httpClient.HttpPost<LoginResponse>("auth/login", login, {
            headers: {
                client_id: environment.client_id,
                client_secret: environment.client_secret
            }
        }).pipe(
            map(response => {
                if (response.success) {
                    sessionStorage.setItem('token', response.content.tokenJWT ?? "");
                    this.subjectLogin.next(true);
                }
                return response;
            }),
        );
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
}
