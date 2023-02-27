import {Component, Input} from '@angular/core';
import {DashboardService} from "../../../dashboard/dashboard.service";
import {ITask} from "../../models/task/ITask";
import {Subject} from "rxjs";
import {MessageService} from "../../snackbar/message.service";

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css']
})
export class AddTaskComponent{
  @Input("idInput") basketId! : string;

  taskTitle: string = "";
  constructor(private ds : DashboardService, private ms: MessageService) {
  }

  addTask() {
    let newTask = {} as ITask;
    newTask.title = this.taskTitle;
    newTask.description = "";
    newTask.status = "";
    if(this.taskTitle){
      this.ds.addTask(newTask,this.basketId).subscribe(()=>{
        this.ds.isAdded.next(true);
      });
    }
    else{
      this.ms.errorMessage("Task name cannot be empty!")
    }


  }



}
