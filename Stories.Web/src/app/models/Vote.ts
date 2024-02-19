export class Vote {
  constructor(id: number, upVote: boolean, userId: number) {
    this.id = id;
    (this.upVote = upVote), (this.userId = userId);
  }

  id: number;
  upVote: boolean;
  userId: number;
}
