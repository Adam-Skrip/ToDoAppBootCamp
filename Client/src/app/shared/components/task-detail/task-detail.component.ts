import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {ITaskResult} from "../../models/task/ITaskResult";
import * as moment from "moment/moment";

@Component({
  selector: 'app-task-detail',
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.css']
})
export class TaskDetailComponent implements OnInit {
  tempDate!: Date;
  date: string = "";
  constructor(@Inject(MAT_DIALOG_DATA) public data : ITaskResult) {
  }

  ngOnInit(): void {
    this.tempDate = new Date(this.data.createdAt)
    this.date = moment(this.tempDate).format("D MMMM, YYYY")
  }



}
