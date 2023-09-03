/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DayPlanService } from './day-plan.service';

describe('Service: DayPlan', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DayPlanService]
    });
  });

  it('should ...', inject([DayPlanService], (service: DayPlanService) => {
    expect(service).toBeTruthy();
  }));
});
