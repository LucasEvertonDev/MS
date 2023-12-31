import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable, Injector } from "@angular/core";
import { Observable, catchError, of } from "rxjs";
import { optionsHttp } from "../models/options.model";
import { ResponseDto } from "../responses/base/response.model";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
})
export class AppClientAuth {
    public AUTH_API_BASE_URL = environment.AuthApi;
    public GATEWAY_API_BASE_URL = environment.GatewayApi;
    protected urlApiBase!: string;

    constructor(private readonly _httpClient: HttpClient) {
        this.SetBaseUrl(this.AUTH_API_BASE_URL);
    }

    public HttpGet<T>(endpoint: string, options?: optionsHttp): Observable<ResponseDto<T>> {
        return this._httpClient.get<ResponseDto<T>>(`${this.urlApiBase}${endpoint}`, options);
    }

    public HttpPost<T>(endpoint: string, body: any, options?: optionsHttp): Observable<ResponseDto<T>> {
        return this._httpClient.post<ResponseDto<T>>(`${this.urlApiBase}${endpoint}`, body, options)
            .pipe(
                catchError((error: HttpErrorResponse) => {
                    return of(this.handleError<T>(error));
                })
            );
    }

    public HttpPut<T>(endpoint: string, body: any, options?: optionsHttp): Observable<ResponseDto<T>> {
        return this._httpClient.put<ResponseDto<T>>(`${this.urlApiBase}${endpoint}`, body, options);
    }

    public HttpDelete<T>(endpoint: string, options?: optionsHttp): Observable<ResponseDto<T>> {
        return this._httpClient.delete<ResponseDto<T>>(`${this.urlApiBase}${endpoint}`, options);
    }

    public SetBaseUrl(baseUrl: string): void {
        this.urlApiBase = baseUrl;
    }

    protected handleError<T>(error: HttpErrorResponse): ResponseDto<T> {
        if (error.error) {
            return error.error as ResponseDto<T>
        }

        return { 
            errors: [ { context: "Business", message: "Operação não foi realizada com sucesso. Por favor contate o administrador do sistema!" }],
            httpCode: 500,
            success: false,
            content: {} as T
        }
    };
}

@Injectable({
    providedIn: 'root'
})
export class AppClient extends AppClientAuth{
    constructor(private readonly httpClient: HttpClient) {
        super(httpClient);
        this.SetBaseUrl(this.GATEWAY_API_BASE_URL);
    }
}

