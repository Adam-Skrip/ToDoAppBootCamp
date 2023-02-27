import { Component } from '@angular/core';
import {IUser} from "../../shared/models/IUser";
import {AccountService} from "../account.service";
import {Router} from "@angular/router";
import {FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  hide:boolean = true;
  user = {} as IUser;

  loginForm = new FormGroup({
    username: new FormControl('',[Validators.required]),
    password: new FormControl('',[Validators.required])
  })
  constructor(private accountService: AccountService, private router : Router) {
  }

  login(user : IUser) {
    this.accountService.login(user).subscribe()

    }

  }

