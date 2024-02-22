import { TestBed, inject } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { VoteService } from './vote.service';

describe('VoteService', () => {
  let service: VoteService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [VoteService],
    });

    service = TestBed.inject(VoteService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should add a vote', () => {
    const voteData = {
      storyId: 1,
      userId: 123,
      upVote: true,
    };

    service.add(voteData).subscribe((response) => {
      expect(response).toBeTruthy();
    });

    const req = httpMock.expectOne('http://localhost:5119/api/Votes');
    expect(req.request.method).toBe('POST');
    req.flush({});
  });
});
