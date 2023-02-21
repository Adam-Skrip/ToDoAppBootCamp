import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskComponent } from './components/task/task.component';
import {MaterialModule} from "./modules/material.module";
import {ReactiveFormsModule} from "@angular/forms";



@NgModule({
  declarations: [
    TaskComponent
  ],
    imports: [
        CommonModule,
        MaterialModule,
        ReactiveFormsModule
    ],
  exports: [
    TaskComponent,
    MaterialModule
  ]
})
export class SharedModule { }
