import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { LOCALE_ID } from '@angular/core';
import localePt from '@angular/common/locales/pt';
import { registerLocaleData } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthInterceptor } from './core/interceptors/authorization-token/auth.interceptor';
import { LayoutModule } from './shared/theme/module/layout.module';
import { LoadingService } from './shared/services/loading.service';
import { LoadingInterceptor } from './core/interceptors/loading-interceptor/loading.interceptor';
import { TranslocoRootModule } from './shared/modules/transloco-root.module';

registerLocaleData(localePt);


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
    MatProgressSpinnerModule,
    TranslocoRootModule,
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true  },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true  },
    LoadingService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
