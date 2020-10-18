import { Component, OnInit, Input, Inject } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { REntity } from '../Model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-r-entities',
  templateUrl: './r-entities.component.html',
  styleUrls: ['./r-entities.component.css']
})
export class REntitiesComponent implements OnInit {

  private _parentId: string;

  @Input('componentId') set componentId(value: string) {
    if (this._parentId === value) return;
    this._parentId = value;
    this.getEntities();
  }

  entities: REntity[];
  baseUrl: string;
  
  constructor(private http: HttpClient, private route: ActivatedRoute, @Inject('BASE_URL') baseUrl: string)
  {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {    
    this.getEntities();
  }

  getEntities() {
    this.http.get<REntity[]>(this.baseUrl + `rcomponents/${this._parentId}/entities`).subscribe(entities => {
      this.entities = entities;
    });
  }
}
