import { Injectable, inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { AuthorizationService } from '../../services/authorization.services';
import { Observable, tap } from 'rxjs';

export const unauthorizationGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> => {
  return inject(UnauthorizationGuard).canActivate();
};

@Injectable({
  providedIn: 'root'
})
class UnauthorizationGuard {
  public constructor(
    protected readonly authSerice: AuthorizationService,
    protected readonly router: Router,
  ) { }

  public canActivate(): Observable<boolean> {
    return this.authSerice.usuarioEstaLogado()
      .pipe(
        tap((estaLogado) => {
          if (!estaLogado) {
            this.router.navigate(['/auth'])
            return true;
          } else {
            this.router.navigate(['/dashboard'])
            return false;
          }
        })
      )
  }
}
