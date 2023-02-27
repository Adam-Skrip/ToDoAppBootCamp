import {Component, OnInit} from '@angular/core';
import {AccountService} from "../../account/account.service";
import {Observable} from "rxjs";
import {IUser} from "../../shared/models/IUser";
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  currentUser$?: Observable<IUser | null>
  username: string | null = "";

  constructor(public as: AccountService) {
  }

  logout() {
    this.as.logout()
  }

  ngOnInit(): void {
    this.username = localStorage.getItem('user')
      this.as.currentUser.subscribe((user) => {
        if (user) {
          localStorage.setItem('user', user.username);
        }
      })
  }

}

