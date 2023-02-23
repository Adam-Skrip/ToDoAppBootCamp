import {Component, Input} from '@angular/core';
import {DashboardService} from "../../../dashboard/dashboard.service";
import {ITask} from "../../models/task/ITask";

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css']
})
export class AddTaskComponent {
  @Input("idInput") publicId! : string;
  taskTitle: string = "";
  constructor(private ds : DashboardService) {
  }

  addTask() {
    let newTask = {} as ITask;
    newTask.title = this.taskTitle;
    newTask.description = "";
    newTask.publicId = this.publicId;
    this.ds.addTask(newTask).subscribe();

  }

}
