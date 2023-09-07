// import { FormControl } from '@angular/forms';

import { FormControl } from "@angular/forms";

export function FormControlDec<T>(form: FormControl<T>) {
    return (target: any, memberName: string) => {
      let currentValue: any = target[memberName];
      Object.defineProperty<any>(target, memberName, {
        set: (newValue: any) => {
          currentValue = form;
        },
        get: () => currentValue
      });
    };
  }


  export function configurable(value: boolean) {
    return function (target: any, propertyKey: string, descriptor: PropertyDescriptor) {
      descriptor.configurable = value;
    };
  }

