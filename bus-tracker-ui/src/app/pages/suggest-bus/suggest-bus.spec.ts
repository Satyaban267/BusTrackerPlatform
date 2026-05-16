import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuggestBus } from './suggest-bus';

describe('SuggestBus', () => {
  let component: SuggestBus;
  let fixture: ComponentFixture<SuggestBus>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SuggestBus]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SuggestBus);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
