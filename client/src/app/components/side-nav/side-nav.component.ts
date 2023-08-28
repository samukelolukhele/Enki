import { Component, Input, OnInit } from '@angular/core';
import {
  faDashboard,
  faCalendar,
  faPen,
  faUser,
  faBars,
  faClose,
} from '@fortawesome/free-solid-svg-icons';
import { IsMobileDirective } from '../../shared/directives/isMobile.directive';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.sass'],
  viewProviders: [IsMobileDirective],
})
export class SideNavComponent implements OnInit {
  faDashboard = faDashboard;
  faCalendar = faCalendar;
  faPen = faPen;
  faUser = faUser;
  faBars = faBars;
  faClose = faClose;
  expanded = false;
  constructor() {}

  ngOnInit() {}

  expandNav() {
    this.expanded = !this.expanded;
  }
}
