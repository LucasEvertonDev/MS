import { AfterContentChecked, AfterViewInit, Component, OnInit } from '@angular/core';
import { AuthorizationService } from './core/services/authorization.services';
import { tap } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  private authorization: boolean = false;
  constructor(private authorizationService: AuthorizationService) {

  }

  public ngOnInit(): void {
    this.authorizationService.usuarioEstaLogado()
    .pipe(
      tap((estaLogado) => {
        this.authorization = estaLogado;
      })
    );
  }
}
