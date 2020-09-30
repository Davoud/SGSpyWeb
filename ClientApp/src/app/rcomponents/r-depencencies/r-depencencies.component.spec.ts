import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RDependenciesComponent } from './r-depencencies.component';

describe('RDepencenciesComponent', () => {
  let component: RDependenciesComponent;
  let fixture: ComponentFixture<RDependenciesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RDependenciesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RDependenciesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
