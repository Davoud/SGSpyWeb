import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { REntitiesComponent } from './r-entities.component';

describe('REntitiesComponent', () => {
  let component: REntitiesComponent;
  let fixture: ComponentFixture<REntitiesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ REntitiesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(REntitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
