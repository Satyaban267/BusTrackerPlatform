import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterBus } from './register-bus';

describe('RegisterBus', () => {
  let component: RegisterBus;
  let fixture: ComponentFixture<RegisterBus>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterBus]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterBus);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
