import {
  Component,
  OnInit,
  OnDestroy,
  ElementRef,
  ViewChildren,
  AfterViewInit,
} from '@angular/core';
import { Router } from '@angular/router';
import { fromEvent, merge, Observable, of, Subject } from 'rxjs';
import { Broker } from '../broker.model';
import { BrokerService } from '../broker.service';
import swal from 'sweetalert2';
import { debounceTime, finalize, switchMap, takeUntil } from 'rxjs/operators';
import 'devextreme/ui/html_editor/converters/markdown';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormControlName,
} from '@angular/forms';
import { GenericValidator } from 'src/app/shared/validator/generic-validator';
import { TeamService } from '../../team/team.service';
import { Team } from '../../team/team.model';
import { EMAIL_PATTERN } from 'src/app/shared/constants/common.const';
import { PrimeNGConfig } from 'primeng/api';
import { default as data } from '../../../../assets/json/vi.json';
import moment from 'moment';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';
import { StringValidator } from 'src/app/shared/validator/string-validator';

@Component({
  selector: 'app-broker-new',
  templateUrl: './broker-new.component.html',
  styleUrls: ['./broker-new.component.scss'],
})
export class BrokerNewComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  //broker
  broker: Broker;

  //team
  team: Team;

  teams: Team[] = [];

  selectedTeam: Team;

  //gender
  genders: any;

  selectedGender = { gender: "Nam" };

  //date
  maxDate: Date = new Date();
  minDate: Date = new Date();

  //image
  avatarFile: File;

  changedImg: boolean;

  //editor
  editorValueType = 'html';

  //validate
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements!: ElementRef[];

  errorMessage = '';

  brokerForm!: FormGroup;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };

  private genericValidator: GenericValidator;

  constructor(
    private brokerServices: BrokerService,
    private router: Router,
    private fb: FormBuilder,
    private teamServices: TeamService,
    public primengConfig: PrimeNGConfig,
    private reloadService: ReloadRouteService,
  ) {
    // Config VI for calendar
    this.primengConfig.setTranslation(data.primeng);

    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;

    this.broker = new Broker(null, false);
    this.selectedTeam = null;

    this.maxDate = new Date(
      this.maxDate.setFullYear(this.maxDate.getFullYear() - 18)
    );

    this.minDate = new Date(
      this.minDate.setFullYear(this.minDate.getFullYear() - 100)
    );

    this.genders = [{ gender: 'Nam' }, { gender: 'N???' }];

    //image
    this.changedImg = false;

    //editor
    this.editorValueType = 'html';

    //validate
    this.validationMessages = {
      name: {
        required: 'T??n kh??ng ???????c ????? tr???ng.',
        minlength: 'T??n ph???i c?? ??t nh???t 3 k?? t???.',
        maxlength: 'T??n c?? nhi???u nh???t 100 k?? t???.',
      },
      phoneNumber: {
        required: 'S??? ??i???n tho???i kh??ng ???????c ????? tr???ng.',
      },
      email: {
        required: '?????a ch??? email kh??ng ???????c ????? tr???ng.',
        pattern: '?????a ch??? email kh??ng h???p l???',
      },
      gender: {
        required: 'Gi???i t??nh kh??ng ???????c ????? tr???ng.',
      },
      userName: {
        required: 'T??i kho???n kh??ng ???????c ????? tr???ng.',
      },
      birthday: {
        required: 'Ng??y sinh kh??ng ???????c ????? tr???ng.',
      },
      password: {
        required: 'M???t kh???u kh??ng ???????c ????? tr???ng.',
      },
      checkingPassword: {
        required: "M???t kh???u x??c nh???n kh??ng ???????c ????? tr???ng.",
        passwordNotMatch: "M???t kh???u x??c nh???n ph???i tr??ng v???i m???t kh???u m???i",
      },
    };
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.brokerForm = this.fb.group({
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ],
      ],
      email: ['', [Validators.required, Validators.pattern(EMAIL_PATTERN)]],
      phoneNumber: ['', [Validators.required]],
      gender: ['', [Validators.required]],
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]],
      checkingPassword: [
        "",
        [Validators.required, StringValidator.confirmedPassword("password")],
      ],
      birthday: ['', [Validators.required]],
    });

    // Load Data
    this.broker.birthday = this.maxDate;
    this.brokerForm.controls.birthday.setValue(this.maxDate);

    this.isShowSkeleton = false;
  }

  ngAfterViewInit(): void {
    // Watch for the blur event from any input element on the form.
    // This is required because the valueChanges does not provide notification on blur
    const controlBlurs: Observable<any>[] = this.formInputElements.map(
      (formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur')
    );

    // Merge the blur event observable with the valueChanges observable
    // so we only need to subscribe once.
    merge(this.brokerForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(100))
      .subscribe((value) => {
        this.displayMessage = this.genericValidator.processMessages(
          this.brokerForm
        );
      });
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  hideDialog() {
    this.reloadService.routingReload('/nguoi-moi-gioi', null);
  }

  cancelDialog() {
    this.reloadService.routingNotReload('/nguoi-moi-gioi', null);
  }

  //data functions
  getTeam(broker: Broker): Team {
    for (let i = 0; i < this.teams.length; i++) {
      if (broker.teamId === this.teams[i].id) {
        return this.teams[i];
      }
    }
    return;
  }
  updateTeam(broker: Broker): Broker {
    let team = this.getTeam(broker);
    if (team) {
      broker.team = team;
      broker.teamName = team.name;
    }
    return broker;
  }
  getTeams() {
    this.teamServices.getTeams().subscribe((res) => {
      this.teams = res;
    });
  }

  // Image Function
  getImgFromChild(imgFile) {
    this.avatarFile = imgFile;
    this.changedImg = true;
  }

  saveBroker() {
    this.loading = true;
    this.brokerServices
      .getBrokerByUserName(this.broker.userName)
      .subscribe((brokers) => {
        if (brokers.length > 0) {
          this.broker.userName = null;
          swal.fire({
            title: 'C???nh b??o!',
            text: 'T??i kho???n ???? t???n t???i.',
            icon: 'warning',
            customClass: {
              confirmButton: 'btn btn-warning animation-on-hover',
            },
            buttonsStyling: false,
            timer: 2000,
          });
          this.loading = false;
        } else {
          this.broker.email = this.broker.email.toLowerCase();
          if (this.broker.phoneNumber.includes(' ')) {
            let phoneArr = this.broker.phoneNumber.split(' ');
            this.broker.phoneNumber = phoneArr[0] + phoneArr[1] + phoneArr[2];
          }
          this.selectedGender.gender == 'Nam'
            ? (this.broker.gender = true)
            : (this.broker.gender = false);
          this.broker.birthday = new Date(
            moment(this.broker.birthday).format('YYYY-MM-DDThh:mm:ssZ')
          );
          this.broker.status = 1;
          this.broker = new Broker(this.broker, true);
          this.brokerServices
            .insertBroker(this.broker)
            .pipe(
              takeUntil(this.destroySubs$),
              switchMap((broker) => {
                this.brokerForm.reset();
                this.hideDialog();
                swal.fire({
                  title: 'Th??nh c??ng!',
                  text: 'T???o m???i Nh??n vi??n m??i gi???i th??nh c??ng.',
                  icon: 'success',
                  customClass: {
                    confirmButton: 'btn btn-success animation-on-hover',
                  },
                  buttonsStyling: false,
                  timer: 2000,
                });
                //update image
                if (this.changedImg && this.avatarFile) {
                  let formData: FormData = new FormData();
                  formData.append('file', this.avatarFile);
                  return this.brokerServices.updateBrokerAvatar(
                    broker.id,
                    formData
                  );
                }
                return of(broker);
              }),
              finalize(() => {
                this.loading = false;
                this.broker.userName = null; //clear field on form
              })
            )
            .subscribe(
              (brokerAfterUpdateImage) => {},
              (error) => {
                swal.fire({
                  title: 'Th???t b???i!',
                  text: 'T??i kho???n ho???c m???t kh???u kh??ng h???p l???',
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
      });
  }
}
