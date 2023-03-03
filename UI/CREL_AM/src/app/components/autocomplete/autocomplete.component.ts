import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { Subject } from 'rxjs';
import { switchMap, takeUntil } from 'rxjs/operators';
import { Property } from 'src/app/features/property/property.model';
import { PropertyService } from 'src/app/features/property/property.service';
import { District } from 'src/app/shared/models/district.model';
import { StreetSegment } from 'src/app/shared/models/streetSegment.model';
import { Ward } from 'src/app/shared/models/ward.model';
import { LocationService } from 'src/app/shared/services/location.service';
import { MapService } from 'src/app/shared/services/map.service';
import { categories } from '../../shared/constants/constants';
interface Country {
  name: string;
  code: string;
}
@Component({
  selector: 'app-autocomplete',
  templateUrl: './autocomplete.component.html',
  styleUrls: ['./autocomplete.component.scss'],
})
export class AutocompleteComponent implements OnInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();
  isShowDialog: boolean = true;

  formatted_address: string;
  isAddressManuallyTyped: boolean;
  categories = categories;
  selectedCategories: any;
  selectedNameCategories: string[];

  autocompleteInput: string = '';
  queryWait: boolean;
  @ViewChild('addressText') addressText: any;
  @Output() autocomplete = new EventEmitter();
  @Output() isCloseDialog = new EventEmitter();
  @Output() propertyApply = new EventEmitter();
  filterValue = '';
  @Input() placeRes;
  @Input() location;

  district: string;
  ward: string;
  street: string;
  streetNumber: string;

  isShowExist: boolean = false;

  properties: Property[] = [];

  constructor(
    private ref: ChangeDetectorRef,
    private propertyService: PropertyService,
    private locationService: LocationService,
    private mapService: MapService
  ) {
    this.isAddressManuallyTyped = false;

    this.selectedCategories = [];
    this.selectedNameCategories = [];
  }

  ngOnInit(): void {}

  ngOnChanges(changes: SimpleChanges) {
    if (changes['placeRes']&&changes['placeRes'].currentValue) {
      this.district = this.mapService.getDistrict(this.placeRes);
      this.ward = this.mapService.getWard(this.placeRes);
      this.street = this.mapService.getStreet(this.placeRes);
      this.streetNumber = this.mapService.getStreetNumber(this.placeRes);
    }
    if (changes['location']&&changes['location'].currentValue) {
      this.location = changes['location'].currentValue;
      this.district = this.location.ward.district.name;
      this.ward = this.location.ward.name;
      this.street = this.location.streetSegment.name;
      this.streetNumber = this.location.address;
      this.propertyService
        .getPropertiesByLocation(changes['location'].currentValue.id)
        .subscribe((properties) => {
          if (properties.length > 0) {
            properties.forEach((property, index) => {
              let images = [];
              property.media.forEach((image) => {
                if (image.type === 1) {
                  images.push(image);
                }
              });
              property.mainImage = images[0];

              if (properties.length - 1 === index) {
                this.isShowExist = true;
                this.properties = properties;
                console.log(properties);
              }
            });
          }
        });
    }
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  format(value) {
    return `${value}`;
  }
  onKeyAddress($event: Event): void {
    this.autocompleteInput = ($event.target as HTMLInputElement).value;
    this.autocomplete.emit();
    this.isAddressManuallyTyped = true;
  }

  hideDialog() {
    this.isCloseDialog.emit(true);
  }

  createNew() {
    this.propertyApply.emit(null);
    this.isCloseDialog.emit(true);
  }

  applyProperty(property) {
    this.propertyApply.emit(property);
  }
}
