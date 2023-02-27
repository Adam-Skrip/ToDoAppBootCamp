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
    username: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(30), Validators.pattern('^[A-Za-zÀ-ž]+$')]),
    password: new FormControl('', [Validators.required, Validators.minLength(8),
      matchValidator('confirmPasswordFormReg', true)]),
    passwordConfirmation: new FormControl('', [Validators.required, matchValidator('password')])
  })
  hide: boolean = true;
  newUser = {} as IUserRegister;


  constructor(private accountService: AccountService) {
  }

  register(newUser: IUserRegister) {
    this.accountService.register(newUser).subscribe()
  }

  createRegisterForm() {

  }


  getErrorEmail() {
    if (this.registerForm.controls['email'].hasError('required')) {
      return 'You must enter a email';
    }
    return this.registerForm.controls['email'].hasError('email') ? 'Not a valid email' : '';
  }

  getErrorUsername() {
    if (this.registerForm.controls['username'].hasError('required')) {
      return 'You must enter a username';
    }
    return this.registerForm.controls['username'].hasError('username') ? 'Not a valid username' : '';
  }

  getErrorPassword() {
    if (this.registerForm.controls['password'].hasError('required')) {
      return 'You must enter a password';
    }
    return this.registerForm.controls['password'].hasError('password') ? 'Not a valid password' : '';
  }
  getErrorConfPassword() {
    if (this.registerForm.controls['passwordConfirmation'].hasError('required')) {
      return 'You must enter a confirm password';
    }
    return this.registerForm.controls['passwordConfirmation'].hasError('passwordConfirmation') ? 'Passwords are not matching' : '';
  }
}

