import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TeamDetailComponent } from '../team/team-detail/team-detail.component';
import { TeamListComponent } from '../team/team-list/team-list.component';
import { TeamComponent } from './team.component';
const routes: Routes = [
  {
    path: '',
    component: TeamComponent,
    children: [
      {
        path: 'danh-sach',
        component: TeamListComponent,
        children: [
          {
            path: 'chi-tiet',
            component: TeamDetailComponent,
          },
        ],
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TeamRoutingModule {}
