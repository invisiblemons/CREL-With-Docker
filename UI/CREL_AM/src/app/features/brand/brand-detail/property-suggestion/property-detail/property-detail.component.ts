import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { Subject } from 'rxjs';
import { finalize, switchMap, takeUntil } from 'rxjs/operators';
import { Project } from 'src/app/features/project/project.model';
import { ProjectService } from 'src/app/features/project/project.service';
import { Property } from 'src/app/features/property/property.model';
import { PropertyService } from 'src/app/features/property/property.service';
import { District } from 'src/app/shared/models/district.model';
import { Media } from 'src/app/shared/models/media.model';
import { StreetSegment } from 'src/app/shared/models/streetSegment.model';
import { Ward } from 'src/app/shared/models/ward.model';
import { LocationService } from 'src/app/shared/services/location.service';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';

@Component({
  selector: 'app-property-detail',
  templateUrl: './property-detail.component.html',
  styleUrls: ['./property-detail.component.scss'],
})
export class PropertyDetailComponent implements OnInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();
  isShowSkeleton: boolean;
  isShowDialog: boolean;
  items: MenuItem[];
  activeItem: MenuItem;
  currentMenuId = 'detail-body-1';

  /* 
  Fields of object
  */
  statuses: { label: string; value: number }[];
  property: Property;
  project: Project;
  detailLocation: string;
  street: StreetSegment;
  ward: Ward;
  district: District;
  projects: Project[];
  // direction
  directions: { label: string; value: number }[];
  selectedDirection: { label: string; value: number };

  images: Media[] = [];
  certificateImages: Media[] = [];

  displayCustom: boolean;
  activeIndex: number = 0;

  fromBrandDetail: number;

  constructor(
    private propertyServices: PropertyService,
    private route: ActivatedRoute,
    private router: Router,
    private projectServices: ProjectService,
    private locationServices: LocationService,
    private reloadServices: ReloadRouteService
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.statuses = [
      { label: '???? xo??', value: 0 },
      { label: '?????i c???p nh???t', value: 1 },
      { label: '?????i cho thu??', value: 2 },
      { label: '??ang ???????c thu??', value: 3 },
      { label: 'Qu?? h???n thu??', value: 4 },
    ];
    //direction
    this.directions = [
      { label: 'B???c', value: 0 },
      { label: '????ng B???c', value: 1 },
      { label: '????ng', value: 2 },
      { label: '????ng Nam', value: 3 },
      { label: 'Nam', value: 4 },
      { label: 'T??y Nam', value: 5 },
      { label: 'T??y', value: 6 },
      { label: 'T??y B???c', value: 7 },
    ];
  }

  ngOnInit(): void {
    this.route.queryParams
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((params) => {
          if (params.fromBrandDetail) {
            this.fromBrandDetail = params.fromBrandDetail;
          }
          return this.propertyServices.getPropertyById(params['id']);
        }),
        switchMap((property) => {
          this.locationServices
            .getWardById(property.location.wardId)
            .pipe(
              takeUntil(this.destroySubs$),
              switchMap((ward: Ward) => {
                this.ward = ward;
                //get street by id
                return this.locationServices.getStreetSegmentById(
                  property.location.streetSegment.id
                );
              }),
              switchMap((streetSegment: StreetSegment) => {
                this.street = streetSegment;
                //get district
                return this.locationServices.getDistrictById(
                  this.ward.districtId
                );
              })
            )
            .subscribe((district: District) => {
              this.district = district;
              //get address
              this.property.location.addressName =
                property.location.address +
                ', ' +
                this.street.name +
                ', ' +
                this.ward.name +
                ', ' +
                this.district.name;
            });
          this.property = property;

          this.property.media.forEach((image) => {
            if (image.type === 1) {
              this.images.push(image);
            } else if (image.type === 2) {
              this.certificateImages.push(image);
            }
          });

          return this.projectServices.getProjects();
        })
      )
      .subscribe((projects) => {
        this.projects = projects;
        this.property = this.updateProject(this.property);
        this.directions.forEach((direction) => {
          direction.value === this.property.direction
            ? (this.selectedDirection = direction)
            : '';
        });
        this.isShowSkeleton = false;
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    if (this.fromBrandDetail) {
      this.reloadServices.routingNotReload(
        '/thuong-hieu/danh-sach/chi-tiet',
        this.fromBrandDetail
      );
    } else
      this.reloadServices.routingNotReload('/bat-dong-san/danh-sach', null);
  }

  //data functions
  getProject(property: Property): Project {
    for (let i = 0; i < this.projects.length; i++) {
      if (property.projectId === this.projects[i].id) {
        return this.projects[i];
      }
    }
    return;
  }
  updateProject(property: Property): Property {
    let project = this.getProject(property);
    if (project) {
      property.project = project;
    }
    return property;
  }

  imageClick(index: number) {
    this.activeIndex = index;
    this.displayCustom = true;
  }
}
