import { Story } from './../../models/Story';
/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CardComponent } from './card.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { StoryService } from '../../services/story/story.service';
import { of } from 'rxjs';

describe('CardComponent', () => {
  let component: CardComponent;
  let fixture: ComponentFixture<CardComponent>;
  let mockStoryService: jasmine.SpyObj<StoryService>;

  beforeEach(() => {
    mockStoryService = jasmine.createSpyObj('StoryService', ['delete']);

    TestBed.configureTestingModule({
      imports: [CardComponent, HttpClientTestingModule],
      providers: [{ provide: StoryService, useValue: mockStoryService }],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CardComponent);
    component = fixture.componentInstance;
    component.story = new Story(1, 'Title', 'Description', 'Departament', []);
    fixture.detectChanges();
  });

  it('should display story information on card', () => {
    const cardTitle: HTMLElement =
      fixture.nativeElement.querySelector('mat-card-title');
    const cardDescription: HTMLElement =
      fixture.nativeElement.querySelector('p');
    const cardDepartament: HTMLElement =
      fixture.nativeElement.querySelector('mat-card-subtitle');

    expect(cardTitle.textContent).toBe(component.story.title);
    expect(cardDescription.textContent).toBe(component.story.description);
    expect(cardDepartament.textContent).toBe(component.story.departament);
  });

  it('should trigger handleVote with parameter of true on click', () => {
    spyOn(component, 'handleVote');
    const voteUpButton = fixture.nativeElement.querySelectorAll('button')[0];
    voteUpButton.click();
    expect(component.handleVote).toHaveBeenCalledWith(true);
  });

  it('should trigger handleVote with parameter of false on click', () => {
    spyOn(component, 'handleVote');
    const voteDownButton = fixture.nativeElement.querySelectorAll('button')[1];
    voteDownButton.click();
    expect(component.handleVote).toHaveBeenCalledWith(false);
  });

  it('should trigger handleEdit on click', () => {
    spyOn(component, 'handleEdit');
    const editButton = fixture.nativeElement.querySelectorAll('button')[2];
    editButton.click();
    expect(component.handleEdit).toHaveBeenCalled();
  });

  it('should trigger handleDelete on click', () => {
    spyOn(component, 'handleDelete');
    const deleteButton = fixture.nativeElement.querySelectorAll('button')[3];
    deleteButton.click();
    expect(component.handleDelete).toHaveBeenCalled();
  });

  it('should emit storyDeleted event on handleDelete', () => {
    spyOn(component.storyDeleted, 'emit');
    mockStoryService.delete.and.returnValue(of(null));

    component.handleDelete();

    expect(mockStoryService.delete).toHaveBeenCalledWith(component.story.id);
    expect(component.storyDeleted.emit).toHaveBeenCalledWith(
      `story of id "${component.story.id}" deleted`
    );
  });

  it('should emit openUpdateForm event on handleEdit', () => {
    spyOn(component.openUpdateForm, 'emit');

    component.handleEdit();

    expect(component.openUpdateForm.emit).toHaveBeenCalledWith(
      `editing story of id "${component.story.id}"`
    );
  });

  it('should emit newVote event on handleVote', () => {
    const upVote: boolean = false;
    spyOn(component.newVote, 'emit');

    component.handleVote(upVote);

    expect(component.newVote.emit).toHaveBeenCalledWith(upVote);
  });
});
