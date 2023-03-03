import { ChangeDetectorRef, Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import CustomStore from "devextreme/data/custom_store";
import { Subject } from "rxjs";
import { switchMap, takeUntil } from "rxjs/operators";
import {
  AVATAR_DEFAULT,
  IMAGE_DEFAULT,
} from "src/app/shared/constants/common.const";
import { District } from "src/app/shared/models/district.model";
import { StreetSegment } from "src/app/shared/models/streetSegment.model";
import { Ward } from "src/app/shared/models/ward.model";
import { LocationService } from "src/app/shared/services/location.service";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { Brand } from "../../brand/brand.model";
import { BrandService } from "../../brand/brand.service";
import { Broker } from "../../broker/broker.model";
import { BrokerService } from "../../broker/broker.service";
import { Owner } from "../../owner/owner.model";
import { OwnerService } from "../../owner/owner.service";
import { Project } from "../../project/project.model";
import { ProjectService } from "../../project/project.service";
import { Property } from "../../property/property.model";
import { PropertyService } from "../../property/property.service";
import swal from "sweetalert2";
import { BrandRequestService } from "../brand-request.service";
import { BrandRequest } from "../brand-request.model";

@Component({
  selector: "app-handling-request",
  templateUrl: "./handling-request.component.html",
  styleUrls: ["./handling-request.component.scss"],
})
export class HandlingRequestComponent implements OnInit, OnDestroy {
  AVATAR_DEFAULT = AVATAR_DEFAULT;

  IMAGE_DEFAULT = IMAGE_DEFAULT;
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean = true;

  isShowDialog: boolean;

  //Field of Objects
  propertiesId: number[] = [];

  statuses: { label: string; value: number }[];

  property: Property;

  selectedProperties: Property[] = [];

  isSelectedProperty: boolean;

  properties: Property[] = [];

  propertyGridDataSource: any;

  propertyGridColumns: any = [
    {
      caption: "Tên",
      dataField: "name",
      dataType: "string",
    },
    {
      caption: "Địa chỉ",
      dataField: "addressFull",
      dataType: "string",
    },
    {
      caption: "Nhân viên hỗ trợ",
      dataField: "brokerName",
      dataType: "string",
    },
    {
      caption: "Người sở hữu BĐS",
      dataField: "ownerNames",
      dataType: "string",
    },
  ];

  propertyGridBoxValue: number[];

  isPropertyGridBoxOpened: boolean = false;

  brand: Brand;

  projects: Project[] = [];

  brokers: Broker[] = [];

  owners: Owner[] = [];

  brandRequest: BrandRequest = new BrandRequest();

  constructor(
    private route: ActivatedRoute,
    private reloadRouteServices: ReloadRouteService,
    private propertyServices: PropertyService,
    private brandServices: BrandService,
    private locationServices: LocationService,
    private projectServices: ProjectService,
    private brokerServices: BrokerService,
    private ownerServices: OwnerService,
    private router: Router,
    private brandRequestServices: BrandRequestService,
    private ref: ChangeDetectorRef
  ) {
    this.isShowSkeleton = true;
    this.isShowDialog = true;
    this.isSelectedProperty = false;
  }

  ngOnInit(): void {
    // Get properties
    this.route.queryParams.subscribe((params) => {
      this.brandRequestServices
        .getRequestById(params["id"])
        .pipe(
          takeUntil(this.destroySubs$),
          switchMap((request) => {
            this.brandRequest = request;
            return this.brandServices.getBrandById(this.brandRequest.brandId);
          })
        )
        .subscribe((brand) => {
          this.brand = brand;
        });
    });

    // Get properties
    this.loadData();
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  //data functions
  loadData() {
    this.projectServices
      .getProjects()
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((projects) => {
          this.projects = projects;
          return this.brokerServices.getBrokers();
        }),
        switchMap((brokers) => {
          this.brokers = brokers;
          return this.ownerServices.getOwners();
        }),
        switchMap((owners) => {
          this.owners = this.owners;
          return this.propertyServices.getActiveProperties();
        })
      )
      .subscribe((properties) => {
        properties.forEach((property, index) => {
          let images = [];
          property.media.forEach((image) => {
            if (image.type === 1) {
              images.push(image);
            }
          });
          property.mainImage = images[0];
          //get project name
          if (property.projectId) {
            let project: Project = this.getProject(property);
            if (project) {
              property.project = project;
              property.projectName = project.name;
            }
          }
          //get broker name
          if (property.brokerId) {
            let broker: Broker = this.getBroker(property);
            if (broker) {
              property.broker = broker;
              property.brokerName = broker.name;
            }
          }
          //get owner name
          if (property.owners.length != 0) {
            property.ownerNames = [];
            property.owners.forEach((owner) => {
              property.ownerNames.unshift(owner.name);
            });
          }
          // Get location
          this.locationServices
            .getWardById(property.location.wardId)
            .pipe(
              takeUntil(this.destroySubs$),
              switchMap((ward: Ward) => {
                property.location.ward = ward;
                //get street by id
                return this.locationServices.getStreetSegmentById(
                  property.location.streetSegment.id
                );
              }),
              switchMap((streetSegment: StreetSegment) => {
                property.location.streetSegment = streetSegment;
                //get district
                return this.locationServices.getDistrictById(
                  property.location.ward.districtId
                );
              })
            )
            .subscribe((district: District) => {
              //get address
              property.addressFull =
                property.location.address +
                ", " +
                property.location.streetSegment.name +
                ", " +
                property.location.ward.name +
                ", " +
                district.name;
              if (properties.length - 1 === index) {
                let selectedTemp = [];
                let propertyTemp: any;
                if (this.brandRequest.properties.length > 0) {
                  this.brandRequest.properties.forEach((selectedProperty) => {
                    propertyTemp = properties.filter(
                      (propertyInList) =>
                        propertyInList.id === selectedProperty.id
                    );
                    if (propertyTemp.length > 0) {
                      selectedTemp.push(propertyTemp[0]);
                    }
                    properties = properties.filter(
                      (propertyInList) =>
                        propertyInList.id !== selectedProperty.id
                    );
                  });
                  this.selectedProperties = selectedTemp;
                  this.properties = properties;
                } else {
                  this.properties = properties;
                }

                //gridbox devex
                this.propertyGridDataSource = this.makeAsyncDataSource(
                  this.properties
                );
                this.isShowSkeleton = false;
              }
            });
        });
      });
  }

  //dev-extreme
  makeAsyncDataSource(items) {
    return new CustomStore({
      loadMode: "raw",
      key: "id",
      load() {
        return items;
      },
    });
  }
  propertyGridBox_displayExpr(item) {
    return item && `${item.name} <${item.ownerNames[0]}>`;
  }

  onPropertyGridBoxOptionChanged(e) {
    if (e.name === "value") {
      this.isPropertyGridBoxOpened = false;

      let searchedProperty = this.properties.filter(
        (res) => res.id === e.value[0]
      )[0];

      if (this.selectedProperties) {
        //move selected item to top
        this.properties = this.properties.filter(
          (item) => item.id !== searchedProperty.id
        );
        this.properties.unshift(searchedProperty);

        this.isSelectedProperty = true;
      }
      if (!e.previousValue) {
        this.ref.detectChanges();
      }
      //click searched item
      document.getElementById(searchedProperty.id.toString()).click();
    }
  }

  getProject(property: Property): Project {
    if (this.projects) {
      for (let i = 0; i < this.projects.length; i++) {
        if (property.projectId === this.projects[i].id) {
          return this.projects[i];
        }
      }
    }
    return;
  }
  getOwner(property: Property): Owner[] {
    let owners;
    for (let i = 0; i < this.owners.length; i++) {
      property.owners.forEach((owner) => {
        if (owner.id === this.owners[i].id) {
          owners.unshift(owner);
        }
      });
    }
    return owners;
  }
  getBroker(property: Property): Broker {
    if (this.brokers) {
      for (let i = 0; i < this.brokers.length; i++) {
        if (property.brokerId === this.brokers[i].id) {
          return this.brokers[i];
        }
      }
    }
    return;
  }

  // functions of list
  openDetailProperty(property) {
    this.router.navigate(["/tong-quan/xu-ly-de-xuat/chi-tiet-mat-bang"], {
      queryParams: { id: property.id, fromRequest: true },
    });
  }
  openDetailBrand(brand) {
    this.router.navigate(["/tong-quan/xu-ly-de-xuat/chi-tiet-thuong-hieu"], {
      queryParams: { id: brand.id, fromRequest: true },
    });
  }
  hideDialog() {
    this.reloadRouteServices.routingNotReload("/tong-quan", null);
  }

  suggestProperty() {
    let propertiesId = [];
    this.selectedProperties.forEach((property) => {
      propertiesId.push(property.id);
    });
    this.brandRequestServices
      .updateRequest(this.brandRequest)
      .subscribe((brandRequest) => {
        this.brandRequestServices
          .handlingRequest(this.brandRequest.id, propertiesId)
          .subscribe((request) => {
            swal.fire({
              title: "Thành công!",
              text: "Đã xử lý yêu cầu mặt bằng từ thương hiệu.",
              icon: "success",
              customClass: {
                confirmButton: "btn btn-success animation-on-hover",
              },
              buttonsStyling: false,
              timer: 2000,
            });
            this.reloadRouteServices.routingReload("/tong-quan", null);
          });
      });
  }

  openBrandDetail(brandRequest) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return true;
    };
    this.router.onSameUrlNavigation = "reload";
    this.router.navigate(["/tong-quan/xu-ly-de-xuat/chi-tiet-thuong-hieu"], {
      queryParams: { id: brandRequest.brandId, fromHome: true },
      skipLocationChange: true,
    });
  }
}
