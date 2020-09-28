import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'panel',
  styleUrls: ['./explorer.component.css'],
  templateUrl: './panel.html',  
})
export class PanelComponent {
  @Input() opened = false;
  @Input() title: string;
  @Input() count: number = 0;
  @Output() toggle: EventEmitter<any> = new EventEmitter<any>();
}
