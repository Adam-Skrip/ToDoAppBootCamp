import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {ITask} from "../shared/models/task/ITask";
import {ITaskResult} from "../shared/models/task/ITaskResult";
import {IListResult} from "../shared/models/list/IListResult";
import {IList} from "../shared/models/list/IList";
import {Observable, ReplaySubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  token! : string;

  newList = {} as IList;
  baseUrl = "https://localhost:5001/api/";



  constructor(private http:HttpClient) { }




  getAllTasks() {
    return this.http.get<ITaskResult[]>(this.baseUrl+"task");
  }

  getTask(){

  }



  addTask(task: ITask){
    return this.http.post(this.baseUrl+"task/new", task,this.httpOptions);

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
    this.newList = list;
    console.log(list)
  }


  getAllLists() : Observable<IListResult[]>{
    return this.http.get<IListResult[]>(this.baseUrl+"basket", this.httpOptions);
  }

  deleteList(id: string){
    return this.http.delete(this.baseUrl+"basket/remove?publicId="+ id, this.httpOptionsDelete);
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

  httpOptionsDelete = {
    headers: new HttpHeaders({
      'Content-Type': 'text/plain',
      'Authorization': "Bearer "+this.getToken() })
  };
}
