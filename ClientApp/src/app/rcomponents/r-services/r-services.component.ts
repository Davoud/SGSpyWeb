import { Component, OnInit, Input } from '@angular/core';
import { ServerService } from '../../server.service';
import { RServiceInterface } from '../Model';

@Component({
  selector: 'app-r-services',
  templateUrl: './r-services.component.html',
  styleUrls: ['./r-services.component.css']
})
export class RServicesComponent implements OnInit {

  component: string;
  interfaces: RServiceInterface[];

  @Input('componentId') set componentId(value: string) {
    if (this.component === value) return;
    this.component = value;
    let dot = value.indexOf('.');
    this.server.getServices(value.substring(0, dot), value.substring(dot + 1)).subscribe((ints: RServiceInterface[]) => {
      this.interfaces = ints;
    })
  }

  constructor(private server: ServerService) { }

  ngOnInit() {
    
  }

}
