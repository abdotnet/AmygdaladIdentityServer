import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProducthistoryComponent } from './producthistory.component';

describe('ProducthistoryComponent', () => {
  let component: ProducthistoryComponent;
  let fixture: ComponentFixture<ProducthistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProducthistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProducthistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
