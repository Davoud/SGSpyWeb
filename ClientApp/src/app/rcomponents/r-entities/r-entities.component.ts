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
 
  @Input('componentId') set componentId(value: string) {
    if (this.component === value) return;
    this.component = value;  
    let dot = value.indexOf('.');
    this.server.getEntities(value.substring(0, dot), value.substring(dot + 1)).subscribe((entities: REntity[]) => {
      this.entities = entities;
    })
  }

  entities: REntity[];
  

  constructor(private server: ServerService)
  {
    
  }

  ngOnInit() {    
    
  }

  
}
