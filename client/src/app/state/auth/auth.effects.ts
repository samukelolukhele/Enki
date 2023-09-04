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
            map((successResponse) =>
              AuthActions.loginSuccess({ successResponse })
            )
          )
      ),
      catchError((error) => of(AuthActions.loginFailure({ error })))
    );
  });

  loginSuccess$ = createEffect(
    () => {
      return this.action$.pipe(
        ofType(AuthActions.loginSuccess),
        tap(({ successResponse }) => {
          localStorage.setItem('token', `bearer ${successResponse}`);
          this.router.navigate(['dashboard']);
        })
      );
    },
    { dispatch: false }
  );

  registerRequest$ = createEffect(() => {
    return this.action$.pipe(
      ofType(AuthActions.registerRequest),
      exhaustMap((action) =>
        this.authService
          .register(action.credentials)
          .pipe(
            map((successResponse) =>
              AuthActions.registerSuccess({ successResponse })
            )
          )
      )
    );
  });

  registerSuccess = createEffect(
    () => {
      return this.action$.pipe(
        ofType(AuthActions.registerSuccess),
        tap(({ successResponse }) => {
          localStorage.setItem('token', `bearer ${successResponse}`);
        })
      );
    },
    { dispatch: false }
  );

  constructor(
    private action$: Actions,
    private authService: AuthService,
    private router: Router
  ) {}
}
