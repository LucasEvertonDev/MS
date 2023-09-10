import { FormArray, FormControl, FormGroup, Validators } from "@angular/forms";
import { FormControlDec } from "src/app/core/decorators/form-control.decotator";

export class FormCustom {
    constructor() {
        this.teste = new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: [Validators.required] },)
        this.cpf = new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: [Validators.required] },)
        this.telefone = new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: [Validators.required] },)
    }

    public teste: FormControl<string>;
    public cpf: FormControl<string>;
    public telefone: FormControl<string>;
    public items: FormArray<FormGroup<items>> = new FormArray<FormGroup<items>>([new FormGroup<items>(new items())]);

    public static AddItem(): FormGroup<items> {
        return new FormGroup<items>(new items());
    }
}



class items {
    public preco: FormControl<string> = new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: [Validators.required] });
    public nome: FormControl<string> = new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: [Validators.required] });
}