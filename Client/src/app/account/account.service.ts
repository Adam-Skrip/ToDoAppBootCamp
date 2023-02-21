import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IUser} from "../shared/models/IUser";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  login(user: IUser){

  }

  register(user: IUser){

  }

  logout(){
    localStorage.removeItem('token');
  }

  loggedIn() {
    return !!localStorage.getItem('token')  //double negate, so it will always return boolean
  }
}
