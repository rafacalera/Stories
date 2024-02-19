import { AppComponent } from './../../app.component';
import { MatDividerModule } from '@angular/material/divider';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { Story } from '../../models/Story';
import { StoryService } from '../../services/story/story.service';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css'],
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatDividerModule, MatIconModule],
})
export class CardComponent {
  @Input() story!: Story;
  @Output() deleteEvent = new EventEmitter();
  @Output() openUpdateFormEvent = new EventEmitter();

  constructor(private _storyService: StoryService) {}

  handleDelete = (): void => {
    this._storyService.delete(this.story.id).subscribe((data) => {
      this.deleteEvent.emit(`story of id "${this.story.id}" deleted`);
    });
  };

  handleEdit = (): void => {
    this.openUpdateFormEvent.emit(`editing story of id "${this.story.id}"`);
  };
}
