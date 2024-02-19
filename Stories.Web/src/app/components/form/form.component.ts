import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { Story } from '../../models/Story';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { StoryService } from '../../services/story/story.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrl: './form.component.css',
  standalone: true,
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
  ],
})
export class FormComponent {
  @Input() story!: Story;
  @Output() closeFormEvent = new EventEmitter();
  @Output() storyUpdatedFormEvent = new EventEmitter();

  constructor(private _storyService: StoryService) {}

  handleClose() {
    this.closeFormEvent.emit('Closing window');
  }

  handleUpdate(event: Event) {
    event.preventDefault();

    this._storyService.update(this.story).subscribe((data) => {
      console.log(data);
    });

    this.storyUpdatedFormEvent.emit(this.story);
    this.handleClose();
  }
}
