import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { unauthorizationGuard } from './unauthorization.guard';

describe('unauthorizationGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => unauthorizationGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
