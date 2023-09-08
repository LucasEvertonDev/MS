import { FormControl, FormGroup, Validators } from "@angular/forms"

export class FormCourse {
    public constructor() {
        this.id = new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: [] },)
        this.name = new FormControl<string>({ value: '', disabled: false }, { nonNullable: true, validators: [Validators.required] },)
        this.startDate = new FormControl<Date>({ value: new Date(), disabled: false }, { nonNullable: true, validators: [Validators.required] },)
        this.endDate = new FormControl<Date | null>({ value: null, disabled: false }, { nonNullable: false, validators: [Validators.required] },)
    }

    public id: FormControl<string>;
    public name: FormControl<string>;
    public startDate: FormControl<Date>;
    public endDate: FormControl<Date | null>;

    public static customValidatorDatesInterval(): any {
        return (group: FormGroup<FormCourse>): void => {

            if (group.controls.startDate.value && group.controls.endDate.value) {
                group.controls.endDate.setErrors(
                    group.controls.startDate.value > group.controls.endDate.value ? { invalidPeriod: true } : null
                );
            }
        };
    }
}