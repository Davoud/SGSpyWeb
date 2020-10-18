import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Domain } from '../Model';


@Component({
  selector: 'app-explorer',
  templateUrl: './explorer.component.html',
  styleUrls: ['./explorer.component.css']
})
export class ExplorerComponent implements OnInit {

  domains: Domain[];
  baseUrl: string;

  
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.getDomains();
  }

  getDomains() {
    this.http.get<Domain[]>(this.baseUrl + "rcomponents").subscribe(domains => {
      this.domains = domains;
    });
  }

  
}
