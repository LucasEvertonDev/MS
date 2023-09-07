import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomRoutingModule } from './custom-routing.module';
import { LayoutModule } from '@angular/cdk/layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/shared/modules/material.module';
import { FormComponent } from './components/form/form.component';


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
    MaterialModule
  ]
})
export class CustomModule { }
