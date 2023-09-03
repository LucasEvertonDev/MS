import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { AuthRoutingModule } from './auth-routing.module';
import { LayoutModule } from 'src/app/shared/theme/module/layout.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { MaterialModule } from 'src/app/shared/modules/material.module';
import { FocusDirective } from 'src/app/core/directives/focus.directive';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    FocusDirective
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    HttpClientModule,
    LayoutModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule
  ],
  providers: [
    
  ]
})
export class AuthModule { }
