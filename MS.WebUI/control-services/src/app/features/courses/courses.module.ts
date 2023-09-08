import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoursesRoutingModule } from './courses-routing.module';
import { ListComponent } from './components/list/list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/shared/modules/material.module';
import { LayoutModule } from 'src/app/shared/theme/module/layout.module';
import { FormComponent } from './components/form/form.component';


@NgModule({
  declarations: [
    ListComponent,
    FormComponent
  ],
  imports: [
    CommonModule,
    CoursesRoutingModule,
    LayoutModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule
  ]
})
export class CoursesModule { }
