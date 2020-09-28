import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RServicesComponent } from './r-services.component';

describe('RServicesComponent', () => {
  let component: RServicesComponent;
  let fixture: ComponentFixture<RServicesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RServicesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RServicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
