import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomRoutingModule } from './custom-routing.module';
import { LayoutModule } from '@angular/cdk/layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/shared/modules/material.module';
import { FormComponent } from './components/form/form.component';
import { TranslocoRootModule } from 'src/app/shared/modules/transloco-root.module';
import { IConfig, NgxMaskDirective, NgxMaskPipe, provideEnvironmentNgxMask, provideNgxMask } from 'ngx-mask';

const maskConfig: Partial<IConfig> = {
  validation: false,
};

@NgModule({
  declarations: [
    FormComponent
  ],
  imports: [
    CommonModule,
    CustomRoutingModule,
    LayoutModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    TranslocoRootModule,
    NgxMaskDirective,
    NgxMaskPipe
  ],
  providers: [
    provideEnvironmentNgxMask(maskConfig), // modulo global possivelmente
    provideNgxMask()
  ]
})
export class CustomModule { }
