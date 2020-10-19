import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RDomainComponent } from './r-domain.component';

describe('RDomainComponent', () => {
  let component: RDomainComponent;
  let fixture: ComponentFixture<RDomainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RDomainComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RDomainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
