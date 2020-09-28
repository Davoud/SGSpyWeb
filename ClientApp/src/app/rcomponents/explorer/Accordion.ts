import { Component, ContentChildren, QueryList, AfterContentInit } from '@angular/core';
import { PanelComponent } from './panel';

@Component({
  selector: 'accordion',
  template: '<ng-content></ng-content>'
})
export class AccordionComponent implements AfterContentInit {
  @ContentChildren(PanelComponent) panels: QueryList<PanelComponent>;

  ngAfterContentInit() {
    
    this.panels.toArray()[0].opened = true;
    
    this.panels.toArray().forEach((panel: PanelComponent) => {
    
      panel.toggle.subscribe(() => {        
        this.openPanel(panel);
      });
    });
  }

  openPanel(panel: PanelComponent) {    
    this.panels.toArray().forEach(p => p.opened = false);    
    panel.opened = true;
  }

  

}
