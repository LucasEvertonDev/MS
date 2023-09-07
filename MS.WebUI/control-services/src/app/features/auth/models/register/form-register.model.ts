import { FormControl } from "@angular/forms";
import { FormControlDec } from "src/app/core/decorators/form-control.decotator";

export interface FormRegister {
    username: FormControl<string>;
    password: FormControl<string>;
    comfirmpassword: FormControl<string>;
    userGroupId: FormControl<string>;
    name: FormControl<string>;
    email: FormControl<string>;
}