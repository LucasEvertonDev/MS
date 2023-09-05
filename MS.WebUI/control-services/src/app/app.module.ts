import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthInterceptor } from './core/interceptors/authorization-token/auth.interceptor';
import { LayoutModule } from './shared/theme/module/layout.module';
import { LayoutComponent } from './shared/theme/views/layout/layout.component';
import { LayoutAccountComponent } from './shared/theme/views/layout-account/layout-account.component';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    LayoutModule,
    HttpClientModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true  }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
