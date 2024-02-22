import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Story } from '../../models/Story';

@Injectable({
  providedIn: 'root',
})
export class StoryService {
  private apiRoute: string = 'http://localhost:5119/api/Stories';

  constructor(private http: HttpClient) {}

  differenceOfVotes = (story: Story): number => {
    const upVotes = story.votes.filter((voto) => voto.upVote).length;
    const downVotes = story.votes.length - upVotes;
    return upVotes - downVotes;
  };

  getAll = (): Observable<any> => {
    return this.http.get(this.apiRoute);
  };

  delete = (id: number): Observable<any> => {
    return this.http.delete(`${this.apiRoute}/${id}`);
  };

  add = (
    title: string,
    description: string,
    departament: string
  ): Observable<any> => {
    return this.http.post(this.apiRoute, {
      title: title,
      description: description,
      departament: departament,
    });
  };

  update = (story: Story): Observable<any> => {
    return this.http.put(`${this.apiRoute}/${story.id}`, {
      title: story.title,
      description: story.description,
      departament: story.departament,
    });
  };
}
