import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomRoutingModule } from './custom-routing.module';
import { LayoutModule } from '@angular/cdk/layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/shared/modules/material.module';
import { FormComponent } from './components/form/form.component';
import { TranslocoRootModule } from 'src/app/shared/modules/transloco-root.module';


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
  ]
})
export class CustomModule { }
