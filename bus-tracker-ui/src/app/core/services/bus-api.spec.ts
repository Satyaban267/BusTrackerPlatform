import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';

import { BusApiService, Bus } from './bus-api';

describe('BusApiService', () => {
  let service: BusApiService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    // 1. Configure the testing module with HTTP providers
    TestBed.configureTestingModule({
      providers: [
        BusApiService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });
    
    // 2. Inject the service and the testing controller
    service = TestBed.inject(BusApiService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    // 3. Ensure that there are no outstanding HTTP requests after each test
    httpMock.verify();
  });

  // Test 1: Check if the service initializes correctly
  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  // Test 2: Check if getBuses makes a GET request and returns data
  it('should retrieve buses from the API via GET', () => {
    // Set up some dummy data that the "fake" backend will return
    const dummyBuses: Bus[] = [
      { id: 1, operatorName: 'Test Express', route: 'A to B', generalPrice: 15.00 },
      { id: 2, operatorName: 'Mock Transit', route: 'C to D', generalPrice: 25.00 }
    ];

    // Call the method we want to test
    service.getBuses().subscribe(buses => {
      // Assert that the data returned matches our dummy data
      expect(buses.length).toBe(2);
      expect(buses).toEqual(dummyBuses);
    });

    // Tell the HttpTestingController what URL to expect
    const request = httpMock.expectOne('http://localhost:5041/api/buses');
    
    // Assert that the request was a GET method
    expect(request.request.method).toBe('GET');
    
    // Flush the dummy data (this simulates the backend sending the response)
    request.flush(dummyBuses);
  });
});