import {
  Component,
  OnInit,
  OnDestroy,
  ElementRef,
  ViewChildren,
  AfterViewInit,
  ChangeDetectorRef,
  ViewChild,
  NgZone,
} from '@angular/core';
import { Router } from '@angular/router';
import { fromEvent, merge, Observable, Subject } from 'rxjs';
import { Property } from '../property.model';
import { PropertyService } from '../property.service';
import swal from 'sweetalert2';
import {
  debounceTime,
  switchMap,
  takeUntil,
  filter,
  finalize,
} from 'rxjs/operators';
import 'devextreme/ui/html_editor/converters/markdown';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormControlName,
} from '@angular/forms';
import { GenericValidator } from 'src/app/shared/validator/generic-validator';
import { ProjectService } from '../../project/project.service';
import { Project } from '../../project/project.model';
import { District } from 'src/app/shared/models/district.model';
import { Ward } from 'src/app/shared/models/ward.model';
import { StreetSegment } from 'src/app/shared/models/streetSegment.model';
import { LocationService } from 'src/app/shared/services/location.service';
import { Location } from 'src/app/shared/models/location.model';
import { BrokerService } from '../../broker/broker.service';
import { Broker } from '../../broker/broker.model';
import { Owner } from '../../owner/owner.model';
import { OwnerService } from '../../owner/owner.service';
import CustomStore from 'devextreme/data/custom_store';
import { GeoLocation } from 'src/app/shared/models/types-map/GeoLocation/geolocation-class';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';
import { MapService } from 'src/app/shared/services/map.service';
import { loader } from 'src/app/shared/constants/loader';
import { AutocompleteComponent } from 'src/app/components/autocomplete/autocomplete.component';
import { Media } from 'src/app/shared/models/media.model';
import { TeamService } from '../../team/team.service';
import { LocalStorageService } from '../../login/local-storage.service';
import { Team } from '../../team/team.model';
import { StringValidator } from 'src/app/shared/validator/string-validator';

