import { Story } from './models/Story';
import { StoryService } from './services/story/story.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'Stories of Product Owner';
  editStory: Story = new Story(1, '', '', '', []);
  stories!: Array<Story>;
  editMode: boolean = false;
  openForm: boolean = false;

  constructor(private _storyService: StoryService) {}

  ngOnInit() {
    this._storyService.getAll().subscribe((data: Array<any>) => {
      this.stories = data.sort(
        (a, b) =>
          this._storyService.differenceOfVotes(b) -
          this._storyService.differenceOfVotes(a)
      );
    });
  }

  handleDeleteEvent = (id: number) => {
    this.stories = this.stories.filter((i) => i.id != id);
  };

  handleOpenUpdateFormEvent = (story: Story) => {
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

  handleCloseFormEvent = (): void => {
    this.openForm = false;

    this.editStory = new Story(1, '', '', '', []);
  };

  handleStoryUpdatedFormEvent = (updatedStory: Story): void => {
    let i: number = this.stories.findIndex((e) => e.id == updatedStory.id);

    this.stories[i] = updatedStory;
  };

  handleAdd = (): void => {
    this.editMode = false;
    this.openForm = true;
  };

  handleStoryAddedFormEvent = (addedStory: Story): void => {
    this.stories.push(addedStory);
  };
}
