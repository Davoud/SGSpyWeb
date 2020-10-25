import { Component, OnInit, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RDependency } from '../Model';
import { ServerService } from '../../server.service';

@Component({
  selector: 'app-r-dependencies',
  templateUrl: './r-depencencies.component.html',
  styleUrls: ['./r-depencencies.component.css']
})
export class RDependenciesComponent implements OnInit {

  private component: string;
  dependencies: RDependency[];

  @Input('componentId') set componentId(value: string) {
    if (this.component === value) return;
    this.component = value;
    let dot = value.indexOf('.');
    this.server.getDependencies(value.substring(0, dot), value.substring(dot + 1)).subscribe((dependencies: RDependency[]) => {
      this.dependencies = dependencies;
    })
  }

  constructor(private server: ServerService) { }

  ngOnInit() {
  }
  
  
  
}
