import {Component, Input} from '@angular/core';
import {DashboardService} from "../../../dashboard/dashboard.service";
import {ITask} from "../../models/task/ITask";

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css']
})
export class AddTaskComponent {
  @Input("idInput") basketId! : string;
  taskTitle: string = "";
  constructor(private ds : DashboardService) {
  }

  addTask() {
    let newTask = {} as ITask;
    newTask.title = this.taskTitle;
    newTask.description = "";
    newTask.status = "";
    this.ds.addTask(newTask,this.basketId).subscribe();

  }

}
