import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { StudentEditComponent } from '../students/student-edit/student-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChanges implements CanDeactivate<StudentEditComponent> {

  canDeactivate(component: StudentEditComponent) {
    if (component.editForm.dirty) {
      return confirm('Chcesz opuścić tę podstronę? Niezapisane zmiany zostaną utracone.');
    }
    return true;
  }

}