import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {ITask} from "../shared/models/ITask";
import {ITaskResult} from "../shared/models/ITaskResult";

@Injectable({
  providedIn: 'root'
})
export class DashboardService {


  constructor(private http:HttpClient) { }

  getAllTasks() {
    return this.http.get<ITaskResult[]>("http://localhost:5001/api/task");
  }

  getTask(){

  }

  addTask(task: ITask){

  }

  update(task: ITask){

  }

  removeTask(task: ITask){

  }
}
