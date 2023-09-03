import { FormControl } from "@angular/forms";

export interface FormRegister {
    username: FormControl<string>;
    password: FormControl<string>;
    comfirmpassword: FormControl<string>;
    userGroupId: FormControl<string>;
    name: FormControl<string>;
    email: FormControl<string>;
}