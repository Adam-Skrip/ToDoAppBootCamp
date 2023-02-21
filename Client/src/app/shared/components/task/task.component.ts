import { Component } from '@angular/core';
import {FormBuilder} from "@angular/forms";

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent {
  taskList = this._formBuilder.group({
    task: false,
  });

  constructor(private _formBuilder: FormBuilder) {
  }
}
