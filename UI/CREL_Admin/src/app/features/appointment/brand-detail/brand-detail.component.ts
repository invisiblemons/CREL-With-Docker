import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewChildren,
} from "@angular/core";
import {
  FormBuilder,
  FormControlName,
  FormGroup,
  Validators,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import CustomStore from "devextreme/data/custom_store";
import { fromEvent, merge, Observable, Subject } from "rxjs";
import { debounceTime, finalize, switchMap, takeUntil } from "rxjs/operators";
import { ReloadRouteService } from "src/app/shared/services/reload-route.service";
import { GenericValidator } from "src/app/shared/validator/generic-validator";
import { Broker } from "../../broker/broker.model";
import { Industry } from "../../industry/industry.model";
import { IndustryService } from "../../industry/industry.service";
import { Brand } from "../../brand/brand.model";
import { BrandService } from "../../brand/brand.service";
import swal from "sweetalert2";
import { TeamService } from "../../team/team.service";
import { BrokerService } from "../../broker/broker.service";
import { Team } from "../../team/team.model";
import { BrandVerificationService } from "src/app/shared/services/brand-verification.service";
import { Appointment } from "../appointment.model";

@Component({
  selector: "app-brand-detail",
  templateUrl: "./brand-detail.component.html",
  styleUrls: ["./brand-detail.component.scss"],
})
export class BrandDetailComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  fromSuggestion: boolean;

  verifyModal: boolean;

  unverifyModal: boolean;

  reasons: string[];

  /* 
  fields for object
  */
  // Brand
  statuses: { label: string; value: number }[];
  brandId: string;
  brand: Brand;
  industry: Industry;
  industries: Industry[] = [];
  rejectMessage: string = "";

  isChecked: boolean = false;

  @Output() statusBrandDialog = new EventEmitter();

  @Input() appointment: Appointment;

  constructor(
    private brandServices: BrandService,
    private route: ActivatedRoute,
    private router: Router,
    private industryServices: IndustryService,
    private reloadRouteServices: ReloadRouteService,
    private ref: ChangeDetectorRef,
    private fb: FormBuilder,
    private brokerServices: BrokerService,
    private teamServices: TeamService,
    private brandVerificationService: BrandVerificationService
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.statuses = [
      { label: "Đã xoá", value: 0 },
      { label: "Đợi xét duyệt", value: 1 },
      { label: "Được duyệt", value: 2 },
      { label: "Không được duyệt", value: 3 },
    ];
  }

  ngOnInit(): void {
    this.brandServices.getBrandById(this.appointment.property.id)
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((brand) => {
          this.brand = brand;
          return this.industryServices.getIndustries();
        }),
      )
      .subscribe((industries) => {
        this.industries = industries;
        this.brand = this.updateIndustry(this.brand);
        this.isShowSkeleton = false;
        this.isChecked = true;
      });
  }

  ngAfterViewInit(): void {
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  //data functions
  getIndustry(brand: Brand): Industry {
    for (let i = 0; i < this.industries.length; i++) {
      if (brand.industryId === this.industries[i].id) {
        return this.industries[i];
      }
    }
    return;
  }
  updateIndustry(brand: Brand): Brand {
    let industry = this.getIndustry(brand);
    if (industry) {
      brand.industry = industry;
      brand.industryName = industry.name;
    }
    return brand;
  }
  
  hideDialog() {
    this.statusBrandDialog.emit();
  }
}
