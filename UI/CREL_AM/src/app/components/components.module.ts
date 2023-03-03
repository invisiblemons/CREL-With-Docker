import { CUSTOM_ELEMENTS_SCHEMA, LOCALE_ID, NgModule } from '@angular/core';
import { CommonModule, registerLocaleData } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import localeVi from '@angular/common/locales/vi';
registerLocaleData(localeVi);
import { SimpleScrollSpyModule } from 'angular-simple-scroll-spy';

import { InputRestrictionDirective } from '../shared/directives/inputRestrictionDirective';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {
  DxVectorMapModule,
  DxHtmlEditorModule,
  DxButtonGroupModule,
  DxValidatorModule,
  DxValidationSummaryModule,
  DxDateBoxModule,
  DxDropDownBoxModule,
  DxTreeViewModule,
  DxDataGridModule,
} from 'devextreme-angular';

import { JwBootstrapSwitchNg2Module } from 'jw-bootstrap-switch-ng2';
import { NgxUiLoaderModule } from 'ngx-ui-loader';

import { FooterComponent } from './footer/footer.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { PictureUploadComponent } from './picture-upload/picture-upload.component';
import { FixedPluginComponent } from './fixed-plugin/fixed-plugin.component';
import { AuthNavbarComponent } from './auth-navbar/auth-navbar.component';
import { CardComponent } from './card/card.component';
import { AutocompleteComponent } from "./autocomplete/autocomplete.component";

import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { CalendarModule } from 'primeng/calendar';
import { SliderModule } from 'primeng/slider';
import { MultiSelectModule } from 'primeng/multiselect';
import { ContextMenuModule } from 'primeng/contextmenu';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { ProgressBarModule } from 'primeng/progressbar';
import { InputTextModule } from 'primeng/inputtext';
import { FileUploadModule } from 'primeng/fileupload';
import { ToolbarModule } from 'primeng/toolbar';
import { RatingModule } from 'primeng/rating';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputNumberModule } from 'primeng/inputnumber';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ChartModule } from 'primeng/chart';
import { BadgeModule } from 'primeng/badge';
import { CarouselModule } from 'primeng/carousel';
import { SkeletonModule } from 'primeng/skeleton';
import { ChipModule } from 'primeng/chip';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { TooltipModule } from 'primeng/tooltip';
import { CascadeSelectModule } from 'primeng/cascadeselect';
import { PasswordModule } from 'primeng/password';
import { DividerModule } from 'primeng/divider';
import { CheckboxModule } from 'primeng/checkbox';
import { TagModule } from 'primeng/tag';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { KeyFilterModule } from 'primeng/keyfilter';
import { InputMaskModule } from 'primeng/inputmask';
import { InputSwitchModule } from 'primeng/inputswitch';
import { ScrollTopModule } from 'primeng/scrolltop';
import { ImageModule } from 'primeng/image';
import { FieldsetModule } from 'primeng/fieldset';
import { TreeSelectModule } from 'primeng/treeselect';
import { PickListModule } from 'primeng/picklist';
import { GalleriaModule } from "primeng/galleria";

const PRIMENG_DEPENDENCIES = [
  TableModule,
  ToastModule,
  CalendarModule,
  SliderModule,
  MultiSelectModule,
  ContextMenuModule,
  DialogModule,
  ButtonModule,
  ProgressBarModule,
  DropdownModule,
  InputTextModule,
  RatingModule,
  FileUploadModule,
  ToolbarModule,
  RadioButtonModule,
  InputNumberModule,
  ConfirmDialogModule,
  InputTextareaModule,
  ProgressSpinnerModule,
  ChartModule,
  BadgeModule,
  CarouselModule,
  SkeletonModule,
  ChipModule,
  ConfirmPopupModule,
  FieldsetModule,
  TooltipModule,
  NgxUiLoaderModule,
  CascadeSelectModule,
  PasswordModule,
  DividerModule,
  CheckboxModule,
  TagModule,
  MessageModule,
  MessagesModule,
  KeyFilterModule,
  InputMaskModule,
  InputSwitchModule,
  ImageModule,
  ScrollTopModule,
  FieldsetModule,
  TreeSelectModule,
  PickListModule,
  GalleriaModule
];

const DEVEXTREME_DEPENDENCIES = [
  DxVectorMapModule,
  DxHtmlEditorModule,
  DxButtonGroupModule,
  DxValidatorModule,
  DxValidationSummaryModule,
  DxDateBoxModule,
  DxDropDownBoxModule,
  DxTreeViewModule,
  DxDataGridModule,
];

const SHARED_COMPONENTS = [
  InputRestrictionDirective,
  FooterComponent,
  NavbarComponent,
  AuthNavbarComponent,
  SidebarComponent,
  PictureUploadComponent,
  FixedPluginComponent,
  CardComponent,
  AutocompleteComponent
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    JwBootstrapSwitchNg2Module,
    CollapseModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    NgbModule,
    SimpleScrollSpyModule,
    /* 3rd party libraries */
    ...PRIMENG_DEPENDENCIES,
    ...DEVEXTREME_DEPENDENCIES,
    /* 3rd party libraries */
  ],
  declarations: [...SHARED_COMPONENTS],

  exports: [
    ...SHARED_COMPONENTS,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    SimpleScrollSpyModule,
    /* 3rd party libraries */
    ...PRIMENG_DEPENDENCIES,
    ...DEVEXTREME_DEPENDENCIES,
    /* 3rd party libraries */
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [{ provide: LOCALE_ID, useValue: 'vi-VN' }],
})
export class ComponentsModule {}
