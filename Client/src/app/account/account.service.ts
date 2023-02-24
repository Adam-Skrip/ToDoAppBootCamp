import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {IUser} from "../shared/models/IUser";
import {environment} from "../../environments/environment";
import {IUserRegister} from "../shared/models/IUserRegister";
import {catchError, EMPTY, map, Observable, ReplaySubject} from "rxjs";
import {Router} from "@angular/router";
import {MessageService} from "../shared/snackbar/message.service";
import {IList} from "../shared/models/list/IList";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private currentUserSource = new ReplaySubject<IUser | null>(1);
  currentUser$ = this.currentUserSource.asObservable();



  baseUrl = environment.apiUrl+ 'auth/';

  constructor(private http: HttpClient, private router: Router, private messageService: MessageService) { }

  login(user: IUser) {
    return this.http.post(this.baseUrl + "login",user, {responseType:"text"}).pipe(
      map(response => {
        console.log(response);
        this.setToken(response, user);
      }),
      catchError(err => this.processError(err))

    )
  }

  register(newUser: IUserRegister){
    return this.http.post(this.baseUrl + "register", newUser)

  }

  logout(){
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
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
      this.router.navigateByUrl("dashboard").then(() => {
        this.messageService.successMessage("Logged in successfully")
      } )
      this.currentUserSource.next(user);
    } else {
      localStorage.removeItem('token');
      this.currentUserSource.next(null)
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
      if (error.status < 500) {
        const message = error.error.errorMessage || JSON.parse(error.error).errorMessage;
        this.messageService.errorMessage(message);
        return EMPTY;
      }
      this.messageService.errorMessage("Server failed");
    }
    console.error(error);
    return EMPTY;
  }
}
