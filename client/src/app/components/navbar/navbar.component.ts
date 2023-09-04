import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable, of } from 'rxjs';
import { selectIsLoggedIn } from 'src/app/state/auth/auth.selectors';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.sass'],
})
export class NavbarComponent implements OnInit {
  isLoggedIn$: boolean = false;
  constructor(private store: Store) {
    this.store
      .select(selectIsLoggedIn)
      .subscribe((isLoggedIn) => (this.isLoggedIn$ = isLoggedIn));
  }

  ngOnInit(): void {}
}
