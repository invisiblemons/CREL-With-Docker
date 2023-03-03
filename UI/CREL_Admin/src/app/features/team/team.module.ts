import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ComponentsModule } from "src/app/components/components.module";
import { TeamComponent } from "./team.component";
import { TeamDetailComponent } from "./team-detail/team-detail.component";
import { TeamListComponent } from "./team-list/team-list.component";
import { TeamNewComponent } from "./team-new/team-new.component";
import { TeamEditComponent } from "./team-edit/team-edit.component";
import { BrokerModule } from "../broker/broker.module";
import { PropertyModule } from "../property/property.module";
import { AreaManagerModule } from "../area-manager/area-manager.module";

@NgModule({
  declarations: [
    TeamComponent,
    TeamDetailComponent,
    TeamListComponent,
    TeamNewComponent,
    TeamEditComponent,
  ],
  imports: [CommonModule, ComponentsModule, BrokerModule, PropertyModule, AreaManagerModule],
  exports: [
    TeamComponent,
    TeamDetailComponent,
    TeamListComponent,
    TeamNewComponent,
    TeamEditComponent,
  ],
})
export class TeamModule {}
