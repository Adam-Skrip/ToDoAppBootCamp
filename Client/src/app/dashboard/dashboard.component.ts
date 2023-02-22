import {Component, OnInit} from '@angular/core';
import {DashboardService} from "./dashboard.service";
import {ITaskResult} from "../shared/models/task/ITaskResult";
import {ITask} from "../shared/models/task/ITask";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  taskList: ITaskResult [] = [];

  public taskTitle: string = "";
  public taskDescription: string = "";

  constructor(private dashService: DashboardService) {
  }

  ngOnInit(): void {
    this.getTasks();
  }

  getTasks() {
    this.taskList = [];
    this.dashService.getAllTasks().subscribe((response: ITaskResult[]) => {
      for (let id = 0; id < response.length; id++) {
        let data = {} as ITaskResult;
        data.title = response[id].title;
        data.description = response[id].description;
        this.taskList.push(data);
      }
      console.log(response);
    })
  }

  addTask() {
    console.log(this.taskTitle)
    let newTask = {} as ITask;
    newTask.title = this.taskTitle;
    newTask.description = this.taskDescription;
    this.dashService.addTask(newTask).subscribe(() => {
        this.getTasks()
      }
    );

  }

  addList() {

  }
}
