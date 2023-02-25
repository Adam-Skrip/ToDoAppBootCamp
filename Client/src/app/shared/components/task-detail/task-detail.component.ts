import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {ITaskResult} from "../../models/task/ITaskResult";
import * as moment from "moment/moment";
import {DashboardService} from "../../../dashboard/dashboard.service";
import {ITask} from "../../models/task/ITask";

@Component({
  selector: 'app-task-detail',
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.css']
})
export class TaskDetailComponent implements OnInit {
  tempDate!: Date;
  date: string = "";
  constructor(@Inject(MAT_DIALOG_DATA) public data : ITaskResult,
              private dashboradService: DashboardService,
              private dialogRef: MatDialogRef<TaskDetailComponent>
              ) {
  }

  ngOnInit(): void {
    this.tempDate = new Date(this.data.createdAt)
    this.date = moment(this.tempDate).format("D MMMM, YYYY")
  }


  deleteTask(id: string) {
    this.dashboradService.deleteTask(id).subscribe();
  }

  updateTask(data: ITaskResult) {
    let task = {} as ITask;
    let id = data.publicId;
    task.title = data.title;
    task.description = data.description;
    task.status = data.status;
    this.dashboradService.updateTask(id,task).subscribe(()=>{
      this.dialogRef.close();
    });
  }

  onChangeStatus($event : any){
    this.data.status = $event.target.value;

  }
}
