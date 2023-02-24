import {Component, OnInit} from '@angular/core';
import {DashboardService} from "./dashboard.service";
import {ITaskResult} from "../shared/models/task/ITaskResult";
import {IListResult} from "../shared/models/list/IListResult";
import {AddListComponent} from "../shared/components/add-list/add-list.component";
import {MatDialog} from "@angular/material/dialog";
import {TaskDetailComponent} from "../shared/components/task-detail/task-detail.component";
import {MessageService} from "../shared/snackbar/message.service";

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

  constructor(private dashService: DashboardService, private messageService: MessageService, public dialog: MatDialog) {
  }



  ngOnInit(): void {
    this.getLists()
  }

  openDialog(){
    const dialogRef = this.dialog.open(AddListComponent);
    dialogRef.afterClosed().subscribe()
  }

  openTaskDetail(task: ITaskResult) {
    const dialogRef = this.dialog.open(TaskDetailComponent, {
      height: '500px',
      width: '400px',
      data : task
    });
    dialogRef.afterClosed().subscribe()

  }



  getLists(){

    this.dashService.getAllLists().subscribe((response: IListResult[]) =>{
      // for (let id = 0; id < response.length; id++){
      //   let data = {} as IListResult;
      //   data.publicId = response[id].publicId;
      //   data.name = response[id].name;
      //   data.quests = response[id].quests;
      //   this.lists.push(data)
      //}
      this.lists = response;
    console.log(response)
    })

  }

  deleteList(id: string) {
    this.dashService.deleteList(id).subscribe(() =>{
      this.getLists();
      this.messageService.successMessage("List was deleted")

    })

  }


}
