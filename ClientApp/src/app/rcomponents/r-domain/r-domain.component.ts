import { Component, OnInit } from '@angular/core';
import { Params, ActivatedRoute } from '@angular/router';
import { ServerService } from '../../server.service';
import { RComponent } from '../Model';

@Component({
  selector: 'app-r-domain',
  templateUrl: './r-domain.component.html',
  styleUrls: ['./r-domain.component.css']
})
export class RDomainComponent implements OnInit {

  domain: string = "";
  components: RComponent[] = [];

  constructor(private route: ActivatedRoute, private server: ServerService) { }

  ngOnInit() {
    this.route.params.subscribe((p: Params) => {
      this.domain = p['domain'];
      this.server.getComponents(this.domain).subscribe((cmp: RComponent[]) => {
        this.components = cmp;
      })
    });


  }

}
