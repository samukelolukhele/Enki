import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { AuthService } from 'src/app/auth/auth.service';
import * as AuthActions from './auth.actions';
import { catchError, exhaustMap, map, of, tap } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class AuthEffects {
  loginRequest$ = createEffect(() => {
    return this.action$.pipe(
      ofType(AuthActions.loginRequest),
      exhaustMap((action) =>
        this.authService
          .login(action.credentials.email, action.credentials.password)
          .pipe(
            map((loginSuccessResponse) =>
              AuthActions.loginSuccess({ loginSuccessResponse })
            )
          )
      ),
      catchError((error) => of(AuthActions.loginFailure({ error })))
    );
  });

  loginSuccess$ = createEffect(() => {
    return this.action$.pipe(
      ofType(AuthActions.loginSuccess),
      tap(({ loginSuccessResponse }) => {
        this.router.navigateByUrl('/dashboard');
      })
    );
  });

  constructor(
    private action$: Actions,
    private authService: AuthService,
    private router: Router
  ) {}
}
