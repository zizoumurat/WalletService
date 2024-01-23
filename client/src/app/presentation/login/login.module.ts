import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginRoutingModule } from './login.routing.module';
import { LoginComponent } from './login.component';
import { SharedModule } from 'src/app/presentation/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    LoginRoutingModule
  ],
  declarations: [LoginComponent],
  exports:[]
})
export class LoginModule { }
