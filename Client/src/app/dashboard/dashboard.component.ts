import {Component, OnInit} from '@angular/core';
import {DashboardService} from "./dashboard.service";
import {ITaskResult} from "../shared/models/task/ITaskResult";
import {IListResult} from "../shared/models/list/IListResult";
import {AddListComponent} from "../shared/components/add-list/add-list.component";
import {MatDialog} from "@angular/material/dialog";
import {TaskDetailComponent} from "../shared/components/task-detail/task-detail.component";
import {MessageService} from "../shared/snackbar/message.service";
import {CdkDragDrop, moveItemInArray, transferArrayItem} from "@angular/cdk/drag-drop";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  lists: IListResult [] = [];
  listId: string = "";

  taskId: string = "";
  oldBasketId : string = "";
  newBasketId : string = "";



  constructor(public dashService: DashboardService, private messageService: MessageService, public dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.getLists();
    this.dashService.isAdded.subscribe((response)=>{
      if (response){
        this.getLists();
      }
    })

  }


  openDialog(){
    const dialogRef = this.dialog.open(AddListComponent);
    dialogRef.afterClosed().subscribe(()=>{
      this.getLists();
    })
  }

  openTaskDetail(task: ITaskResult) {
    const dialogRef = this.dialog.open(TaskDetailComponent, {
      disableClose: true,
      height: '500px',
      width: '450px',
      data : task
    });
    dialogRef.afterClosed().subscribe(() =>{
      this.getLists();
    })

  }


  getLists(){
    this.dashService.getAllLists().subscribe((response: IListResult[]) =>{
      this.lists = response;
      this.dashService.isAdded.next(false);
    })
  }

  deleteList(id: string) {
    this.dashService.deleteList(id).subscribe(() =>{
      this.getLists();
      this.messageService.successMessage("List was deleted")
    })
  }

  getIds(oldId: string, taskId: string){
    this.oldBasketId = oldId;
    this.taskId = taskId;
  }
  getListId(id: string){
    this.listId = id;
  }

  updateList(event: any) {
    let name = event.target.value;
    if(name){
      this.dashService.updateList(this.listId,name).subscribe((response)=>{
        console.log(response)
        this.getLists();
      })
    }
    else {
      this.messageService.errorMessage("List name cannot be empty!")
      this.getLists()
    }

  }

  migrateTask(oldId:string, newId: string, taskId: string){
    return this.dashService.migrateTask(oldId,newId,taskId).subscribe(()=>{
      this.getLists();
    })
  }

  drop(event: CdkDragDrop<any,any>, id : string) {
    console.log(event)

    this.newBasketId = id;
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
      this.migrateTask(this.oldBasketId,this.newBasketId,this.taskId);
    }
  }




}
