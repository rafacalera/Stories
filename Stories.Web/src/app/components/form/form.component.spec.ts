import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormComponent } from './form.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { Story } from '../../models/Story';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { StoryService } from '../../services/story/story.service';
import { of, throwError } from 'rxjs';

describe('FormComponent', () => {
  let component: FormComponent;
  let fixture: ComponentFixture<FormComponent>;
  let mockStoryService: jasmine.SpyObj<StoryService>;
  let mockEvent: Event = new Event('click', {
    bubbles: true,
    cancelable: true,
  });

  beforeEach(async () => {
    mockStoryService = jasmine.createSpyObj('StoryService', ['update', 'add']);

    await TestBed.configureTestingModule({
      imports: [
        FormComponent,
        HttpClientTestingModule,
        BrowserAnimationsModule,
        NoopAnimationsModule,
      ],
      providers: [{ provide: StoryService, useValue: mockStoryService }],
    }).compileComponents();

    fixture = TestBed.createComponent(FormComponent);
    component = fixture.componentInstance;
    component.story = new Story(1, 'Title', 'Description', 'Departament', []);
    fixture.detectChanges();
  });

  it('should render a h1 with content of "Add a new story" if editMode false', () => {
    component.editMode = false;
    fixture.detectChanges();

    const editModeFalseH1: HTMLElement =
      fixture.nativeElement.querySelector('h1');

    expect(editModeFalseH1.textContent).toBe('Add a new story');
  });

  it('should render a h1 with content of "Editing Story of Id: " if editMode true', () => {
    component.editMode = true;
    fixture.detectChanges();

    const editModeTrueH1: HTMLElement =
      fixture.nativeElement.querySelector('h1');

    expect(editModeTrueH1.textContent).toBe(
      `Editing Story of Id: ${component.story.id}`
    );
  });

  it('should trigger handleClose on click', () => {
    component.editMode = false;
    fixture.detectChanges();

    spyOn(component, 'handleClose');
    const closeButton: HTMLButtonElement =
      fixture.nativeElement.querySelector('button');

    closeButton.click();
    expect(component.handleClose).toHaveBeenCalled();
  });

  it('should render the inputs with the value of the story', () => {
    component.editMode = true;
    fixture.detectChanges();

    const titleInput: HTMLInputElement =
      fixture.nativeElement.querySelectorAll('input')[0];

    const descriptionInput: HTMLInputElement =
      fixture.nativeElement.querySelector('textarea');

    const departamentInput: HTMLInputElement =
      fixture.nativeElement.querySelectorAll('input')[1];

    expect(titleInput.value).toBe(component.story.title);
    expect(descriptionInput.value).toBe(component.story.description);
    expect(departamentInput.value).toBe(component.story.departament);
  });

  it('should trigger handleUpdate on click', () => {
    component.editMode = true;
    fixture.detectChanges();

    spyOn(component, 'handleUpdate');
    const updateButton: HTMLButtonElement =
      fixture.nativeElement.querySelectorAll('button')[1];

    updateButton.click();
    expect(component.handleUpdate).toHaveBeenCalledWith(jasmine.any(Event));
  });

  it('should trigger handleAdd on click', () => {
    component.editMode = false;
    fixture.detectChanges();

    spyOn(component, 'handleAdd');
    const updateButton: HTMLButtonElement =
      fixture.nativeElement.querySelectorAll('button')[1];

    updateButton.click();
    expect(component.handleAdd).toHaveBeenCalledWith(jasmine.any(Event));
  });

  it('should emit closeForm event on handleClose', () => {
    spyOn(component.closeForm, 'emit');

    component.handleClose();

    expect(component.closeForm.emit).toHaveBeenCalledWith('Closing window');
  });

  it('should emit storyUpdated event on handleUpdate', () => {
    spyOn(component.storyUpdated, 'emit');
    spyOn(component, 'handleClose');

    mockStoryService.update.and.returnValue(of(component.story));

    component.handleUpdate(mockEvent);

    expect(mockStoryService.update).toHaveBeenCalledWith(component.story);
    expect(component.storyUpdated.emit).toHaveBeenCalledWith(component.story);
    expect(component.handleClose).toHaveBeenCalled();
  });

  it('should handle error 400 on handleUpdate', () => {
    spyOn(window, 'alert');
    const error = { status: 400 };

    mockStoryService.update.and.returnValue(throwError(() => error));

    component.handleUpdate(mockEvent);

    expect(window.alert).toHaveBeenCalledWith(`Fill in the fields correctly`);
  });

  it('should handle other errors on handleUpdate', () => {
    spyOn(window, 'alert');
    const error = { status: 500 };

    mockStoryService.update.and.returnValue(throwError(() => error));

    component.handleUpdate(mockEvent);

    expect(window.alert).toHaveBeenCalledWith(
      `Communication error\n try again later`
    );
  });

  it('should emit storyAdded event on handleAdd', () => {
    spyOn(component.storyAdded, 'emit');
    spyOn(component, 'handleClose');

    mockStoryService.add.and.returnValue(of(component.story));

    component.handleAdd(mockEvent);

    expect(mockStoryService.add).toHaveBeenCalledWith(
      component.story.title,
      component.story.description,
      component.story.departament
    );
    expect(component.storyAdded.emit).toHaveBeenCalledWith(component.story);
    expect(component.handleClose).toHaveBeenCalled();
  });

  it('should handle error 400 on handleAdd', () => {
    spyOn(window, 'alert');
    const error = { status: 400 };

    mockStoryService.add.and.returnValue(throwError(() => error));

    component.handleAdd(mockEvent);

    expect(window.alert).toHaveBeenCalledWith(`Fill in the fields correctly`);
  });

  it('should handle other errors on handleAdd', () => {
    spyOn(window, 'alert');
    const error = { status: 500 };

    mockStoryService.add.and.returnValue(throwError(() => error));

    component.handleAdd(mockEvent);

    expect(window.alert).toHaveBeenCalledWith(
      `Communication error\n try again later`
    );
  });
});
