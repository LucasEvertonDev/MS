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
      comfirmpassword: new FormControl<string>({ value: '', disabled: false }, { nonNullable: true}),
      email: new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: Validators.compose([ Validators.compose([Validators.required, Validators.email])]) }),
      name: new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: Validators.compose([Validators.required]) }),
      userGroupId: new FormControl<string>({ value: "F97E565D-08AF-4281-BC11-C0206EAE06FA", disabled: true }, { nonNullable: true, validators: Validators.compose([Validators.required]) }),
    });

    this.formRegister.addValidators(this.comparisonValidator());
  }

  public register(): void {

  }

  public comparisonValidator() : any{
    return (group: FormGroup): any => {
       const control1 = group.controls['password'];
       const control2 = group.controls['comfirmpassword'];
       if (control1.value !== control2.value) {
          control2.setErrors({notEquivalent: true});
       } else {
          control2.setErrors(null);
       }
    };
  }
}
