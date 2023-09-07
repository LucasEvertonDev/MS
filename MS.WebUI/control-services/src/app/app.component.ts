import { AfterContentChecked, AfterViewInit, Component, OnInit } from '@angular/core';
import { AuthorizationService } from './core/services/authorization.services';
import { delay, tap } from 'rxjs';
import { LoadingService } from './shared/services/loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public loading: boolean = false;
  public authorization: boolean = false;
  constructor(private authorizationService: AuthorizationService,
    private _loading: LoadingService
  ) {

  }

  public ngOnInit(): void {
    this.authorizationService.usuarioEstaLogado()
      .subscribe((estaLogado) => {
        this.authorization = estaLogado
      });

    this._loading.loadingSub
      .pipe(delay(0)) // This prevents a ExpressionChangedAfterItHasBeenCheckedError for subsequent requests
      .subscribe((loading) => {
        this.loading = loading;
      });
  }
}
