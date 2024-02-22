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
import { of, Observable } from 'rxjs';
import { FormsModule } from '@angular/forms';
import {
  BrowserAnimationsModule,
  NoopAnimationsModule,
} from '@angular/platform-browser/animations';
import { CardComponent } from './components/card/card.component';

describe('AppComponent', () => {
  let fixture: ComponentFixture<AppComponent>;
  let component: AppComponent;
  let storyServiceSpy: jasmine.SpyObj<StoryService>;
  let stories: Array<Story>;

  beforeEach(async () => {
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
    }).compileComponents();
  });

  beforeEach(() => {
    storyServiceSpy = jasmine.createSpyObj('StoryService', [
      'getAll',
      'differenceOfVotes',
    ]);

    TestBed.configureTestingModule({
      declarations: [AppComponent],
      providers: [{ provide: StoryService, useValue: storyServiceSpy }],
    });

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    stories = [
      new Story(1, 'Title', 'Description', 'Departament', []),
      new Story(2, 'Title2', 'Description2', 'Departament2', []),
    ];
    storyServiceSpy.getAll.and.returnValue(of(stories));
    storyServiceSpy.differenceOfVotes.and.callThrough();
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

  it('should trigger handleAdd method on button click', () => {
    spyOn(component, 'handleAdd');
    const addButton = fixture.nativeElement.querySelector('button');
    addButton.click();
    expect(component.handleAdd).toHaveBeenCalled();
  });

  it('should render the same number of cards as stories.length', waitForAsync(() => {
    fixture.detectChanges();

    fixture.whenStable().then(() => {
      fixture.detectChanges();

      const cards = fixture.nativeElement.querySelectorAll('app-card');
      expect(cards.length).toBe(stories.length);
    });
  }));
});
