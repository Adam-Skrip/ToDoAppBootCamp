import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {IUser} from "../shared/models/IUser";
import {environment} from "../../environments/environment";
import {IUserRegister} from "../shared/models/IUserRegister";
import {catchError, EMPTY, map, Observable, ReplaySubject, Subject} from "rxjs";
import {Router} from "@angular/router";
import {MessageService} from "../shared/snackbar/message.service";
import {IList} from "../shared/models/list/IList";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  public currentUser = new Subject<IUser|null>()
  public currentUser$ = this.currentUser.asObservable()



  baseUrl = environment.apiUrl+ 'auth/';

  constructor(private http: HttpClient, private router: Router, private messageService: MessageService) { }

  login(user: IUser) {
    return this.http.post(this.baseUrl + "login",user, {responseType:"text"}).pipe(
      map(response => {
        this.setToken(response, user);
      }),
      catchError(err => this.processError(err))

    )
  }

  register(newUser: IUserRegister){
    return this.http.post(this.baseUrl + "register", newUser,{observe:"response"}).pipe(
      map(response => {
        if (response.status == 201){
          this.router.navigateByUrl('login').then(r => {
            this.messageService.successMessage("User was successfully registered")
          })
        }
      }),
      catchError(err => this.processError(err))
    );

  }

  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('user')
    this.currentUser.next(null);
    this.router.navigateByUrl('home').then(() => {
      this.messageService.successMessage("Logout successfully")
    } );
  }

  isLoggedIn() {
    return !!localStorage.getItem('token')
  }

  private  setToken(value: string, user: IUser) {
    if (value) {
      localStorage.setItem('token', value);
      localStorage.setItem('user',user.username)
      this.router.navigateByUrl("dashboard").then(() => {
        this.messageService.successMessage("Logged in successfully")
      } )
      this.currentUser.next(user);
    } else {
      localStorage.removeItem('token');
      localStorage.removeItem('user')
      this.currentUser.next(null)
    }
  }


  processError(error:any): Observable<never> {
    if (error instanceof HttpErrorResponse) {
      if (error.status === 0) {
        this.messageService.errorMessage("Server unavailable");
        return EMPTY;
      }
      if (error.status === 401) {
        this.messageService.errorMessage("Wrong credentials");
        return EMPTY;
      }
      if (error.status === 400){
        this.messageService.errorMessage("Bad request");
        return EMPTY;
      }
      if (error.status <= 500) {
        console.log(error.error)
        this.messageService.errorMessage(error.error);
        return EMPTY;
      }
      this.messageService.errorMessage("Server failed");
    }
    console.error(error);
    return EMPTY;
  }
}
