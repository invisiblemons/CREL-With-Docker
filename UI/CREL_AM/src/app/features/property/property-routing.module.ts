import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { BrandDetailComponent } from "../brand/brand-detail/brand-detail.component";
import { OwnerDetailComponent } from "../owner/owner-detail/owner-detail.component";
import { OwnerEditComponent } from "../owner/owner-edit/owner-edit.component";
import { OwnerEditGuard } from "../owner/owner-edit/owner-edit.guard";
import { OwnerListComponent } from "../owner/owner-list/owner-list.component";
import { OwnerNewComponent } from "../owner/owner-new/owner-new.component";
import { OwnerNewGuard } from "../owner/owner-new/owner-new.guard";
import { ProjectDetailComponent } from "../project/project-detail/project-detail.component";
import { ProjectEditComponent } from "../project/project-edit/project-edit.component";
import { ProjectEditGuard } from "../project/project-edit/project-edit.guard";
import { ProjectListComponent } from "../project/project-list/project-list.component";
import { ProjectNewComponent } from "../project/project-new/project-new.component";
import { ProjectNewGuard } from "../project/project-new/project-new.guard";
import { SuggestionComponent } from "../suggestion/suggestion.component";
import { PropertyDetailComponent } from "./property-detail/property-detail.component";
import { PropertyEditComponent } from "./property-edit/property-edit.component";
import { PropertyEditGuard } from "./property-edit/property-edit.guard";
import { PropertyListComponent } from "./property-list/property-list.component";
import { PropertyNewComponent } from "./property-new/property-new.component";
import { PropertyNewGuard } from "./property-new/property-new.guard";
import { PropertyComponent } from "./property.component";

const routes: Routes = [
  {
    path: "",
    component: PropertyComponent,
    data: {
      breadcrumb: "Bất động sản thương mại",
    },
    children: [
      {
        path: "",
        redirectTo: "danh-sach",
        pathMatch: "full",
      },
      {
        path: "danh-sach",
        component: PropertyListComponent,
        children: 
        [
          {
            path: "de-xuat",
            component: SuggestionComponent,
            children: [
              {
                path: "chi-tiet",
                component: BrandDetailComponent
              }
            ]
          },
          {
            path: "chi-tiet",
            component: PropertyDetailComponent,
          },
          {
            path: "tao-moi",
            component: PropertyNewComponent,
            canDeactivate: [PropertyNewGuard],
          },
          {
            path: "chinh-sua",
            component: PropertyEditComponent,
            canDeactivate: [PropertyEditGuard],
          }
        ]
      },
      {
        path: "du-an",
        component: ProjectListComponent,
        data: {
          breadcrumb: "Thông tin dự án",
        },
        children: [
          {
            path: "chi-tiet",
            component: ProjectDetailComponent,
          },
          {
            path: "tao-moi",
            component: ProjectNewComponent,
            canDeactivate: [ProjectNewGuard],
          },
          {
            path: "chinh-sua",
            component: ProjectEditComponent,
            canDeactivate: [ProjectEditGuard],
          }
        ]
      },
      {
        path: "nguoi-so-huu",
        component: OwnerListComponent,
        data: {
          breadcrumb: "Thông tin người sở hữu",
        },children: [
          {
            path: "chi-tiet",
            component: OwnerDetailComponent,
          },
          {
            path: "tao-moi",
            component: OwnerNewComponent,
            canDeactivate: [OwnerNewGuard],
          },
          {
            path: "chinh-sua",
            component: OwnerEditComponent,
            canDeactivate: [OwnerEditGuard],
          }
        ]
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PropertyRoutingModule {}
