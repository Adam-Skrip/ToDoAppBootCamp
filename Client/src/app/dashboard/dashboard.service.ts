import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {ITask} from "../shared/models/task/ITask";
import {ITaskResult} from "../shared/models/task/ITaskResult";
import {IListResult} from "../shared/models/list/IListResult";
import {IList} from "../shared/models/list/IList";
import {ReplaySubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  token! : string;
  baseUrl = "https://localhost:5001/api/";



  constructor(private http:HttpClient) { }




  getAllTasks() {
    return this.http.get<ITaskResult[]>(this.baseUrl+"task");
  }

  getTask(){

  }



  addTask(task: ITask){
    return this.http.post(this.baseUrl+"task/new", task);

  }

  update(task: ITask){

  }

  removeTask(task: ITask){

  }

  addList(list:IList){
    console.log(list)
    console.log(this.httpOptions)
    return this.http.post(this.baseUrl+"basket/new", list, this.httpOptions );
  }

  getNewListName(list: IList){
    console.log(list)
  }


  getAllLists(){
    return this.http.get<IListResult[]>(this.baseUrl+"basket", this.httpOptions);
  }


  getToken() : string{
    let tempToken = localStorage.getItem('token');
    if (tempToken){
      this.token = tempToken.slice(1,-1);
    }
    return this.token;

  }


  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': "Bearer "+this.getToken() })
  };
}
