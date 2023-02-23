import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskComponent } from './components/task/task.component';
import {MaterialModule} from "./modules/material.module";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { AddTaskComponent } from './components/add-task/add-task.component';
import { AddListComponent } from './components/add-list/add-list.component';



@NgModule({
  declarations: [
    TaskComponent,
    AddTaskComponent,
    AddListComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule
  ],
  exports: [
    TaskComponent,
    MaterialModule,
    AddTaskComponent
  ]
})
export class SharedModule { }
