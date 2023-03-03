import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';

import { TeamNewComponent } from './team-new.component';

@Injectable({
  providedIn: 'root'
})
export class TeamNewGuard implements CanDeactivate<TeamNewComponent> {
  canDeactivate(component: TeamNewComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.teamForm.dirty) {
      const name = component.teamForm.get('name')?.value || 'New Team';
      return confirm(`bạn có chắc muốn thoát khỏi tạo mới nhóm ?`);
    }
    return true;
  }
}
