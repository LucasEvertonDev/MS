import { AuthorizationService } from 'src/app/core/services/authorization.services';
import { LoginService } from '../../services/login.service';
import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { FormLogin } from '../../models/login/form-login.model';
import { Router } from '@angular/router';
import { Observable, Subject, Subscription, firstValueFrom, map, take, takeUntil } from 'rxjs';
import { LoginRequest } from 'src/app/core/api';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  // Our magical observable that will be passed to takeUntil()
  private readonly ngUnsubscribe$: Subject<void> = new Subject<void>();
  public formLogin!: FormGroup<FormLogin>;
  public authLogin!: LoginRequest;
  @ViewChild("inputLogin") inputlogin!: ElementRef;

  public constructor(private loginService: LoginService,
    private router: Router,
    private formBuilder: FormBuilder,
    private snackBar: SnackBarService,
    private authorization: AuthorizationService) { }

  public async ngOnInit(): Promise<void> {
    if (await this.estaLogado()) {
      this.router.navigateByUrl('dashboard')
    }

    this.criarFormulario();
    this.inputlogin?.nativeElement?.focus();
  }

  public async estaLogado(): Promise<boolean> {
    return await firstValueFrom(this.authorization.usuarioEstaLogado().pipe(takeUntil(this.ngUnsubscribe$)));
  }

  //https://blog.bitsrc.io/6-ways-to-unsubscribe-from-observables-in-angular-ab912819a78f
  public login(): void {
    // assign faz um mapper do objeto
    this.authLogin = Object.assign('', this.authLogin, this.formLogin.value);
    this.authLogin.username = this.authLogin.username.toLowerCase();

    this.loginService.login({ ...this.authLogin })
      .pipe(
        take(1) // posso guardar a inscricao com take until ou cortar aqui nesse momento takeUntil(this.ngUnsubscribe$)
      )
      .subscribe(response => {
        if (response.success) {
          this.router.navigate(['/dashboard'], { state: { prevPage: '/dashboard' } });
          //this.router.navigateByUrl('dashboard')
        }
        else {
          this.snackBar.ShowErrors(response.errors);
        }
      });
  }

  private criarFormulario(): void {
    // Serve para validar em conjunto ao mesmo tempo o compose
    //Validators.compose([Validators.required, Validators.email]);
    this.formLogin = this.formBuilder.group<FormLogin>({
      username: new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: [Validators.required], },),
      password: new FormControl<string>('', { nonNullable: true, validators: Validators.compose([Validators.required, Validators.minLength(6)]) },)
    });
  }

  public ngOnDestroy(): void {
    // Emit a value so that takeUntil will handle the closing of our subscriptions;
    this.ngUnsubscribe$.next();
    // Unsubscribe from our unsubscriber to avoid creating a memory leak
    this.ngUnsubscribe$.unsubscribe();
  }
}