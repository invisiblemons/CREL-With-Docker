import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { TeamEditComponent } from './team-edit.component';


@Injectable({
  providedIn: 'root'
})
export class TeamEditGuard implements CanDeactivate<TeamEditComponent> {
  canDeactivate(component: TeamEditComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.teamForm.dirty) {
      const name = component.teamForm.get('name')?.value || 'New Team';
      return confirm(`bạn có chắc muốn thoát khỏi chỉnh sửa nhóm ?`);
    }
    return true;
  }
}
