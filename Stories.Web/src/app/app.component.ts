import { Story } from './models/Story';
import { StoryService } from './services/story/story.service';
import { Component, OnInit } from '@angular/core';
import { VoteService } from './services/vote/vote.service';
import { HttpResponse } from '@angular/common/http';
import { Vote } from './models/Vote';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'Stories of Product Owner';
  userId!: number;
  editStory: Story = new Story(1, '', '', '', []);
  stories!: Array<Story>;
  editMode: boolean = false;
  openForm: boolean = false;

  constructor(
    private _storyService: StoryService,
    private _voteService: VoteService
  ) {}

  ngOnInit() {
    this._storyService.getAll().subscribe((data: Array<any>) => {
      this.stories = data.sort(
        (a, b) =>
          this._storyService.differenceOfVotes(b) -
          this._storyService.differenceOfVotes(a)
      );
    });
  }

  handleAdd = (): void => {
    this.editMode = false;
    this.openForm = true;
  };

  onStoryDeleted = (id: number) => {
    this.stories = this.stories.filter((i) => i.id != id);
  };

  onOpenUpdateForm = (story: Story) => {
    this.editMode = true;
    this.openForm = true;
    this.editStory = new Story(
      story.id,
      story.title,
      story.description,
      story.departament,
      []
    );
  };

  onCloseForm = (): void => {
    this.openForm = false;

    this.editStory = new Story(1, '', '', '', []);
  };

  onStoryUpdated = (updatedStory: Story): void => {
    let i: number = this.stories.findIndex((e) => e.id == updatedStory.id);

    this.stories[i] = updatedStory;
  };

  onStoryAdded = (addedStory: Story): void => {
    this.stories.push(addedStory);
  };

  onNewVote = (upVote: boolean, storyId: number): void => {
    if (this.userId == null || undefined)
      return alert('An User is required to vote!');

    this._voteService
      .add({
        storyId: storyId,
        userId: this.userId,
        upVote: upVote,
      })
      .subscribe({
        next: () => {
          return alert(`Vote registred succefully!`);
        },
        error: (error) => {
          if (error.status === 400)
            return alert(`This user already voted in this story!`);
          return alert(`Communication error\n try again later`);
        },
      });
  };
}
