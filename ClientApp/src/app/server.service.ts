import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Domain, RTreeNode, RComponent, REntity, RDependency } from './rcomponents/Model';



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

  getComponents(domain: string): Observable<RComponent[]> {
    return this.http.get<RComponent[]>(this.baseUrl + `rcomponents/${domain}`);
  }

  getEntities(domain: string, component: string): Observable<REntity[]> {
    return this.http.get<REntity[]>(this.baseUrl + `rcomponents/${domain}/${component}/entities`);
  }

  getDependencies(domain: string, component: string): Observable<RDependency[]> {
    return this.http.get<RDependency[]>(this.baseUrl + `rcomponents/${domain}/${component}/dependencies`);
  }
}
