import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {ITask} from "../shared/models/task/ITask";
import {ITaskResult} from "../shared/models/task/ITaskResult";
import {IListResult} from "../shared/models/list/IListResult";
import {IList} from "../shared/models/list/IList";
import {Observable, ReplaySubject, Subject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  token! : string;

  newList = {} as IList;
  baseUrl = environment.apiUrl;

  public isAdded = new Subject<boolean>()
  public idAdded$ = this.isAdded.asObservable()

  constructor(private http:HttpClient) { }

  //TASK
  addTask(task: ITask, basketId : string){
    return this.http.post(this.baseUrl+"task/new/?basketId="+basketId, task,this.httpOptions);

  }

  updateTask(id:string,task: ITask){
    return this.http.put(this.baseUrl+"task/update/?questId="+id,task, this.httpOptions);

  }

  deleteTask(id:string){
    return this.http.delete(this.baseUrl+"task/remove?publicId="+ id, this.httpOptionsDelete);
  }

  migrateTask(oldId:string, newId: string, taskId: string){
    return this.http.put<IListResult>(
      this.baseUrl+"basket/migrate?oldBasketId="+oldId+"&newBasketId="+newId+"&questId="+taskId,
      null,this.httpOptions)
  }


  //LIST
  addList(list:IList){
    return this.http.post(this.baseUrl+"basket/new", list, this.httpOptions );
  }




  getAllLists() : Observable<IListResult[]>{
    return this.http.get<IListResult[]>(this.baseUrl+"basket", this.httpOptions);
  }

  updateList(id:string, name:string){
    return this.http.put<IListResult>(this.baseUrl+"basket/update?publicId="+id+"&newBasket="+name,null,this.httpOptions)
  }
  deleteList(id: string){
    return this.http.delete(this.baseUrl+"basket/remove?publicId="+ id, this.httpOptionsDelete);
  }

  //TOKEN
  getToken() : string{
    let tempToken = localStorage.getItem('token');
    if (tempToken){
      this.token = tempToken.slice(1,-1);
    }
    return this.token;

  }

  //AUTH
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
