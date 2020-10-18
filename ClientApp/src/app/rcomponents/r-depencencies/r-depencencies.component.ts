import { Component, OnInit, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RDependency } from '../Model';

@Component({
  selector: 'app-r-dependencies',
  templateUrl: './r-depencencies.component.html',
  styleUrls: ['./r-depencencies.component.css']
})
export class RDependenciesComponent implements OnInit {

  private _parentId: string;
  dependencies: RDependency[];

  @Input('componentId') set componentId(value: string) {
    if (this._parentId === value) return;
    this._parentId = value;
    this.getDependencies();
  }

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {
  }
  
  getDependencies() {
    this.http.get<RDependency[]>(this.baseUrl + `rcomponents/${this._parentId}/dependencies`).subscribe(dependencies => {
      this.dependencies = dependencies;      
    });
  }
}
