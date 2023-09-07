import { FormControl, Validators } from "@angular/forms";

export class FormListCourses {
    public constructor() {
        this.name = new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: [] },)
    }
    public name: FormControl<string>;
}