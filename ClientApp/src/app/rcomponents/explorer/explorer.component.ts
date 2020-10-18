import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Domain } from '../Model';
import { ServerService } from '../../server.service';


@Component({
  selector: 'app-explorer',
  templateUrl: './explorer.component.html',
  styleUrls: ['./explorer.component.css']
})
export class ExplorerComponent implements OnInit {

  domains: Domain[];
 

  
  constructor(private server: ServerService) {
    
  }

  ngOnInit() {
    this.getDomains();
  }

  getDomains() {    
    this.server.getDomains().subscribe(domains => {
      this.domains = domains;
    });
  }

  
}
