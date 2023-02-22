import {Component, OnInit} from '@angular/core';
import {AccountService} from "../../account/account.service";
import {Observable} from "rxjs";
import {IUser} from "../../shared/models/IUser";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit{
  currentUser$?: Observable<IUser | null>
  constructor(public as: AccountService) {
  }

  logout() {
    this.as.logout()
  }

  ngOnInit(): void {
    this.currentUser$ = this.as.currentUser$;
    console.log(this.as.isLoggedIn())
    console.log(this.currentUser$  === null)
  }
}
