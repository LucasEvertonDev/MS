import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { AppClient, ResponseDto, SearchCourseReponse, PaginationReuslt } from 'src/app/core/api';

@Injectable({
  providedIn: 'root'
})
export class CoursesService {

  constructor(protected httpClient: AppClient) {
    httpClient.SetBaseUrl(httpClient.GATEWAY_API_BASE_URL);
  }

  public searchUsers(pageIndex: number, pageSize: number, name?: string): Observable<ResponseDto<PaginationReuslt<SearchCourseReponse>>> {
    let params = new HttpParams();
    params = name ? params.append('name', name) : params;

    return this.httpClient.HttpGet<PaginationReuslt<SearchCourseReponse>>(`courses/${pageIndex + 1}/${pageSize}`, {
      params: params
    })
      .pipe(
        map(response => {
          return response;
        }),
      );
  }
}
