import { Component } from '@angular/core';
import {IUserRegister} from "../../shared/models/IUserRegister";
import {AccountService} from "../account.service";
import {MessageService} from "../../shared/snackbar/message.service";
import {Router} from "@angular/router";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {matchValidator} from "./CustomValidators";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm = new FormGroup({
    email: new FormControl('', [Validators.email, Validators.required, Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]),
    username: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(25), Validators.pattern('^[A-Za-zÀ-ž0-9]+$')]),
    password: new FormControl('', [Validators.required, Validators.minLength(8),
      matchValidator('confirmPasswordFormReg', true), Validators.pattern('^(?=.*[A-Z])(?=.*[a-z])(?=.*?[0-9])(?=.*?[!@#\$&*~]).{8,}$')]),
    passwordConfirmation: new FormControl('', [Validators.required, matchValidator('password')])
  })
  hide: boolean = true;
  newUser = {} as IUserRegister;


  constructor(private accountService: AccountService, private msgService: MessageService,private router: Router) {
  }

  register(newUser: IUserRegister) {
    this.accountService.register(newUser).subscribe()
  }

  createRegisterForm() {

  }


}

