import {Component, EventEmitter, Output} from '@angular/core';
import {DashboardService} from "../../../dashboard/dashboard.service";
import {IList} from "../../models/list/IList";
import {MatDialogRef} from "@angular/material/dialog";


@Component({
  selector: 'app-add-list',
  templateUrl: './add-list.component.html',
  styleUrls: ['./add-list.component.css']
})
export class AddListComponent {
  list = {} as IList
  constructor(private dashboardService: DashboardService, public refDialog : MatDialogRef<AddListComponent>) {
  }
  onSubmit(list : IList) {
    if(list){
      this.dashboardService.addList(list).subscribe(()=>{
        this.refDialog.close();
      })

    }
  }
}
