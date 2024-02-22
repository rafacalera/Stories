import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
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
    CommonModule,
  ],
})
export class FormComponent {
  @Input() editMode!: boolean;
  @Input() story!: Story;
  @Output() closeForm = new EventEmitter();
  @Output() storyUpdated = new EventEmitter();
  @Output() storyAdded = new EventEmitter();

  constructor(private _storyService: StoryService) {}

  handleClose = (): void => {
    this.closeForm.emit('Closing window');
  };

  handleUpdate = (event: Event): void => {
    event.preventDefault();

    this._storyService.update(this.story).subscribe({
      next: (data) => {
        this.storyUpdated.emit(this.story);

        this.handleClose();
      },
      error: (error) => {
        if (error.status === 400) return alert(`Fill in the fields correctly`);
        return alert(`Communication error\n try again later`);
      },
    });
  };

  handleAdd = (event: Event): void => {
    event.preventDefault();

    this._storyService
      .add(this.story.title, this.story.description, this.story.departament)
      .subscribe({
        next: (data) => {
          this.storyAdded.emit(
            new Story(
              data.id,
              data.title,
              data.description,
              data.departament,
              []
            )
          );

          this.handleClose();
        },
        error: (error) => {
          if (error.status === 400)
            return alert(`Fill in the fields correctly`);
          return alert(`Communication error\n try again later`);
        },
      });
  };
}
