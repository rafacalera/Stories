import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface addVote {
  storyId: number;
  userId: number;
  upVote: boolean;
}

@Injectable({
  providedIn: 'root',
})
export class VoteService {
  private apiRoute: string = 'http://localhost:5119/api/Votes';

  constructor(private http: HttpClient) {}

  add = (vote: addVote): Observable<any> => {
    return this.http.post(this.apiRoute, vote);
  };
}
