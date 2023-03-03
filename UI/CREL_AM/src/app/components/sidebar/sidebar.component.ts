import { Component, OnInit } from '@angular/core';
import { PrimeIcons } from 'primeng/api';
import { ReloadRouteService } from 'src/app/shared/services/reload-route.service';

export interface RouteInfo {
  path: string;
  title: string;
  type: string;
  icontype: string;
  collapse?: string;
  isCollapsed?: boolean;
  isCollapsing?: any;
  children?: ChildrenItems[];
}

export interface ChildrenItems {
  path: string;
  title: string;
  icontype: string;
  type?: string;
  collapse?: string;
  children?: ChildrenItems2[];
  isCollapsed?: boolean;
}
export interface ChildrenItems2 {
  path?: string;
  icontype: string;
  title?: string;
  type?: string;
}
//Menu Items
export const ROUTES: RouteInfo[] = [
  {
    path: "/tong-quan",
    title: "Tổng quan",
    type: "link",
    icontype: "tim-icons icon-bank",
  },
  {
    path: '/bat-dong-san',
    title: 'Bất động sản',
    type: 'sub',
    icontype: PrimeIcons.BRIEFCASE,
    collapse: 'pages',
    isCollapsed: true,
    children: [
      {
        path: 'danh-sach',
        title: 'Danh sách BĐS',
        type: 'link',
        icontype: PrimeIcons.LIST,
      },
      {
        path: 'du-an',
        title: 'Thông tin dự án',
        type: 'link',
        icontype: PrimeIcons.FOLDER,
      },
      {
        path: 'nguoi-so-huu',
        title: 'Thông tin người sở hữu',
        type: 'link',
        icontype: PrimeIcons.USER,
      },
    ],
  },
  {
    path: "/thuong-hieu",
    title: "Thương hiệu",
    type: "sub",
    icontype: PrimeIcons.TH_LARGE,
    collapse: "pages",
    isCollapsed: true,
    children: [
      {
        path: "danh-sach",
        title: "Danh sách thương hiệu",
        type: "link",
        icontype: PrimeIcons.LIST,
      },
      {
        path: "cuoc-hen",
        title: "Cuộc hẹn",
        type: "link",
        icontype: "tim-icons icon-puzzle-10",
      },
      {
        path: "hop-dong",
        title: "Hợp đồng",
        type: "link",
        icontype: PrimeIcons.BOOK,
      },
    ]
  },
  {
    path: '/nguoi-moi-gioi/danh-sach',
    title: 'Nhân viên môi giới',
    type: 'link',
    icontype: 'tim-icons icon-user-run',
  },
  {
    path: '/nhom/danh-sach',
    title: 'Nhóm',
    type: 'link',
    icontype: PrimeIcons.USERS,
  }
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent implements OnInit {
  menuItems: any[];

  constructor(private reloadRouteServices: ReloadRouteService) {}

  ngOnInit() {
    this.menuItems = ROUTES.filter((menuItem) => menuItem);
  }

  routingFunction(path) {
    this.reloadRouteServices.routingReload(path, null);
  }
}
