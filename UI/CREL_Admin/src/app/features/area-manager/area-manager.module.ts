import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AreaManagerComponent } from "./area-manager.component";
import { AreaManagerDetailComponent } from "./area-manager-detail/area-manager-detail.component";
import { ComponentsModule } from "src/app/components/components.module";
import { AreaManagerNewComponent } from "./area-manager-new/area-manager-new.component";
import { AreaManagerEditComponent } from "./area-manager-edit/area-manager-edit.component";
import { AreaManagerListComponent } from "./area-manager-list/area-manager-list.component";
import { TeamListComponent } from "./area-manager-detail/team-list/team-list.component";

@NgModule({
  declarations: [
    AreaManagerComponent,
    AreaManagerDetailComponent,
    AreaManagerNewComponent,
    AreaManagerEditComponent,
    AreaManagerListComponent,
    TeamListComponent
  ],
  imports: [CommonModule, ComponentsModule],
  exports: [
    AreaManagerComponent,
    AreaManagerDetailComponent,
    AreaManagerNewComponent,
    AreaManagerEditComponent,
    AreaManagerListComponent,
  ],
})
export class AreaManagerModule {}
