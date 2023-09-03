import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { IndexComponent } from './components/index/index.component';
import { LayoutModule } from 'src/app/shared/theme/module/layout.module';


@NgModule({
  declarations: [
    IndexComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    LayoutModule
  ]
})
export class HomeModule { }
