import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RExplorerComponent } from './r-explorer.component';

describe('RExplorerComponent', () => {
  let component: RExplorerComponent;
  let fixture: ComponentFixture<RExplorerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RExplorerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RExplorerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
