import {
  Component,
  OnInit,
  OnDestroy,
  ViewChildren,
  ElementRef,
  AfterViewInit,
} from '@angular/core';
import { fromEvent, merge, Observable, Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { Project } from '../project.model';
import { ProjectService } from '../project.service';
import { debounceTime, finalize, switchMap, takeUntil } from 'rxjs/operators';
import swal from 'sweetalert2';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormControlName,
} from '@angular/forms';
import { GenericValidator } from 'src/app/shared/validator/generic-validator';
import 'devextreme/ui/html_editor/converters/markdown';
import { LocationService } from 'src/app/shared/services/location.service';
import { District } from 'src/app/shared/models/district.model';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';
import moment from 'moment';
import { Team } from '../../team/team.model';
import { Ward } from 'src/app/shared/models/ward.model';
import { LocalStorageService } from '../../login/local-storage.service';
import { TeamService } from '../../team/team.service';

@Component({
  selector: 'app-project-edit',
  templateUrl: './project-edit.component.html',
  styleUrls: ['./project-edit.component.scss'],
})
export class ProjectEditComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  reasons: string[];

  statuses: { label: string; value: number }[];

  selectedStatus: { label: string; value: number };

  //project
  project: Project;

  selectedProject: Project;

  //location
  districts: District[];

  selectedDistrict: District;

  //editor
  editorValueType: string;

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  projectForm!: FormGroup;

  errorMessage = '';

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  teams: Team[] = [];

  rawWards: Ward[] = [];

  districtIdList: number[] = [];

  constructor(
    private projectServices: ProjectService,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private locationService: LocationService,
    private reloadService: ReloadRouteService,
    private teamServices: TeamService,
    private localStorageService: LocalStorageService
  ) {
    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;
    this.statuses = [
      { label: '???? xo??', value: 0 },
      { label: 'Ho???t ?????ng', value: 1 },
    ];

    this.reasons = [
      'L???i n???i dung sai s??? th???t',
      'L???i h??nh ???nh kh??ng li??n quan',
      'L???i d??ng t??? ng??? th?? t???c',
      'L???i n???i dung ch???a th??ng tin ph??n bi???t ch???ng t???c, v??ng mi???n',
    ];

    //editor
    this.editorValueType = 'html';

    //validate
    this.validationMessages = {
      name: {
        required: 'T??n kh??ng ???????c ????? tr???ng.',
        minlength: 'T??n ph???i c?? ??t nh???t 3 k?? t???.',
        maxlength: 'T??n c?? nhi???u nh???t 100 k?? t???.',
      },
      company: {
        required: 'T??n ch??? ?????u t?? kh??ng ???????c ????? tr???ng.',
      },
      handoverYear: {
        required: 'N??m b??n giao kh??ng ???????c ????? tr???ng.',
      },
      district: {
        required: 'Qu???n/Huy???n kh??ng ???????c ????? tr???ng.',
      },
      status: {
        required: 'Tr???ng th??i kh??ng ???????c ????? tr???ng.',
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.projectForm = this.fb.group({
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ],
      ],
      company: ['', [Validators.required]],
      handoverYear: ['', [Validators.required]],
      district: ['', [Validators.required]],
      status: ['', [Validators.required]],
    });

    // Load Data
    this.locationService
      .getDistricts()
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((districts) => {
          this.districts = districts;
          return this.route.queryParams;
        }),
        switchMap((params) => {
          return this.projectServices.getProjectById(params['id']);
        })
      )
      .subscribe((project) => {
        this.project = project;
        this.statuses.forEach((status) => {
          status.value === this.project.status
            ? (this.selectedStatus = status)
            : '';
        });
        this.districts.forEach((district) => {
          if (district.id === this.project.districtId) {
            this.selectedDistrict = district;
          }
        });

        this.project.handoverYear = new Date(this.project.handoverYear);
        this.projectForm.controls.handoverYear.setValue(
          this.project.handoverYear
        );
        this.isShowSkeleton = false;
      });
  }

  ngAfterViewInit(): void {
    // Watch for the blur event from any input element on the form.
    // This is required because the valueChanges does not provide notification on blur
    const controlBlurs: Observable<any>[] = this.formInputElements.map(
      (formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur')
    );

    // Merge the blur event observable with the valueChanges observable
    // so we only need to subscribe once.
    merge(this.projectForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.projectForm
        );
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingReload('/bat-dong-san/du-an', null);
  }

  cancelDialog() {
    this.reloadService.routingNotReload('/bat-dong-san/du-an', null);
  }

  getDistricts() {
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
          return this.locationService.getDistricts();
        })
      )
      .subscribe((res) => {
        if (res.length > 0 && this.districtIdList.length > 0) {
          this.districts = res.filter((district) =>
            this.districtIdList.includes(district.id)
          );
        } else {
          this.districts = [];
        }
      });
  }

  saveProject() {
    if (this.projectForm.valid) {
      this.loading = true;
      this.project.status = this.selectedStatus.value;
      this.project.districtId = this.selectedDistrict.id;

      this.project.handoverYear = new Date(
        moment(this.project.handoverYear).format('YYYY-MM-DDThh:mm:ssZ')
      );
      this.project = new Project(this.project, false);
      this.projectServices
        .updateProject(this.project)
        .pipe(
          takeUntil(this.destroySubs$),
          finalize(() => (this.loading = false))
        )
        .subscribe(
          (project: Project) => {
            //get Project
            this.project = project;

            swal.fire({
              title: 'Th??nh c??ng!',
              text: 'C???p nh???t d??? ??n b???t ?????ng s???n th????ng m???i th??nh c??ng.',
              icon: 'success',
              customClass: {
                confirmButton: 'btn btn-success animation-on-hover',
              },
              buttonsStyling: false,
              timer: 2000,
            });
            this.projectForm.reset();
            this.hideDialog();
          },
          (error) => {
            swal.fire({
              title: 'Th???t b???i!',
              text:
                'C???p nh???t d??? ??n b???t ?????ng s???n th????ng m???i th???t b???i v???i l???i ' +
                error.error.title,
              icon: 'error',
              customClass: {
                confirmButton: 'btn btn-info animation-on-hover',
              },
              buttonsStyling: false,
              timer: 2000,
            });
          }
        );
    }
  }

  //extend
  changeStatus(ev) {
    this.selectedStatus = ev.value;
  }
}
