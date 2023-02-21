import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {ITask} from "../shared/models/ITask";

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  baseUrl = environment.apiUrl;
  constructor(private http:HttpClient) { }

  getAllTasks(){

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
