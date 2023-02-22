import {Component, Input} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {ITaskResult} from "../../models/task/ITaskResult";

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent {
  @Input("taskInput") task! : ITaskResult;
  taskList = this._formBuilder.group({
    task: false,
  });

  constructor(private _formBuilder: FormBuilder) {
  }
}
