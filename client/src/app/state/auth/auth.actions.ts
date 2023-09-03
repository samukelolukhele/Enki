import { createAction, props } from '@ngrx/store';

export const loginRequest = createAction(
  '[Auth] Login Request',
  props<{ credentials: { email: string; password: string } }>()
);

//! Implement
// export const register = createAction("[Auth] Register Request", props<{ credentials: { email: string; password: string, fName } }>)

export const loginSuccess = createAction(
  '[Auth] Login Success.',
  props<{ loginSuccessResponse: { token: string } }>()
);

export const loginFailure = createAction(
  '[Auth] Login Failure.',
  props<{ error: string }>()
);
