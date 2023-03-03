import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ComponentsModule } from "src/app/components/components.module";
import { AppointmentNewComponent } from "./appointment-new/appointment-new.component";
import { AppointmentListComponent } from "./appointment-list/appointment-list.component";
import { AppointmentEditComponent } from "./appointment-edit/appointment-edit.component";
import { AppointmentDetailComponent } from "./appointment-detail/appointment-detail.component";
import { BrandDetailComponent } from "./brand-detail/brand-detail.component";
import { PropertyDetailComponent } from "./property-detail/property-detail.component";
import { BrokerDetailComponent } from "./broker-detail/broker-detail.component";
import { AppointmentComponent } from "./appointment.component";

@NgModule({
  declarations: [
    AppointmentComponent,
    AppointmentDetailComponent,
    AppointmentNewComponent,
    AppointmentListComponent,
    AppointmentEditComponent,
    PropertyDetailComponent,
    BrokerDetailComponent,
    BrandDetailComponent,
  ],
  imports: [CommonModule, ComponentsModule],
  exports: [
    AppointmentComponent,
    AppointmentDetailComponent,
    AppointmentNewComponent,
    AppointmentListComponent,
    AppointmentEditComponent,
  ],
})
export class AppointmentModule {}
