import { Injectable } from '@angular/core';
import { Router, UrlTree } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable, map } from 'rxjs';
import { selectIsLoggedIn } from 'src/app/state/auth/auth.selectors';

/**
 *@description Blocks the user from accessing certain pages if they are not logged in.
 *
 * @export
 * @class AuthGuard
 */
@Injectable({
  providedIn: 'root',
})
export class AuthGuard {
  constructor(private store: Store, private router: Router) {}

  canActivate():
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.store.select(selectIsLoggedIn).pipe(
      map((authenticate) => {
        if (!authenticate) {
          return this.router.createUrlTree(['login']);
        }

        return true;
      })
    );
  }
}
