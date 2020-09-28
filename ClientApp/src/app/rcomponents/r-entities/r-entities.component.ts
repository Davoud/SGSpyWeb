import { Component, OnInit, Input, Inject } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { REntity } from '../Mode';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-r-entities',
  templateUrl: './r-entities.component.html',
  styleUrls: ['./r-entities.component.css']
})
export class REntitiesComponent implements OnInit {

  parentId: string;
  entities: REntity[];
  baseUrl: string;
  constructor(private http: HttpClient, private route: ActivatedRoute, @Inject('BASE_URL') baseUrl: string)
  {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.route.parent.params.subscribe((p: Params) => {
      this.parentId = p['id'];
      this.getEntities();
    });

  }

  getEntities() {
    this.http.get<REntity[]>(this.baseUrl + `rcomponents/${this.parentId}/entities`).subscribe(entities => {
      this.entities = entities;
    });
  }
}
