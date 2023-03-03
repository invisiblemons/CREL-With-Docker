import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Project } from '../project.model';
import { ProjectService } from '../project.service';
import { Column } from 'src/app/core/models/table.model';
import swal from 'sweetalert2';
import { Subject } from 'rxjs';
import { TABLE_CONFIG } from 'src/app/shared/constants/common.const';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';
import { switchMap, takeUntil } from 'rxjs/operators';
import { TeamService } from '../../team/team.service';
import { LocalStorageService } from '../../login/local-storage.service';
import { Team } from '../../team/team.model';
import { Ward } from 'src/app/shared/models/ward.model';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss'],
})
export class ProjectListComponent implements OnInit, OnDestroy {
  destroySubs$: Subject<boolean> = new Subject<boolean>();

  TABLE_CONFIG = TABLE_CONFIG;

  cols: Column[];

  displayCols: Column[];

  _selectedColumns: Column[];

  get selectedColumns(): Column[] {
    return this._selectedColumns;
  }

  set selectedColumns(val: Column[]) {
    this._selectedColumns = this.cols.filter(
      (col: Column) => val.includes(col) || col.disabled
    );
  }

  first = 0;

  isShowSpin: boolean;

  loading: boolean;

  statuses: { label: string; value: number }[];

  selectedStatus: { label: string; value: number };

  //project
  project: Project;

  projects: Project[] = [];

  selectedProjects: Project[] = [];

  index: number;

  teams: Team[] = [];

  rawWards: Ward[] = [];
  districtIdList: number[] = [];

  constructor(
    private projectServices: ProjectService,
    private router: Router,
    private reloadServices: ReloadRouteService,
    private teamServices: TeamService,
    private localStorageService: LocalStorageService
  ) {
    this.isShowSpin = true;
    this.loading = false;
    this.statuses = [
      { label: 'Đã xoá', value: 0 },
      { label: 'Hoạt động', value: 1 },
    ];
    this.projects = [];

    this.cols = [
      {
        field: 'cbb',
        header: '',
        width: '3rem',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: 'id',
        header: 'Mã',
        width: '5rem',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: 'name',
        header: 'Tên',
        width: '12rem',
        disabled: true,
        visible: true,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'company',
        header: 'Tên chủ đầu tư',
        width: '12rem',
        disabled: false,
        visible: false,
        headerAlign: 'left',
        textAlign: 'left',
      },
      {
        field: 'handoverYear',
        header: 'Năm bàn giao',
        width: '5rem',
        disabled: false,
        visible: false,
        headerAlign: 'right',
        textAlign: 'right',
      },
      {
        field: 'status',
        header: 'Trạng thái',
        width: '10rem',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
      {
        field: 'action',
        header: 'Thao tác',
        width: '15rem',
        disabled: true,
        visible: true,
        headerAlign: 'center',
        textAlign: 'center',
      },
    ];
    this.displayCols = this.cols.filter((element) => !element.disabled);
    this._selectedColumns = this.cols.filter((element) => element.visible);
  }

  ngOnInit() {
    this.getProjects();
  }

  ngOnDestroy() {
    // Unsubscribe from the subject
    this.destroySubs$.next(true);
    this.destroySubs$.unsubscribe();
  }

  //data functions
  getProject(project: Project): Project {
    for (let i = 0; i < this.projects.length; i++) {
      if (project.id === this.projects[i].id) {
        return this.projects[i];
      }
    }
    return;
  }
  getProjects() {
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
          return this.projectServices.getProjects();
        })
      )
      .subscribe((res) => {
        
        let temp = [];
        if(res.length >0 && this.districtIdList.length>0) {
          res.forEach(project => {
            if(this.districtIdList.includes(project.districtId)) {
              temp.push(project);
            }
          });
          this.projects = temp;
          this.isShowSpin = false;
        } else {
          this.isShowSpin = false;
          this.projects = [];
        }
      });
  }

  // paging
  next() {
    this.first = this.first + this.TABLE_CONFIG.ROWS;
  }
  prev() {
    this.first = this.first - this.TABLE_CONFIG.ROWS;
  }
  reset() {
    this.first = 0;
  }
  isLastPage(): boolean {
    return this.projects
      ? this.first === this.projects.length - this.TABLE_CONFIG.ROWS
      : true;
  }
  isFirstPage(): boolean {
    return this.projects ? this.first === 0 : true;
  }

  //table functions
  deleteSelectedProjects() {
    swal
      .fire({
        title: 'Bạn có chắc muốn xoá?',
        text: 'Những thông tin dự án bất động sản thương mại này sẽ bị xoá!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Có, xoá nó!',
        cancelButtonText: 'Không, giữ nguyên',
        customClass: {
          confirmButton: 'btn btn-danger animation-on-hover mr-1',
          cancelButton: 'btn btn-default animation-on-hover',
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          this.selectedProjects.forEach((project, index) => {
            this.projectServices.deleteProject(project).subscribe((res) => {
              if (res) {
                this.selectedProjects[index] = res;
              }
            });
          });
          //Update table
          this.projects.forEach((projectItem) => {
            this.selectedProjects.forEach((selectedProjectItem) => {
              if (projectItem.id === selectedProjectItem.id) {
                projectItem = selectedProjectItem;
              }
            });
          });
          swal.fire({
            title: 'Không hoạt động!',
            text: 'Những thông tin dự án BĐS được chọn đã bị xoá.',
            icon: 'success',
            customClass: {
              confirmButton: 'btn btn-success animation-on-hover',
            },
            buttonsStyling: false,
            timer: 2000,
          });
        }
      });
  }
  deleteProject(project) {
    swal
      .fire({
        title: 'Bạn có chắc muốn xoá?',
        text: 'Thông tin dự án BĐS này sẽ bị xoá!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Có, xoá nó!',
        cancelButtonText: 'Không, giữ nguyên',
        customClass: {
          confirmButton: 'btn btn-danger animation-on-hover mr-1',
          cancelButton: 'btn btn-default animation-on-hover',
        },
        buttonsStyling: false,
      })
      .then((result) => {
        if (result.value) {
          this.projectServices.deleteProject(project).subscribe((res) => {
            if (res) {
              project.status = 0;
              swal.fire({
                title: 'Xoá thành công!',
                text: 'Thông tin dự án BĐS đã xoá.',
                icon: 'success',
                customClass: {
                  confirmButton: 'btn btn-success animation-on-hover',
                },
                buttonsStyling: false,
                timer: 2000,
              });
            }
          });
        }
      });
  }
  openDetailProject(project) {
    this.reloadServices.routingNotReload(
      '/bat-dong-san/du-an/chi-tiet',
      project.id
    );
  }
  openEditProject(project) {
    this.reloadServices.routingNotReload(
      '/bat-dong-san/du-an/chinh-sua',
      project.id
    );
  }
  openNewProject() {
    this.reloadServices.routingNotReload('/bat-dong-san/du-an/tao-moi', null);
  }
}
