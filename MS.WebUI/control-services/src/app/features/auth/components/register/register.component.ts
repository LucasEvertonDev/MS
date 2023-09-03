import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { FormRegister } from '../../models/register/form-register.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  public formRegister!: FormGroup<FormRegister>;
  public constructor(private formBuilder: FormBuilder) {

  }

  public ngOnInit(): void {
    this.criarFormulario();
  }

  private criarFormulario(): void {
    this.formRegister = this.formBuilder.group<FormRegister>({
      username: new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: [Validators.required], },),
      password: new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: Validators.compose([Validators.required, Validators.minLength(4)]) }),
      comfirmpassword: new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: this.checkPasswords }),
      email: new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: Validators.compose([ Validators.compose([Validators.required, Validators.email])]) }),
      name: new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: Validators.compose([Validators.required]) }),
      userGroupId: new FormControl<string>({ value: "F97E565D-08AF-4281-BC11-C0206EAE06FA", disabled: true }, { nonNullable: true, validators: Validators.compose([Validators.required]) }),
    });
  }

  public register(): void {

  }

  public checkPasswords: ValidatorFn = (group: AbstractControl):  ValidationErrors | null => { 
    let pass = group.get('password')?.value ?? "";
    let confirmPass = group.get('comfirmpassword')?.value ?? "";
    return pass === confirmPass ? null : { password_not_equals: true }
  }

   ConfirmedValidator(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];
      if (
        matchingControl.errors &&
        !matchingControl.errors.confirmedValidator
      ) {
        return;
      }
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ confirmedValidator: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }
}
