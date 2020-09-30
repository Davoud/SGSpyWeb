import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-r-services',
  templateUrl: './r-services.component.html',
  styleUrls: ['./r-services.component.css']
})
export class RServicesComponent implements OnInit {



  @Input('componentId') parentId: string;

  constructor() { }

  ngOnInit() {
  }

}
