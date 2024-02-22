import { TestBed, inject } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { StoryService } from './story.service';
import { Story } from '../../models/Story';
import { Vote } from '../../models/Vote';

describe('StoryService', () => {
  let service: StoryService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [StoryService],
    });

    service = TestBed.inject(StoryService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should calculate the difference of votes correctly', () => {
    const story: Story = new Story(1, 'Title', 'Description', 'Departament', [
      new Vote(1, true, 1),
      new Vote(2, true, 2),
      new Vote(3, false, 3),
    ]);

    const result = service.differenceOfVotes(story);

    expect(result).toBe(1);
  });

  it('should fetch stories from the API via GET', () => {
    const expectedStories: Story[] = [
      new Story(1, 'Title', 'Description', 'Departament', []),
      new Story(2, 'Title2', 'Description2', 'Departament2', []),
      new Story(3, 'Title3', 'Description3', 'Departament3', []),
    ];

    service.getAll().subscribe((stories) => {
      expect(stories).toEqual(expectedStories);
    });

    const req = httpMock.expectOne('http://localhost:5119/api/Stories');
    expect(req.request.method).toEqual('GET');

    req.flush(expectedStories);
  });

  it('should delete a story via DELETE', () => {
    const storyId = 1;

    service.delete(storyId).subscribe((res) => {
      expect(res.status).toBe(200);
    });

    const req = httpMock.expectOne(
      `http://localhost:5119/api/Stories/${storyId}`
    );
    expect(req.request.method).toEqual('DELETE');

    req.flush({ status: 200 });
  });

  it('should add a new story via POST', () => {
    const title = 'New Title';
    const description = 'New Description';
    const departament = 'New Departament';

    const newStory = new Story(1, title, description, departament, []);

    service.add(title, description, departament).subscribe((data) => {
      expect(data).toEqual(newStory);
    });

    const req = httpMock.expectOne('http://localhost:5119/api/Stories');
    expect(req.request.method).toEqual('POST');
    expect(req.request.body).toEqual({
      title: newStory.title,
      description: newStory.description,
      departament: newStory.departament,
    });

    req.flush(newStory);
  });

  it('should update a story via PUT', () => {
    const updatedStory: Story = new Story(
      1,
      'Updated Title',
      'Updated Description',
      'Updated Departament',
      []
    );

    service.update(updatedStory).subscribe((data) => {
      expect(data).toEqual(updatedStory);
    });

    const req = httpMock.expectOne(
      `http://localhost:5119/api/Stories/${updatedStory.id}`
    );
    expect(req.request.method).toEqual('PUT');
    expect(req.request.body).toEqual({
      title: updatedStory.title,
      description: updatedStory.description,
      departament: updatedStory.departament,
    });

    req.flush(updatedStory);
  });
});
