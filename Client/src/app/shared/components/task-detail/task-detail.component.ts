import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {ITaskResult} from "../../models/task/ITaskResult";

@Component({
  selector: 'app-task-detail',
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.css']
})
export class TaskDetailComponent  {
  constructor(@Inject(MAT_DIALOG_DATA) public data : ITaskResult) {
  }

}