@Component({
  selector: 'app-property-new',
  templateUrl: './property-new.component.html',
  styleUrls: ['./property-new.component.scss'],
})
export class PropertyNewComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  /* 
  fields for object
  */
  //status
  statuses: { label: string; value: number }[];
  selectedStatus: { label: string; value: number };
  //direction
  directions: { label: string; value: number }[];
  selectedDirection: { label: string; value: number };
  // Certificates
  certificates: { label: string; value: number }[];
  selectedCertificate: { label: string; value: number };

  //property
  property: Property;

  properties: Property[];

  //project
  project: Project;

  projects: Project[] = [];

  //broker
  brokers: Broker[];

  selectedBroker: Broker;

  brokerGridDataSource: any;

  brokerGridBoxValue: number[] = [];

  isBrokerGridBoxOpened: boolean = false;

  brokerGridColumns: any = [
    {
      caption: 'Tên',
      dataField: 'name',
      dataType: 'string',
    },
    {
      caption: 'Số điện thoại',
      dataField: 'phoneNumber',
      dataType: 'string',
    },
  ];

  //owner
  owners: Owner[];

  selectedOwners: Owner[];

  ownerGridDataSource: any;

  ownerGridBoxValue: number[] = [];

  isOwnerGridBoxOpened: boolean = false;

  ownerGridColumns: any = [
    {
      caption: 'Tên',
      dataField: 'name',
      dataType: 'string',
    },
    {
      caption: 'Số điện thoại',
      dataField: 'phoneNumber',
      dataType: 'string',
    },
  ];

  //Location
  location: Location;

  districts: District[] = [];

  selectedDistrict: District;

  wards: Ward[];

  rawWards: Ward[];

  selectedWard: Ward;

  streets: StreetSegment[];

  selectedStreet: StreetSegment;

  address: string;

  detailLocation: string = '';

  //another fields
  isHasManyFrontage: boolean = false;

  isHasProject: boolean = false;

  rentalTermOptions: string[];

  depositTermOptions: string[];

  paymentTermOptions: string[];

  //image
  imgFileArray: File[];
  changedImg: boolean;
  newCertificatesImageFileArray: File[];
  changedNewCertificatesImage: boolean;

  //editor
  editorValueType: string;

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = '';

  propertyForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  // Map Fields
  map: google.maps.Map;
  place: any;
  addressType: string = 'geocode';
  locationSearch: GeoLocation;

  isShowAutocompleteDialog: boolean = false;
  @ViewChild(AutocompleteComponent)
  private autocompleteComponent: AutocompleteComponent;

  // Child elements
  isShowOwnerDialog: boolean = false;
  isShowBrokerDialog: boolean = false;
  isShowProjectDialog: boolean = false;

  images: Media[] = [];
  certificateImages: Media[] = [];

  teams: Team[] = [];

  districtIdList: number[] = [];

  @ViewChild('detailocation') detailocation: any;

  locationTemp: Location;

  isLocationExist: boolean = false;

  constructor(
    private propertyServices: PropertyService,
    private projectServices: ProjectService,
    private brokerServices: BrokerService,
    private ownerServices: OwnerService,
    private router: Router,
    private fb: FormBuilder,
    private locationServices: LocationService,
    private ref: ChangeDetectorRef,
    private reloadServices: ReloadRouteService,
    private mapService: MapService,
    private zone: NgZone,
    private teamServices: TeamService,
    private localStorageService: LocalStorageService
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;

    //direction
    this.directions = [
      { label: 'Bắc', value: 0 },
      { label: 'Đông Bắc', value: 1 },
      { label: 'Đông', value: 2 },
      { label: 'Đông Nam', value: 3 },
      { label: 'Nam', value: 4 },
      { label: 'Tây Nam', value: 5 },
      { label: 'Tây', value: 6 },
      { label: 'Tây Bắc', value: 7 },
    ];
    this.certificates = [
      { label: 'Đang chờ sổ', value: 0 },
      { label: 'Đã có sổ', value: 1 },
      { label: 'Giấy tờ khác', value: 2 },
    ];

    this.selectedCertificate = { label: 'Đang chờ sổ', value: 0 };

    this.statuses = [
      { label: 'Đã xoá', value: 0 },
      { label: 'Đợi cập nhật', value: 1 },
      { label: 'Đợi cho thuê', value: 2 },
      { label: 'Đang được thuê', value: 3 },
      { label: 'Quá hạn thuê', value: 4 },
    ];

    this.rentalTermOptions = [
      'Dài hạn từ 5 năm trở lên',
      '5 năm',
      'Dài hạn từ 5 năm trở xuống',
    ];

    this.depositTermOptions = ['3 tháng', '6 tháng', '1 năm'];

    this.paymentTermOptions = [
      '1 năm tăng giá 10%',
      '1 năm thoả thuận lại giá thuê, tăng tối đa 10%',
    ];

    this.selectedDistrict = null;
    this.selectedWard = null;

    this.property = new Property(null, false);
    this.property.rentalTerm = this.rentalTermOptions[0];
    this.property.depositTerm = this.depositTermOptions[0];
    this.property.paymentTerm = this.paymentTermOptions[0];

    //editor
    this.editorValueType = 'html';

    //image
    this.changedImg = false;
    this.changedNewCertificatesImage = false;

    //validate
    this.validationMessages = {
      name: {
        required: 'Tên BĐS TM không được để trống.',
        minlength: 'Tên BĐS TM phải có ít nhất 3 kí tự.',
        maxlength: 'Tên BĐS TM có nhiều nhất 100 kí tự.',
      },
      price: {
        required: 'Giá cho thuê không được để trống.',
      },
      district: {
        required: 'Quận/Huyện không được để trống.',
        invalid: 'Không tồn tại quận/huyện thuộc những nhóm bạn quản lý.',
      },
      ward: {
        required: 'Phường không được để trống.',
      },
      streetSegment: {
        required: 'Đường không được để trống.',
      },
      address: {
        required: 'Địa chỉ chi tiết không được để trống.',
      },
      owner: {
        required: 'Người sở hữu không được để trống.',
      },
      broker: {
        required: 'Nhân viên môi giới không được để trống.',
        invalid:
          'Không có nhân viên môi giới trong khu vực quận, phường bạn chọn.',
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.propertyForm = this.fb.group({
      district: ['', [Validators.required]],
      ward: ['', [Validators.required]],
      streetSegment: ['', [Validators.required]],
      address: ['', [Validators.required]],
      name: [''],
      rentalTerm: [''],
      depositTerm: [''],
      paymentTerm: [''],
      rentalCondition: [''],
      price: [''],
      project: [{ value: '', disabled: true }],
      certificates: [''],
      direction: [''],
      floorArea: [''],
      frontage: [''],
      horizontal: [''],
      vertical: [''],
      floor: [''],
      roadWidth: [''],
      broker: ['', [Validators.required]],
      owner: ['', [Validators.required]],
      checkProject: [''],
      numberOfFrontage: [''],
      area: [''],
    });

    // Load Data
    this.teamServices
      .getTeams()
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((teams) => {
          if (teams && teams.length > 0) {
            teams.forEach((team) => {
              if (team.areaManagerTeams.length > 0) {
                if (
                  team.areaManagerTeams[team.areaManagerTeams.length - 1]
                    .areaManagerId ===
                  this.localStorageService.getUserObject().id
                ) {
                  this.teams.push(team);
                }
              }
            });
          } else {
            this.teams = [];
          }
          this.rawWards = [];
          teams.forEach((team) => {
            if (team.areaManagerTeams.length > 0) {
              if (
                team.areaManagerTeams[team.areaManagerTeams.length - 1]
                  .areaManagerId === this.localStorageService.getUserObject().id
              ) {
                team.wards.forEach((ward) => {
                  this.rawWards.push(ward);
                  if (!this.districtIdList.includes(ward.districtId)) {
                    this.districtIdList.push(ward.districtId);
                  }
                });
              }
            }
          });
          return this.locationServices.getDistricts();
        }),
        switchMap((districts) => {
          if (this.districtIdList.length === 0) {
            this.propertyForm.controls['district'].clearValidators();
            this.propertyForm.controls['district'].setValidators([
              Validators.required,
              StringValidator.invalid(),
            ]);
            this.propertyForm.controls['district'].setErrors({ invalid: true });
            this.propertyForm.controls['district'].updateValueAndValidity();
          } else {
            districts.forEach((district) => {
              if (this.districtIdList.includes(district.id)) {
                this.districts.push(district);
              }
            });
          }
          return this.projectServices.getProjects();
        }),
        switchMap((projects) => {
          let tempProjects = [];
          projects.forEach((project) => {
            if (this.districtIdList.includes(project.districtId)) {
              tempProjects.push(project);
            }
          });
          this.projects = tempProjects;
          return this.ownerServices.getOwners();
        }),
        switchMap((owners) => {
          this.owners = owners;
          //gridbox devex
          this.ownerGridDataSource = this.makeAsyncDataSource(this.owners);
          return this.brokerServices.getBrokers();
        })
      )
      .subscribe((brokers) => {
        let brokersTemp = [];
        brokers.forEach((broker) => {
          let team: Team = this.getTeam(broker);
          if (team) {
            brokersTemp.push(broker);
            broker.team = team;
            broker.teamName = team.name;
          }
        });
        this.brokers = brokersTemp;
        this.isShowSkeleton = false;
      });
  }

  ngAfterViewInit(): void {
    loader.load().then(() => {
      this.map = new google.maps.Map(
        document.getElementById('map') as HTMLElement,
        {
          center: {
            lat: 10.783645286871034,
            lng: 106.6956950392398,
          },
          zoom: 14,
        }
      );
    });

    // Watch for the blur event from any input element on the form.
    // This is required because the valueChanges does not provide notification on blur
    const controlBlurs: Observable<any>[] = this.formInputElements.map(
      (formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur')
    );

    // Merge the blur event observable with the valueChanges observable
    // so we only need to subscribe once.
    merge(this.propertyForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.propertyForm
        );
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadServices.routingReload('/bat-dong-san/danh-sach', null);
  }

  cancelDialog() {
    this.reloadServices.routingNotReload('/bat-dong-san/danh-sach', null);
  }

  changeAutocompleteStatus() {
    this.isShowAutocompleteDialog = true;
  }

  getAutocompleteState(value) {
    if (value) {
      this.isShowAutocompleteDialog = false;
    }
  }

  autocomplete() {
    const ac = new google.maps.places.Autocomplete(
      this.autocompleteComponent.addressText.nativeElement,
      {
        componentRestrictions: { country: 'VN' },
        types: [this.addressType], // 'establishment' / 'address' / 'geocode'
      }
    );
    google.maps.event.addListener(ac, 'place_changed', () => {
      // excute find property on that place
      this.place = ac.getPlace();
    });
  }

  openNewOwnerModal() {
    this.isShowOwnerDialog = true;
  }
  getOwnerStatusDialog(value) {
    this.isShowOwnerDialog = false;
    if (value.id) {
      this.ownerServices.getOwnerById(value.id).subscribe((owner) => {
        this.owners.unshift(owner);
        this.ownerGridDataSource = this.makeAsyncDataSource(null);
        this.ownerGridDataSource = this.makeAsyncDataSource(this.owners);
        this.ownerGridBoxValue = [...this.ownerGridBoxValue, value.id];
        this.propertyForm.controls.owner.setValue(this.ownerGridBoxValue);
      });
    }
  }
  openNewBrokerModal() {
    this.isShowBrokerDialog = true;
  }
  getBrokerStatusDialog(value) {
    this.isShowBrokerDialog = false;
    if (value.id) {
      this.brokerServices.getBrokerById(value.id).subscribe((broker) => {
        this.brokers.unshift(broker);
        this.brokerGridDataSource = this.makeAsyncDataSource(null);
        this.brokerGridDataSource = this.makeAsyncDataSource(this.brokers);
        this.brokerGridBoxValue = [value.id];
        this.propertyForm.controls.broker.setValue(this.brokerGridBoxValue);
      });
    }
  }
  openNewProjectModal() {
    this.isShowProjectDialog = true;
  }
  getProjectStatusDialog(value) {
    this.isShowProjectDialog = false;
    if (value.id) {
      this.projectServices.getProjectById(value.id).subscribe((project) => {
        this.projects.unshift(project);
        this.property.project = project;
        this.propertyForm.controls.project.setValue(project);
      });
    }
  }

  //dev-extreme
  makeAsyncDataSource(items) {
    return new CustomStore({
      loadMode: 'raw',
      key: 'id',
      load() {
        return items;
      },
    });
  }

  brokerGridBox_displayExpr(item) {
    return item && `${item.name} <${item.phoneNumber}>`;
  }

  onBrokerGridBoxOptionChanged(e) {
    if (e.name === 'value' && e.value) {
      this.selectedBroker = this.brokers.filter(
        (res) => res.id === e.value[0]
      )[0];
      this.isBrokerGridBoxOpened = false;
      if (!e.previousValue) {
        this.propertyForm.get('broker').clearValidators();
        this.propertyForm.get('broker').setValidators([Validators.required]);
        this.propertyForm.controls['broker'].updateValueAndValidity();
        this.ref.detectChanges();
      }
    }
  }
  ownerGridBox_displayExpr(item) {
    return item && `${item.name} <${item.phoneNumber}>`;
  }

  onOwnerGridBoxOptionChanged(e) {
    if (e.name === 'value' && e.value) {
      this.selectedOwners = [];

      this.isOwnerGridBoxOpened = false;
      this.owners.forEach((owner) => {
        e.value.forEach((id) => {
          owner.id === id ? this.selectedOwners.push(owner) : '';
        });
      });
      if (!e.previousValue) {
        this.ref.detectChanges();
      }
    }
  }

  //data functions
  getTeam(broker: Broker): Team {
    if (this.teams && this.teams.length > 0) {
      for (let i = 0; i < this.teams.length; i++) {
        if (broker.teamId === this.teams[i].id) {
          return this.teams[i];
        }
      }
    } else return null;
  }
  getProject(property: Property): Project {
    for (let i = 0; i < this.projects.length; i++) {
      if (property.projectId === this.projects[i].id) {
        return this.projects[i];
      }
    }
    return;
  }

  getBroker(property: Property): Broker {
    for (let i = 0; i < this.brokers.length; i++) {
      if (property.brokerId === this.brokers[i].id) {
        return this.brokers[i];
      }
    }
    return;
  }

  getOwner(property: Property): Owner {
    for (let i = 0; i < this.owners.length; i++) {
      if (property.owners[0].id === this.owners[i].id) {
        return this.owners[i];
      }
    }
    return;
  }

  // Image Function
  getImgArrayFromChild(imgArrayFile) {
    this.imgFileArray = imgArrayFile;
    this.changedImg = true;
  }
  getCertificatesImgArrayFromChild(imgArrayFile) {
    this.newCertificatesImageFileArray = imgArrayFile;
    this.changedNewCertificatesImage = true;
  }

  checkingFrontage() {
    this.ref.detectChanges();
    this.propertyForm.controls.streetSegment.setValue([]);
  }

  changeDirection(ev) {
    this.selectedDirection = ev.value;
  }
  getDataLocation(ev) {
    this.locationServices
      .getWards(this.selectedDistrict.id)
      .subscribe((res) => {
        this.wards = res;
      });
    this.locationServices
      .getStreetSegment(this.selectedDistrict.id)
      .subscribe((res) => {
        this.streets = res;
      });
  }

  saveProperty() {
    this.loading = true;
    if (this.isLocationExist) {
      //prepare fields to update property
      this.property.locationId = this.locationTemp.id;
      this.property.createDate = new Date();
      this.property.lastUpdateDate = new Date();
      if (this.isHasProject && this.property.project) {
        this.property.projectId = this.property.project.id;
      }
      //broker
      if (this.selectedBroker) {
        this.property.brokerId = this.selectedBroker.id;
      }
      if (this.selectedDirection) {
        this.property.direction = this.selectedDirection.value;
      }
      this.property.status = 1;
      if (this.selectedCertificate) {
        this.property.certificates = this.selectedCertificate.value;
      } else {
        this.property.certificates = 0;
      }
      this.property = new Property(this.property, true);
      this.propertyServices
        .insertProperty(this.property)
        .pipe(
          takeUntil(this.destroySubs$),
          switchMap((property) => {
            swal.fire({
              title: 'Thành công!',
              text: 'Tạo mới bất động sản thương mại thành công.',
              icon: 'success',
              customClass: {
                confirmButton: 'btn btn-success animation-on-hover',
              },
              buttonsStyling: false,
              timer: 2000,
            });
            this.propertyForm.reset();
            this.hideDialog();

            // Add Owner
            let ownerIds = [];
            this.selectedOwners.forEach((owner) => {
              ownerIds.push(owner.id);
            });
            return this.propertyServices.insertOwnersForProperty(
              property.id,
              ownerIds
            );
          }),
          switchMap((property) => {
            if (this.changedImg && this.imgFileArray.length !== 0) {
              let formData: FormData = new FormData();
              this.imgFileArray.forEach((img) => {
                formData.append('files', img);
              });
              return this.propertyServices.insertMediaForProperty(
                property.id,
                formData
              );
            }
            let propertyForUpdate = new Property(property, false);
            return this.propertyServices.updateProperty(propertyForUpdate);
          }),
          switchMap((property) => {
            if (
              this.changedNewCertificatesImage &&
              this.newCertificatesImageFileArray.length !== 0
            ) {
              let formData: FormData = new FormData();
              this.newCertificatesImageFileArray.forEach((img) => {
                formData.append('files', img);
              });
              return this.propertyServices.insertCertificationForProperty(
                property.id,
                formData
              );
            }
            let propertyForUpdate = new Property(property, false);
            return this.propertyServices.updateProperty(propertyForUpdate);
          }),
          finalize(() => (this.loading = false))
        )
        .subscribe(
          (property) => {},
          (error) => {
            swal.fire({
              title: 'Thất bại!',
              text: 'Tạo mới bất động sản thương mại thất bại kiểm tra lại dữ liệu nhập vào. ',
              icon: 'error',
              customClass: {
                confirmButton: 'btn btn-info animation-on-hover',
              },
              buttonsStyling: false,
              timer: 2000,
            });
            this.loading = false;
          }
        );
    } else {
      //prepare fields to create location
      let fullAddress =
        this.detailLocation +
        ' ' +
        this.selectedStreet.name +
        ', ' +
        this.selectedWard.name +
        ', ' +
        this.selectedDistrict.name +
        ', Hồ Chí Minh, Vietnam';
      let request = {
        query: fullAddress,
        fields: ['geometry'],
      };
      let service = new google.maps.places.PlacesService(this.map);
      service.findPlaceFromQuery(request, (results, status) =>
        this.zone.run(() => {
          if (status === google.maps.places.PlacesServiceStatus.OK) {
            let lat = results[0].geometry.location.lat();
            let lng = results[0].geometry.location.lng();
            this.location = new Location(null, false);
            this.location.latitude = lat;
            this.location.longitude = lng;
            this.location.streetSegmentId = this.selectedStreet.id;
            this.location.address = this.detailLocation;
            this.location.wardId = this.selectedWard.id;
            this.location.status = 1;
            this.location = new Location(this.location, true);
            this.locationServices
              .insertLocation(this.location)
              .pipe(
                takeUntil(this.destroySubs$),
                switchMap((location: Location) => {
                  //prepare fields to update property
                  this.property.locationId = location.id;
                  this.property.createDate = new Date();
                  this.property.lastUpdateDate = new Date();
                  if (this.isHasProject && this.property.project) {
                    this.property.projectId = this.property.project.id;
                  }
                  //broker
                  if (this.selectedBroker) {
                    this.property.brokerId = this.selectedBroker.id;
                  }
                  if (this.selectedDirection) {
                    this.property.direction = this.selectedDirection.value;
                  }
                  this.property.status = 1;
                  if (this.selectedCertificate) {
                    this.property.certificates = this.selectedCertificate.value;
                  } else {
                    this.property.certificates = 0;
                  }
                  this.property.price = parseInt(
                    (this.property.price * 1000000).toFixed(0)
                  );
                  this.property = new Property(this.property, true);
                  return this.propertyServices.insertProperty(this.property);
                }),
                switchMap((property) => {
                  swal.fire({
                    title: 'Thành công!',
                    text: 'Tạo mới bất động sản thương mại thành công.',
                    icon: 'success',
                    customClass: {
                      confirmButton: 'btn btn-success animation-on-hover',
                    },
                    buttonsStyling: false,
                    timer: 2000,
                  });
                  this.propertyForm.reset();
                  this.hideDialog();

                  // Add Owner
                  let ownerIds = [];
                  this.selectedOwners.forEach((owner) => {
                    ownerIds.push(owner.id);
                  });
                  return this.propertyServices.insertOwnersForProperty(
                    property.id,
                    ownerIds
                  );
                }),
                switchMap((property) => {
                  if (this.changedImg && this.imgFileArray.length !== 0) {
                    let formData: FormData = new FormData();
                    this.imgFileArray.forEach((img) => {
                      formData.append('files', img);
                    });
                    return this.propertyServices.insertMediaForProperty(
                      property.id,
                      formData
                    );
                  }
                  let propertyForUpdate = new Property(property, false);
                  return this.propertyServices.updateProperty(
                    propertyForUpdate
                  );
                }),
                switchMap((property) => {
                  if (
                    this.changedNewCertificatesImage &&
                    this.newCertificatesImageFileArray.length !== 0
                  ) {
                    let formData: FormData = new FormData();
                    this.newCertificatesImageFileArray.forEach((img) => {
                      formData.append('files', img);
                    });
                    return this.propertyServices.insertCertificationForProperty(
                      property.id,
                      formData
                    );
                  }
                  let propertyForUpdate = new Property(property, false);
                  return this.propertyServices.updateProperty(
                    propertyForUpdate
                  );
                }),
                finalize(() => (this.loading = false))
              )
              .subscribe(
                (property) => {},
                (error) => {
                  swal.fire({
                    title: 'Thất bại!',
                    text: 'Tạo mới bất động sản thương mại thất bại kiểm tra lại dữ liệu nhập vào. ',
                    icon: 'error',
                    customClass: {
                      confirmButton: 'btn btn-info animation-on-hover',
                    },
                    buttonsStyling: false,
                    timer: 2000,
                  });
                  this.loading = false;
                }
              );
          } else {
            this.location = new Location(null, false);
            this.location.streetSegmentId = this.selectedStreet.id;
            this.location.address = this.detailLocation;
            this.location.wardId = this.selectedWard.id;
            this.location.status = 1;
            this.location = new Location(this.location, true);
            this.locationServices
              .insertLocation(this.location)
              .pipe(
                takeUntil(this.destroySubs$),
                switchMap((location: Location) => {
                  //prepare fields to update property
                  this.property.locationId = location.id;
                  this.property.createDate = new Date();
                  this.property.lastUpdateDate = new Date();
                  if (this.isHasProject && this.property.project) {
                    this.property.projectId = this.property.project.id;
                  }
                  //broker
                  if (this.selectedBroker) {
                    this.property.brokerId = this.selectedBroker.id;
                  }
                  if (this.selectedDirection) {
                    this.property.direction = this.selectedDirection.value;
                  }
                  this.property.status = 1;
                  if (this.selectedCertificate) {
                    this.property.certificates = this.selectedCertificate.value;
                  } else {
                    this.property.certificates = 0;
                  }
                  this.property.price = parseInt(
                    (this.property.price * 1000000).toFixed(0)
                  );
                  this.property = new Property(this.property, true);
                  return this.propertyServices.insertProperty(this.property);
                }),
                switchMap((property) => {
                  swal.fire({
                    title: 'Thành công!',
                    text: 'Tạo mới bất động sản thương mại thành công.',
                    icon: 'success',
                    customClass: {
                      confirmButton: 'btn btn-success animation-on-hover',
                    },
                    buttonsStyling: false,
                    timer: 2000,
                  });
                  this.propertyForm.reset();
                  this.hideDialog();

                  // Add Owner
                  let ownerIds = [];
                  this.selectedOwners.forEach((owner) => {
                    ownerIds.push(owner.id);
                  });
                  return this.propertyServices.insertOwnersForProperty(
                    property.id,
                    ownerIds
                  );
                }),
                switchMap((property) => {
                  if (this.changedImg && this.imgFileArray.length !== 0) {
                    let formData: FormData = new FormData();
                    this.imgFileArray.forEach((img) => {
                      formData.append('files', img);
                    });
                    return this.propertyServices.insertMediaForProperty(
                      property.id,
                      formData
                    );
                  }
                  let propertyForUpdate = new Property(property, false);
                  return this.propertyServices.updateProperty(
                    propertyForUpdate
                  );
                }),
                switchMap((property) => {
                  if (
                    this.changedNewCertificatesImage &&
                    this.newCertificatesImageFileArray.length !== 0
                  ) {
                    let formData: FormData = new FormData();
                    this.newCertificatesImageFileArray.forEach((img) => {
                      formData.append('files', img);
                    });
                    return this.propertyServices.insertCertificationForProperty(
                      property.id,
                      formData
                    );
                  }
                  let propertyForUpdate = new Property(property, false);
                  return this.propertyServices.updateProperty(
                    propertyForUpdate
                  );
                }),
                finalize(() => (this.loading = false))
              )
              .subscribe(
                (property) => {},
                (error) => {
                  swal.fire({
                    title: 'Thất bại!',
                    text: 'Tạo mới bất động sản thương mại thất bại kiểm tra lại dữ liệu nhập vào. ',
                    icon: 'error',
                    customClass: {
                      confirmButton: 'btn btn-info animation-on-hover',
                    },
                    buttonsStyling: false,
                    timer: 2000,
                  });
                  this.loading = false;
                }
              );
          }
        })
      );
    }
  }

  resetLocation(ev) {
    this.selectedDistrict = ev.value;
    this.propertyForm.controls.ward.setValue('');
    this.propertyForm.controls.streetSegment.setValue('');
  }

  onChangeWard(ev) {
    let brokersInTeam = [];
    this.brokers.forEach((broker) => {
      if (broker.teamId === ev.value.teamId) {
        brokersInTeam.push(broker);
      }
    });
    if (brokersInTeam.length === 0) {
      this.propertyForm
        .get('broker')
        .setValidators([Validators.required, StringValidator.invalid()]);
      this.propertyForm.get('broker').setErrors({ invalid: true });
      this.propertyForm.get('broker').updateValueAndValidity();
    }
    this.brokerGridDataSource = this.makeAsyncDataSource(brokersInTeam);
  }

  loadWards(ev) {
    let wardsTemp = [];
    this.locationServices
      .getWards(this.selectedDistrict.id)
      .subscribe((res) => {
        res.forEach((ward) => {
          this.rawWards.forEach((wardOfAM) => {
            if (ward.id === wardOfAM.id) {
              wardsTemp.push(ward);
            }
          });
        });
        this.wards = wardsTemp;
      });
  }

  loadStreets(ev) {
    this.locationServices
      .getStreetSegment(this.selectedDistrict.id)
      .subscribe((res) => {
        this.streets = res;
      });
  }

  changeCertificates(ev) {
    this.selectedCertificate = ev.value;
  }

  onChangeStateProject(event) {
    if (event.checked) {
      this.propertyForm.get('project').enable();
    } else {
      this.propertyForm.get('project').disable();
    }
  }

  changeProject(ev) {
    this.property.project = ev.value;
  }

  onKeyAddress(event) {
    this.detailLocation = (event.target as HTMLInputElement).value;
    if (this.selectedDistrict && this.selectedWard && this.selectedStreet) {
      let fullAddress =
        this.detailLocation +
        ' ' +
        this.selectedStreet.name +
        ', ' +
        this.selectedWard.name +
        ', ' +
        this.selectedDistrict.name +
        ', Hồ Chí Minh, Vietnam';
      let request = {
        query: fullAddress,
        fields: ['geometry'],
      };
      let service = new google.maps.places.PlacesService(this.map);
      service.findPlaceFromQuery(request, (results, status) =>
        this.zone.run(() => {
          if (status === google.maps.places.PlacesServiceStatus.OK) {
            let lat = results[0].geometry.location.lat();
            let lng = results[0].geometry.location.lng();
            this.locationServices
              .getLocationByLatLng(lat, lng)
              .subscribe((locations) => {
                if (locations.length > 0) {
                  this.isLocationExist = true;
                  this.locationTemp = locations[0];
                  this.isShowAutocompleteDialog = true;
                }
              });
          }
        })
      );
    }
  }

  getApplyProperty(event) {
    this.isShowAutocompleteDialog = false;
    this.isLocationExist = true;
    if (event !== null) {
      this.property = event;
      this.certificates.forEach((certificate) => {
        certificate.value === this.property.certificates
          ? (this.selectedCertificate = certificate)
          : '';
      });

      this.property.media.forEach((image) => {
        if (image.type === 1) {
          this.images.push(image);
        } else if (image.type === 2) {
          this.certificateImages.push(image);
        }
      });

      // handle price
      this.property.price = parseFloat(
        (Math.round(this.property.price * 100) / 100000000).toFixed(2)
      );

      // get direction
      this.directions.forEach((direction) => {
        direction.value === this.property.direction
          ? (this.selectedDirection = direction)
          : '';
      });

      // getProject
      if (this.property.projectId) {
        this.isHasProject = true;
        this.propertyForm.get('project').enable();
        this.property.project = this.getProject(this.property);
      }
      // get owner
      if (this.property.owners.length != 0) {
        this.selectedOwners = this.property.owners;
      }
      this.selectedOwners.forEach((owner) => {
        this.ownerGridBoxValue = [...this.ownerGridBoxValue, owner.id];
      });
      this.propertyForm.controls.owner.setValue(this.ownerGridBoxValue);
    }
  }
}
