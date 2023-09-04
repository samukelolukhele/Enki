import { createAction, props } from '@ngrx/store';

type CreateUser = {
  email: string;
  password: string;
  fName: string;
  lName: string;
};

export const loginRequest = createAction(
  '[Auth] Login Request',
  props<{ credentials: { email: string; password: string } }>()
);

export const loginSuccess = createAction(
  '[Auth] Login Success.',
  props<{ successResponse: { value: string } }>()
);

export const loginFailure = createAction(
  '[Auth] Login Failure.',
  props<{ error: string }>()
);

export const register = createAction(
  '[Auth] Register Request.',
  props<{ credentials: CreateUser }>()
);

export const registerSuccess = createAction(
  '[Auth] Register Success.',
  props<{ successResponse: { value: string } }>()
);

export const registerFailure = createAction(
  '[Auth] Register Failure.',
  props<{ error: string }>()
);
