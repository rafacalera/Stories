import { Story } from './models/Story';
import { StoryService } from './services/story/story.service';
import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
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

  constructor(private _storyService: StoryService) {}

  ngOnInit() {
    this._storyService.getAll().subscribe((data: Array<any>) => {
      this.stories = data;
      this.orderByVote(this.stories);
    });
  }

  orderByVote = (storyArray: Story[]) => {
    storyArray.sort(
      (a, b) =>
        this._storyService.differenceOfVotes(b) -
        this._storyService.differenceOfVotes(a)
    );
  };

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

    this._storyService.vote(storyId, this.userId, upVote).subscribe({
      next: (vote: Vote) => {
        let i: number = this.stories.findIndex((e) => e.id == storyId);
        this.stories[i].votes.push(vote);

        this.orderByVote(this.stories);

        return alert('Vote registred succefully!');
      },
      error: (error: HttpErrorResponse) => {
        if (error.status === 400) {
          if (error.error.includes('User already vote')) {
            return alert('This user already voted in this story!');
          }
          return alert("User doesn't exists");
        }

        if (error.status === 404) {
          return alert("Story doesn't exists");
        }

        return alert('Communication error\n try again later');
      },
    });
  };
}
