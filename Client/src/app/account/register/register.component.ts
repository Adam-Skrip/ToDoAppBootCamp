import { Component } from '@angular/core';
import {IUserRegister} from "../../shared/models/IUserRegister";
import {AccountService} from "../account.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  hide:boolean = true;
  newUser = {} as IUserRegister;

  constructor(private accountService: AccountService) {
  }

  register(newUser: IUserRegister) {
    this.accountService.register(newUser).subscribe((res=>{
      console.log(res)
    }))


  }
}
