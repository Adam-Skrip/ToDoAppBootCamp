import {Component, OnInit} from '@angular/core';
import {DashboardService} from "./dashboard.service";
import {ITaskResult} from "../shared/models/task/ITaskResult";
import {ITask} from "../shared/models/task/ITask";
import {IListResult} from "../shared/models/list/IListResult";
import {AddListComponent} from "../shared/components/add-list/add-list.component";
import {MatDialog} from "@angular/material/dialog";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  taskList: ITaskResult [] = [];
  lists: IListResult [] = [];

  public taskTitle: string = "";
  public taskDescription: string = "";

  constructor(private dashService: DashboardService, public dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.getLists()
  }

  openDialog(){
    const dialogRef = this.dialog.open(AddListComponent);
    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
    })
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



  getLists(){
    this.dashService.getAllLists().subscribe((response: IListResult[]) =>{
      for (let id = 0; id < response.length; id++){
        let data = {} as IListResult;
        data.name = response[id].name;
        data.quests = response[id].quests;
        this.lists.push(data)
      }
    console.log(response)
    })

  }
}
