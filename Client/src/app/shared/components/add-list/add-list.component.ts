import { Component } from '@angular/core';
import {DashboardService} from "../../../dashboard/dashboard.service";
import {IList} from "../../models/list/IList";


@Component({
  selector: 'app-add-list',
  templateUrl: './add-list.component.html',
  styleUrls: ['./add-list.component.css']
})
export class AddListComponent {
  list = {} as IList
  constructor(private dashboardService: DashboardService) {
  }
  onSubmit(list : IList) {
    this.dashboardService.addList(list).subscribe();
  }
}