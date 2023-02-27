import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import {SharedModule} from "../shared/shared.module";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {RouterLink} from "@angular/router";



@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
    imports: [
        CommonModule,
        SharedModule,
        FormsModule,
        RouterLink,
        ReactiveFormsModule
    ],
  exports : [
    LoginComponent,
    RegisterComponent
  ]
})
export class AccountModule { }
