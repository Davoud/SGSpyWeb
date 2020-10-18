import { Component, OnInit } from '@angular/core';
import { ServerService } from '../../server.service';
import { RTreeNode } from '../Model';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';


@Component({
  selector: 'app-r-explorer',
  templateUrl: './r-explorer.component.html',
  styleUrls: ['./r-explorer.component.css']
})
export class RExplorerComponent implements OnInit {

  treeControl = new NestedTreeControl<RTreeNode>(node => node.children);
  dataSource = new MatTreeNestedDataSource<RTreeNode>();
  hasChild = (_: number, node: RTreeNode) => !!node.children && node.children.length > 0;
  constructor(private server: ServerService) { }

  ngOnInit() {
    this.server.getTree().subscribe(nodes => {
      this.dataSource.data = nodes;
    })
  }

}
