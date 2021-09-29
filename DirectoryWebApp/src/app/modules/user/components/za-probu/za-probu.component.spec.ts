import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ZaProbuComponent } from './za-probu.component';

describe('ZaProbuComponent', () => {
  let component: ZaProbuComponent;
  let fixture: ComponentFixture<ZaProbuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ZaProbuComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ZaProbuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
