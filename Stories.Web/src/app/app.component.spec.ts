import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Story } from './models/Story';
import { StoryService } from './services/story/story.service';
import { of, Observable, throwError } from 'rxjs';
import { FormsModule } from '@angular/forms';
import {
  BrowserAnimationsModule,
  NoopAnimationsModule,
} from '@angular/platform-browser/animations';
import { CardComponent } from './components/card/card.component';
import { VoteService } from './services/vote/vote.service';

describe('AppComponent', () => {
  let fixture: ComponentFixture<AppComponent>;
  let component: AppComponent;
  let mockStoryService: jasmine.SpyObj<StoryService>;
  let mockVoteService: jasmine.SpyObj<VoteService>;
  let stories: Array<Story>;

  beforeEach(async () => {
    mockStoryService = jasmine.createSpyObj('StoryService', [
      'getAll',
      'differenceOfVotes',
    ]);

    mockVoteService = jasmine.createSpyObj('VoteService', ['add']);

    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatSelectModule,
        MatIconModule,
        FormsModule,
        BrowserAnimationsModule,
        NoopAnimationsModule,
        CardComponent,
      ],
      declarations: [AppComponent],
      providers: [
        { provide: StoryService, useValue: mockStoryService },
        { provide: VoteService, useValue: mockVoteService },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);

    component = fixture.componentInstance;
    stories = [
      new Story(1, 'Title', 'Description', 'Departament', []),
      new Story(2, 'Title2', 'Description2', 'Departament2', []),
    ];
    mockStoryService.getAll.and.returnValue(of(stories));
    mockStoryService.differenceOfVotes.and.callThrough();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it(`should have as title 'Stories of Product Owner'`, () => {
    expect(component.title).toEqual('Stories of Product Owner');
  });

  it('should display a mat-select', () => {
    const matSelect = fixture.nativeElement.querySelector('mat-select');
    expect(matSelect).toBeTruthy();
  });

  it('should trigger handleAdd method on button click', async () => {
    spyOn(component, 'handleAdd');
    const addButton = fixture.nativeElement.querySelector('button');
    addButton.click();

    await fixture.whenStable();
    fixture.detectChanges();

    expect(component.handleAdd).toHaveBeenCalled();
  });

  it('should render the same number of cards as stories.length', () => {
    fixture.detectChanges();

    const cards = fixture.nativeElement.querySelectorAll('app-card');
    expect(cards.length).toBe(stories.length);
  });

  it('should set editMode to false and openForm to true on handleAdd', () => {
    expect(component.editMode).toBe(false);
    expect(component.openForm).toBe(false);

    component.handleAdd();

    expect(component.editMode).toBe(false);
    expect(component.openForm).toBe(true);
  });

  it('should delete from array the story of id provided for onStoryDeleted', async () => {
    fixture.detectChanges();

    expect(component.stories.length).toBe(2);

    component.onStoryDeleted(1);

    expect(component.stories.length).toBe(1);
    expect(component.stories[0].id).toBe(2);
  });

  it('should set editMode true, openForm true and create a new story for editStory when onOpenUpdateForm is called', async () => {
    fixture.detectChanges();

    expect(component.editMode).toBe(false);
    expect(component.openForm).toBe(false);
    expect(component.editStory).toEqual(new Story(1, '', '', '', []));

    component.onOpenUpdateForm(stories[1]);

    expect(component.editMode).toBe(true);
    expect(component.openForm).toBe(true);
    expect(component.editStory).toEqual(stories[1]);
  });

  it('should set openForm false and reset the story from editStory when onCloseForm is called', async () => {
    fixture.detectChanges();

    const oldStory: Story = new Story(
      1,
      'Title',
      'Description',
      'Departament',
      []
    );
    const resetedStory: Story = new Story(1, '', '', '', []);
    component.editStory = oldStory;
    component.openForm = true;

    expect(component.openForm).toBe(true);
    expect(component.editStory).toEqual(oldStory);

    component.onCloseForm();

    expect(component.openForm).toBe(false);
    expect(component.editStory).toEqual(resetedStory);
  });

  it('should update the current stories array changeing the story of the id provided when onStoryUpdated is called ', async () => {
    fixture.detectChanges();

    const updatedStory: Story = new Story(
      2,
      'New Title',
      'New Description',
      'New Departament',
      []
    );

    component.onStoryUpdated(updatedStory);

    expect(component.stories[1]).toEqual(updatedStory);
  });

  it('should add a new story to stories array when onStoryAdded is called', () => {
    fixture.detectChanges();

    const newStory: Story = new Story(
      3,
      'New Story',
      'New Description',
      'New Departament',
      []
    );

    component.onStoryAdded(newStory);

    expect(component.stories.length).toBe(3);
    expect(component.stories[2]).toEqual(newStory);
  });

  it('should display an alert saying that Vote was registred succefully onNewVote call', () => {
    fixture.detectChanges();
    spyOn(window, 'alert');

    component.userId = 1;
    const storyId = 1;
    const upVote = true;

    mockVoteService.add.and.returnValue(of(null));

    component.onNewVote(upVote, storyId);

    expect(mockVoteService.add).toHaveBeenCalledWith({
      storyId: storyId,
      userId: component.userId,
      upVote: upVote,
    });

    expect(window.alert).toHaveBeenCalledWith('Vote registred succefully!');
  });

  it('should handle error 400 displaying an alert onNewVote call', () => {
    fixture.detectChanges();
    spyOn(window, 'alert');

    component.userId = 1;
    const storyId = 1;
    const upVote = true;
    const error = { status: 400 };

    mockVoteService.add.and.returnValue(throwError(() => error));

    component.onNewVote(upVote, storyId);

    expect(window.alert).toHaveBeenCalledWith(
      'This user already voted in this story!'
    );
  });

  it('should handle error 500 displaying an alert onNewVote call', () => {
    fixture.detectChanges();
    spyOn(window, 'alert');

    component.userId = 1;
    const storyId = 1;
    const upVote = true;
    const error = { status: 500 };

    mockVoteService.add.and.returnValue(throwError(() => error));

    component.onNewVote(upVote, storyId);

    expect(window.alert).toHaveBeenCalledWith(
      'Communication error\n try again later'
    );
  });
});
