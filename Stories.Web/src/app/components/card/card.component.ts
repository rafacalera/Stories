import { AppComponent } from './../../app.component';
import { MatDividerModule } from '@angular/material/divider';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { Story } from '../../models/Story';
import { StoryService } from '../../services/story/story.service';
import { VoteService } from '../../services/vote/vote.service';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css'],
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatDividerModule, MatIconModule],
})
export class CardComponent {
  @Input() story!: Story;
  @Output() storyDeleted = new EventEmitter();
  @Output() openUpdateForm = new EventEmitter();
  @Output() newVote = new EventEmitter();

  constructor(private _storyService: StoryService) {}

  handleDelete = (): void => {
    this._storyService.delete(this.story.id).subscribe((data) => {
      this.storyDeleted.emit(`story of id "${this.story.id}" deleted`);
    });
  };

  handleEdit = (): void => {
    this.openUpdateForm.emit(`editing story of id "${this.story.id}"`);
  };

  handleVote = (upVote: boolean): void => {
    this.newVote.emit(upVote);
  };
}
