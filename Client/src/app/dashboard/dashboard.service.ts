import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {ITask} from "../shared/models/task/ITask";
import {ITaskResult} from "../shared/models/task/ITaskResult";

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  baseUrl = "https://localhost:5001/api/task";


  constructor(private http:HttpClient) { }

  getAllTasks() {
    return this.http.get<ITaskResult[]>(this.baseUrl);
  }

  getTask(){

  }

  addTask(task: ITask){
    return this.http.post(this.baseUrl+"/new", task);

  }

  update(task: ITask){

  }

  removeTask(task: ITask){

  }
}
