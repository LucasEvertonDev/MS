import { Injectable, Injector } from "@angular/core";
import { BehaviorSubject, Observable, map } from "rxjs";
import { AppClientAuth, LoginRequest, LoginResponse, ResponseDto } from "../api";
import { environment } from "src/environments/environment";
import jwt_decode from 'jwt-decode';
import { TokenModel } from "../api/models/token.model";

@Injectable({
    providedIn: 'root'
})
export class AuthorizationService {
    private subjectToken: BehaviorSubject<TokenModel> = new BehaviorSubject({} as TokenModel);
    private subjectLogin: BehaviorSubject<any> = new BehaviorSubject(false);

    constructor(protected httpClient: AppClientAuth) {
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
                    this.translateToken(response);

                    sessionStorage.setItem('token', response.content.tokenJWT ?? "");
                    this.subjectLogin.next(true);
                }
                return response;
            }),
        );
    }

    public RefreshToken(): Observable<ResponseDto<LoginResponse>> {
        console.log("atualizei no token", new Date().toISOString());
       
        return this.httpClient.HttpPost<LoginResponse>("auth/refreshtoken", {}, {
            headers: {
                client_id: environment.client_id,
                client_secret: environment.client_secret
            }
        }).pipe(
            map(response => {
                if (response.success) {
                    this.translateToken(response);

                    sessionStorage.setItem('token', response.content.tokenJWT ?? "");
                    this.subjectLogin.next(true);
                }
                return response;
            }),
        );
    }

    private translateToken(response: ResponseDto<LoginResponse>) {
        var token: TokenModel = jwt_decode(response.content.tokenJWT);

        token.expirationFormated = new Date(token.exp);

        token.refreshTokenInMS = token.exp / 10000; // 3 minutos pra refresh token 

        console.log(token);

        this.subjectToken.next(token);
    }

    public logOut(): void {
        sessionStorage.removeItem('token');
        this.subjectLogin.next(false);
        this.subjectToken.next({} as TokenModel);
    }

    public usuarioEstaLogado(): Observable<any> {
        const token = sessionStorage.getItem('token');

        if (token) {
            this.subjectLogin.next(true);
        }

        return this.subjectLogin.asObservable();
    }

    public getToken(): Observable<any> {
        return this.subjectToken.asObservable();
    }
}
