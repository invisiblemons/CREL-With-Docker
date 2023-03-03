import {
  Component,
  OnInit,
  OnDestroy,
  ViewChildren,
  ElementRef,
  AfterViewInit,
} from '@angular/core';
import { fromEvent, merge, Observable, of, Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { Broker } from '../broker.model';
import { BrokerService } from '../broker.service';
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
import { TeamService } from '../../team/team.service';
import { Team } from '../../team/team.model';
import { EMAIL_PATTERN } from 'src/app/shared/constants/common.const';
import { PrimeNGConfig } from 'primeng/api';
import { default as data } from '../../../../assets/json/vi.json';
import moment from 'moment';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';

@Component({
  selector: 'app-broker-edit',
  templateUrl: './broker-edit.component.html',
  styleUrls: ['./broker-edit.component.scss'],
})
export class BrokerEditComponent implements OnInit, AfterViewInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  isShowSkeleton: boolean;

  isShowDialog: boolean;

  loading: boolean;

  // PhoneNumber
  isChangedPhoneNumber: boolean;

  //gender
  genders: any;

  selectedGender = { gender: "Nam" };

  //date
  maxDate: Date = new Date();
  minDate: Date = new Date();

  statuses: { label: string; value: number }[];

  selectedStatus: { label: string; value: number };

  //broker
  broker: Broker;

  //team
  team: Team;

  teams: Team[] = [];

  selectedTeam: Team;

  //image
  avatarFile: File;

  changedImg: boolean;

  //editor
  editorValueType: string;

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
    private teamServices: TeamService,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    public primengConfig: PrimeNGConfig,
    private reloadService: ReloadRouteService,
  ) {
    // Config VI for calendar
    this.primengConfig.setTranslation(data.primeng);

    this.isShowDialog = true;
    this.isShowSkeleton = true;
    this.loading = false;
    this.statuses = [
      { label: 'Đã xoá', value: 0 },
      { label: "Hoạt động", value: 1 }
    ];

    this.maxDate = new Date(
      this.maxDate.setFullYear(this.maxDate.getFullYear() - 18)
    );

    this.minDate = new Date(
      this.minDate.setFullYear(this.minDate.getFullYear() - 100)
    );

    this.genders = [{ gender: 'Nam' }, { gender: 'Nữ' }];
    this.genders = [{ gender: 'Nam' }, { gender: 'Nữ' }];

    //editor
    this.editorValueType = 'html';

    //image
    this.changedImg = false;

    //validate
    this.validationMessages = {
      name: {
        required: 'Tên không được để trống.',
        minlength: 'Tên phải có ít nhất 3 kí tự.',
        maxlength: 'Tên có nhiều nhất 100 kí tự.',
      },
      phoneNumber: {
        required: 'Số điện thoại không được để trống.',
      },
      email: {
        required: 'Địa chỉ email không được để trống.',
        pattern: 'Địa chỉ email không hợp lệ',
      },
      status: {
        required: 'Trạng thái không được để trống.',
      },
      gender: {
        required: 'Giới tính không được để trống.',
      },
      userName: {
        required: 'Tài khoản không được để trống.',
      },
      birthday: {
        required: 'Ngày sinh không được để trống.',
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
      status: ['', [Validators.required]],
      gender: ['', [Validators.required]],
      userName: [{ value: '', disabled: true }, [Validators.required]],
      birthday: ['', [Validators.required]],
    });

    // Load Data
    this.route.queryParams
      .pipe(
        takeUntil(this.destroySubs$),
        switchMap((params) => {
          return this.brokerServices.getBrokerById(params['id']);
        }),
        switchMap((broker) => {
          this.broker = broker;
          return this.teamServices.getTeams();
        })
      )
      .subscribe((teams) => {
        this.teams = teams;
        this.broker = this.updateTeam(this.broker);
        this.selectedTeam = this.broker.team;
        this.statuses.forEach((status) => {
          status.value === this.broker.status
            ? (this.selectedStatus = status)
            : '';
        });

        //label for gender
        this.broker.gender == true
          ? (this.selectedGender = { gender: 'Nam' })
          : (this.selectedGender = { gender: 'Nữ' });
        // set value for fields form
        this.brokerForm.controls.userName.setValue(this.broker.userName);
        this.broker.birthday = new Date(this.broker.birthday);

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

  changeInputPhoneStyle() {
    this.isChangedPhoneNumber = true;
  }

  // Image Function
  getImgFromChild(imgFile) {
    this.avatarFile = imgFile;
    this.changedImg = true;
  }

  saveBroker() {
    if (this.brokerForm.valid) {
      this.loading = true;
      this.broker.status = this.selectedStatus.value;
      this.broker.email = this.broker.email.toLowerCase();
      if (this.isChangedPhoneNumber) {
        if (this.broker.phoneNumber.includes(' ')) {
          let phoneArr = this.broker.phoneNumber.split(' ');
          this.broker.phoneNumber = phoneArr[0] + phoneArr[1] + phoneArr[2];
        }
      }
      this.selectedGender.gender == 'Nam'
        ? (this.broker.gender = true)
        : (this.broker.gender = false);
      this.broker.birthday = new Date(
        moment(this.broker.birthday).format('YYYY-MM-DDThh:mm:ssZ')
      );
      this.broker = new Broker(this.broker, false);
      this.brokerServices
        .updateBroker(this.broker)
        .pipe(
          takeUntil(this.destroySubs$),
          switchMap((broker) => {
            this.brokerForm.reset();
            this.hideDialog();
            swal.fire({
              title: 'Thành công!',
              text: 'Cập nhật Nhân viên môi giới thành công.',
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
          })
        )
        .subscribe(
          (brokerAfterUpdateImage: Broker) => {
            this.brokerServices
                .resetPassword(this.broker.email)
                .subscribe((res) => {
                  
                });
          },
          (error) => {
            swal.fire({
              title: 'Thất bại!',
              text: 'Tài khoản hoặc mật khẩu không hợp lệ',
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
