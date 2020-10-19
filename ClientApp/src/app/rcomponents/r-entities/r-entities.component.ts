import { Component, OnInit, Input, Inject } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { REntity } from '../Model';
import { HttpClient } from '@angular/common/http';
import { ServerService } from '../../server.service';

@Component({
  selector: 'app-r-entities',
  templateUrl: './r-entities.component.html',
  styleUrls: ['./r-entities.component.css']
})
export class REntitiesComponent implements OnInit {

  private component: string;
  private domain: string;

  @Input('componentId') set componentId(value: { domain: string, component: string }) {
    if (this.component === value.component && this.domain === value.domain) return;
    this.component = value.component;
    this.domain = value.domain
    this.server.getEntities(this.domain, this.component).subscribe(entitis => {
      this.entities = this.entities;
    })
  }

  entities: REntity[];
  

  constructor(private server: ServerService)
  {
    
  }

  ngOnInit() {    
    
  }

  
}
