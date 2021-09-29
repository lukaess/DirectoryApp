import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TESTComponent } from './test.component';

describe('TESTComponent', () => {
  let component: TESTComponent;
  let fixture: ComponentFixture<TESTComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TESTComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TESTComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
