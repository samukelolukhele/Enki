import { Component, OnInit } from '@angular/core';
import {
  faDashboard,
  faCalendar,
  faPen,
  faUser,
} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.sass'],
})
export class SideNavComponent implements OnInit {
  faDashboard = faDashboard;
  faCalendar = faCalendar;
  faPen = faPen;
  faUser = faUser;
  constructor() {}

  ngOnInit() {}
}
