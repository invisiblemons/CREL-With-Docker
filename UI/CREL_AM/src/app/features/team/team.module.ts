import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ComponentsModule } from 'src/app/components/components.module';
import { TeamComponent } from './team.component';
import { TeamDetailComponent } from './team-detail/team-detail.component';
import { TeamListComponent } from './team-list/team-list.component';
import { TeamRoutingModule } from './team-routing.module';
import { BrokerModule } from '../broker/broker.module';
import { PropertyModule } from '../property/property.module';
import { AreaManagerModule } from '../area-manager/area-manager.module';

@NgModule({
  declarations: [TeamComponent, TeamDetailComponent, TeamListComponent],
  imports: [
    CommonModule,
    TeamRoutingModule,
    ComponentsModule,
    BrokerModule,
    PropertyModule,
    AreaManagerModule
  ],
  exports: [TeamComponent, TeamDetailComponent, TeamListComponent],
})
export class TeamModule {}
