import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { StaffRoutingModule } from "./staff-routing.module";
import { ComponentsModule } from "src/app/components/components.module";
import { StaffComponent } from "./staff.component";
import { BrokerModule } from "../broker/broker.module";
import { AreaManagerModule } from "../area-manager/area-manager.module";
import { TeamModule } from "../team/team.module";

@NgModule({
  declarations: [StaffComponent],
  imports: [
    CommonModule,
    StaffRoutingModule,
    ComponentsModule,
    BrokerModule,
    AreaManagerModule,
    TeamModule
  ],
})
export class StaffModule {}
