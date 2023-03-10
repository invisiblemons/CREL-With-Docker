import { Component, OnInit } from '@angular/core';
import { Subject, switchMap, takeUntil } from 'rxjs';
import { District } from 'src/app/shared/models/district.model';
import { StreetSegment } from 'src/app/shared/models/streetSegment.model';
import { Ward } from 'src/app/shared/models/ward.model';
import { LocationService } from 'src/app/shared/services/location.service';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';
import { Property } from '../property/property.model';
import { PropertyService } from '../property/property.service';
import { WishlistService } from './wishlist.service';
import swal from 'sweetalert2';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.scss'],
})
export class WishlistComponent implements OnInit {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  properties: Property[] = [];
  detailLocation: string;
  street: StreetSegment;
  ward: Ward;
  district: District;
  loading: boolean;
  isEmpty: boolean;

  constructor(
    private propertyServices: PropertyService,
    private locationServices: LocationService,
    private reloadService: ReloadRouteService,
    private wishListService: WishlistService
  ) {
    this.loading = true;
    this.isEmpty = false;
  }

  ngOnInit(): void {
    this.wishListService.getWishlist().subscribe((wishlists) => {
      if (wishlists.length > 0) {
        wishlists.forEach((wishlist, index) => {
          if (wishlist.status !== 1) {
            this.propertyServices
              .getPropertyById(wishlist.propertyId)
              .subscribe((property) => {
                if (property.status === 2) {
                  this.properties = [...this.properties, property];
                }
                if (wishlists.length - 1 === index) {
                  if (this.properties.length > 0) {
                    this.properties.forEach((property, index) => {
                      let wardTemp, streetSegmentTemp, districtTemp;
                      this.locationServices
                        .getWardById(property.location.wardId)
                        .pipe(
                          takeUntil(this.destroySubs$),
                          switchMap((ward: Ward) => {
                            wardTemp = ward;
                            //get street by id
                            return this.locationServices.getStreetSegmentById(
                              property.location.streetSegment.id
                            );
                          }),
                          switchMap((streetSegment: StreetSegment) => {
                            streetSegmentTemp = streetSegment;
                            //get district
                            return this.locationServices.getDistrictById(
                              wardTemp.districtId
                            );
                          })
                        )
                        .subscribe((district: District) => {
                          districtTemp = district;
                          //get address
                          property.location.addressName =
                            property.location.address +
                            ', ' +
                            streetSegmentTemp.name +
                            ', ' +
                            wardTemp.name +
                            ', ' +
                            districtTemp.name;
                          if (this.properties.length - 1 === index) {
                            this.loading = false;
                          }
                        });
                    });
                  } else {
                    this.properties = [];
                    this.loading = false;
                    this.isEmpty = true;
                  }
                }
              });
          }
        });
      } else {
        this.loading = false;
        this.isEmpty = true;
      }
    });
  }

  viewPropertyDetail(property) {
    this.reloadService.routingReload(
      '/mat-bang-cho-thue/chi-tiet',
      property.id
    );
  }

  removeWishList(property) {
    swal
      .fire({
        title: 'B???n c?? ch???c mu???n lo???i b????',
        text: 'B???t ?????ng s???n th????ng m???i n??y s??? b??? lo???i b???!',
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'Kh??ng, gi??? nguy??n',
        confirmButtonText: 'C??, lo???i b??? n??!',
        customClass: {
          cancelButton: 'btn btn-default animation-on-hover',
          confirmButton: 'btn btn-danger animation-on-hover mr-1',
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          this.wishListService
            .deleteWishlist([property.id])
            .subscribe((res) => {
              this.properties = this.properties.filter(
                (propertyItem) => property.id !== propertyItem.id
              );
              if(this.properties.length === 0) {
                this.isEmpty = true;
              }
              swal.fire({
                title: '???? lo???i b???!',
                text: '???? lo???i b??? kh???i danh s??ch y??u th??ch.',
                icon: 'success',
                customClass: {
                  confirmButton: 'btn btn-success animation-on-hover',
                },
                buttonsStyling: false,
                timer: 2000,
              });
            });
        }
      });
  }
}
