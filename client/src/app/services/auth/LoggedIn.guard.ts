import { Injectable } from '@angular/core';
import { Router, UrlTree } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable, map } from 'rxjs';
import { selectIsLoggedIn } from 'src/app/state/auth/auth.selectors';

/**
 *@description Prevents the user from accessing certain pages if they are logged in e.g login & register page.
 *
 * @export
 * @class LoggedInGuard
 */
@Injectable({
  providedIn: 'root',
})
export class LoggedInGuard {
  constructor(private store: Store, private router: Router) {}

  canActivate():
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.store.select(selectIsLoggedIn).pipe(
      map((authenticate) => {
        if (authenticate) {
          return this.router.createUrlTree(['dashboard']);
        }

        return true;
      })
    );
  }
}
