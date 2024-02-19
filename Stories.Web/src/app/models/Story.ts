import { Vote } from './Vote';

export class Story {
  constructor(
    id: number,
    title: string,
    description: string,
    departament: string,
    votes: Array<Vote>
  ) {
    this.id = id;
    (this.title = title), (this.description = description);
    this.departament = departament;
    this.votes = votes;
  }

  id: number;
  title: string;
  description: string;
  departament: string;
  votes: Array<Vote>;
}
