import { Component, OnInit, ElementRef, OnDestroy } from '@angular/core';
import { ROUTES } from '../sidebar/sidebar.component';
import { Location } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { LocalStorageService } from '../../features/login/local-storage.service';
import { AreaManager } from 'src/app/features/area-manager/area-manager.model';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';

var misc: any = {
  sidebar_mini_active: true,
};
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit, OnDestroy {
  private listTitles: any[];
  location: Location;

  private toggleButton: any;
  public isCollapsed = true;

  areaManager: AreaManager;

  //information areaManager
  avatarUrl: string;
  name: string;

  constructor(
    location: Location,
    private element: ElementRef,
    private router: Router,
    public toastr: ToastrService,
    private localStorageService: LocalStorageService,
    private reloadService: ReloadRouteService
  ) {
    this.location = location;
  }
  // function that adds color white/transparent to the navbar on resize (this is for the collapse)
  updateColor = () => {
    var navbar = document.getElementsByClassName('navbar')[0];
    if (window.innerWidth < 993 && !this.isCollapsed) {
      navbar.classList.add('bg-white');
      navbar.classList.remove('navbar-transparent');
    } else {
      navbar.classList.remove('bg-white');
      navbar.classList.add('navbar-transparent');
    }
  };

  minimizeSidebar() {
    const body = document.getElementsByTagName('body')[0];
    if (body.classList.contains('sidebar-mini')) {
      misc.sidebar_mini_active = true;
    } else {
      misc.sidebar_mini_active = false;
    }
    if (misc.sidebar_mini_active === true) {
      body.classList.remove('sidebar-mini');
      misc.sidebar_mini_active = false;
    } else {
      body.classList.add('sidebar-mini');
      misc.sidebar_mini_active = true;
    }

    // we simulate the window Resize so the charts will get updated in realtime.
    const simulateWindowResize = setInterval(function () {
      window.dispatchEvent(new Event('resize'));
    }, 180);

    // we stop the simulation of Window Resize after the animations are completed
    setTimeout(function () {
      clearInterval(simulateWindowResize);
    }, 2000);
  }
  showSidebarMessage(message) {
    this.toastr.show(
      '<span data-notify="icon" class="tim-icons icon-bell-55"></span>',
      message,
      {
        timeOut: 4000,
        closeButton: true,
        enableHtml: true,
        toastClass: 'alert alert-danger alert-with-icon',
        positionClass: 'toast-top-right',
      }
    );
  }
  ngOnInit() {
    this.areaManager = new AreaManager(null, false);
    this.areaManager = this.localStorageService.getUserObject();
    if (this.areaManager) {
      this.name = this.areaManager.name;
    }
    if (this.areaManager.avatarLink) {
      this.avatarUrl = this.areaManager.avatarLink;
    } else {
      this.avatarUrl = 'assets/img/defaultAvatar.png';
    }
    window.addEventListener('resize', this.updateColor);
    this.listTitles = ROUTES.filter((listTitle) => listTitle);
    const navbar: HTMLElement = this.element.nativeElement;
    this.toggleButton = navbar.getElementsByClassName('navbar-toggler')[0];
    this.router.events.subscribe((event) => {
      this.sidebarClose();
    });
  }
  ngOnDestroy() {
    window.removeEventListener('resize', this.updateColor);
  }
  getTitle() {
    var titlee = this.location.prepareExternalUrl(this.location.path());

    if (titlee.charAt(0) === '#') {
      titlee = titlee.slice(1);
    }
    if (this.listTitles) {
      for (var item = 0; item < this.listTitles.length; item++) {
        //If type sub
        if (
          this.listTitles[item].type === 'sub' &&
          this.listTitles[item].path === '/' + titlee.split('/', 3)[1]
        ) {
          for (
            var iitem = 0;
            iitem < this.listTitles[item].children.length;
            iitem++
          ) {
            if (
              this.listTitles[item].children[iitem].path ===
              titlee.split('/', 3)[2]
            ) {
              return (
                this.listTitles[item].title +
                ' / ' +
                this.listTitles[item].children[iitem].title
              );
            }
          }
        }
        //If type link
        else if (this.listTitles[item].path === titlee) {
          return this.listTitles[item].title;
        }
      }
      return 'Hồ sơ';
    }
  }
  sidebarOpen() {
    const toggleButton = this.toggleButton;
    const body = <HTMLElement>document.getElementsByTagName('body')[0];
    const html = document.getElementsByTagName('html')[0];
    if (window.innerWidth < 991) {
      body.style.position = 'fixed';
    }

    setTimeout(function () {
      toggleButton.classList.add('toggled');
    }, 500);

    html.classList.add('nav-open');
    var $layer = document.createElement('div');
    $layer.setAttribute('id', 'bodyClick');

    if (html.getElementsByTagName('body')) {
      document.getElementsByTagName('body')[0].appendChild($layer);
    }
    var $toggle = document.getElementsByClassName('navbar-toggler')[0];
    $layer.onclick = function () {
      //asign a function
      html.classList.remove('nav-open');
      setTimeout(function () {
        $layer.remove();
        $toggle.classList.remove('toggled');
      }, 400);
      const mainPanel = <HTMLElement>(
        document.getElementsByClassName('main-panel')[0]
      );

      if (window.innerWidth < 991) {
        setTimeout(function () {
          mainPanel.style.position = '';
        }, 500);
      }
    }.bind(this);

    html.classList.add('nav-open');
  }
  sidebarClose() {
    const html = document.getElementsByTagName('html')[0];
    this.toggleButton.classList.remove('toggled');
    const body = <HTMLElement>document.getElementsByTagName('body')[0];

    if (window.innerWidth < 991) {
      setTimeout(function () {
        body.style.position = '';
      }, 500);
    }
    var $layer: any = document.getElementById('bodyClick');
    if ($layer) {
      $layer.remove();
    }
    html.classList.remove('nav-open');
  }
  signOut() {
    this.localStorageService.removeUser();
    this.reloadService.routingReload('/dang-nhap', null);
  }

  routingProfile() {
    this.reloadService.routingReload('/ho-so', null);
  }
}
