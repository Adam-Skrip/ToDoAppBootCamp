import { Component } from '@angular/core';
import {IUser} from "../../shared/models/IUser";
import {AccountService} from "../account.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  hide:boolean = true;
  user = {} as IUser;
  constructor(private accountService: AccountService, private router : Router) {
  }

  login(user : IUser) {

    this.accountService.login(user).subscribe()

    }

  }

