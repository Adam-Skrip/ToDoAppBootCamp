import {Component, OnInit} from '@angular/core';
import {DashboardService} from "./dashboard.service";
import {ITaskResult} from "../shared/models/ITaskResult";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit{

  constructor(private dashService : DashboardService) {
  }

  ngOnInit(): void {
    this.dashService.getAllTasks().subscribe((response:ITaskResult[]) => {
      console.log(response);
    })
  }


}
