import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { FormRegister } from '../../models/register/form-register.model';
import { CreateUserRequest } from 'src/app/core/api/requests/auth/register/create-user-request.model';
import { take } from 'rxjs';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  public formRegister!: FormGroup<FormRegister>;
  public createuser!: CreateUserRequest;
  public constructor(private formBuilder: FormBuilder,
    private registerService: LoginService,
    private router: Router,
    private snackBar: SnackBarService) {

  }

  public ngOnInit(): void {
    this.criarFormulario();
  }

  private criarFormulario(): void {
    this.formRegister = this.formBuilder.group<FormRegister>({
      username: new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: [Validators.required], },),
      password: new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: Validators.compose([Validators.required, Validators.minLength(6)]) }),
      comfirmpassword: new FormControl<string>({ value: '', disabled: false }, { nonNullable: true }),
      email: new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: Validators.compose([Validators.compose([Validators.required, Validators.email])]) }),
      name: new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: Validators.compose([Validators.required]) }),
      userGroupId: new FormControl<string>({ value: "F97E565D-08AF-4281-BC11-C0206EAE06FA", disabled: true }, { nonNullable: true, validators: Validators.compose([Validators.required]) }),
    });

    this.formRegister.addValidators(this.comparisonValidator());
  }

  public register(): void {
    console.log(this.formRegister.value);

    // assign faz um mapper do objeto
    this.createuser = Object.assign('', this.createuser, this.formRegister.value);
    this.createuser.username = this.createuser.username.toLowerCase();
    this.createuser.userGroupId = this.formRegister.value.userGroupId ?? "F97E565D-08AF-4281-BC11-C0206EAE06FA",
    this.registerService.registerUser({ ...this.createuser })
      .pipe(
        take(1) // posso guardar a inscricao com take until ou cortar aqui nesse momento takeUntil(this.ngUnsubscribe$)
      )
      .subscribe(response => {
        if (response.success) {
          this.snackBar.ShowSucess("Usuário cadastrado com sucesso!");
          this.router.navigateByUrl('auth')
        }
        else {
          this.snackBar.ShowErrors(response.errors);
        }
      });
  }

  public comparisonValidator(): any {
    return (group: FormGroup<FormRegister>): void => {
      const password = group.controls.password;
      const comfirmpassword = group.controls.comfirmpassword;
      if (password.value !== comfirmpassword.value) {
        comfirmpassword.setErrors({ notEquivalent: true });
      } else {
        comfirmpassword.setErrors(null);
      }
    };
  }
}
