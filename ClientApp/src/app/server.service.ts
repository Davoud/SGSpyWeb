import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Domain, RTreeNode } from './rcomponents/Model';



@Injectable({
  providedIn: 'root'
})
export class ServerService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getDomains(): Observable<Domain[]> {
    return this.http.get<Domain[]>(this.baseUrl + "rcomponents");
  }

  getTree(): Observable<RTreeNode[]> {
    return this.http.get<RTreeNode[]>(this.baseUrl + "rexplorer");
  }
}
