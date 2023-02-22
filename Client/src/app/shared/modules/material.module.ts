import { NgModule } from '@angular/core';
import {MatInputModule} from "@angular/material/input";
import {MatSelectModule} from "@angular/material/select";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {MatButtonModule} from "@angular/material/button";

const modules = [
  MatInputModule,
  MatSelectModule,
  MatCheckboxModule,
  MatButtonModule
]

@NgModule({
  declarations: [],
  imports: modules,
  exports: modules
})
export class MaterialModule { }
