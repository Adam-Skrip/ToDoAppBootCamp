import { Component } from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {AddListComponent} from "../add-list/add-list.component";

@Component({
  selector: 'app-add-button',
  templateUrl: './add-button.component.html',
  styleUrls: ['./add-button.component.css']
})
export class AddButtonComponent {
  constructor(public dialog: MatDialog) {
  }

  openDialog(){
    const dialogRef = this.dialog.open(AddListComponent);
    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
    })
  }

}
