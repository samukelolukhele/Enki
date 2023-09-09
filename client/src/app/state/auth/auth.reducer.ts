import { Action, createReducer, on } from '@ngrx/store';
import {
  loginFailure,
  loginSuccess,
  registerFailure,
  registerSuccess,
} from './auth.actions';

export interface State {
  token: string | null;
  isLoggedIn: boolean;
  loginError?: string | null;
}

export const initialState: State = {
  token: null,
  isLoggedIn: false,
};

const _authReducer = createReducer(
  initialState,
  on(loginSuccess, (state, { successResponse }) => {
    return {
      ...state,
      token: successResponse.value,
      isLoggedIn: true,
      loginError: null,
    };
  }),
  on(loginFailure, (state, { error }) => {
    return {
      ...state,
      loginError: error,
      token: null,
      isLoggedIn: false,
    };
  }),
  on(registerSuccess, (state, { successResponse }) => {
    return {
      ...state,
      token: successResponse.value,
      isLoggedIn: true,
    };
  }),
  on(registerFailure, (state, { error }) => {
    return {
      ...state,
      loginError: error,
      token: null,
      isLoggedIn: false,
    };
  })
);

export function authReducer(state = initialState, action: any): State {
  return _authReducer(state, action);
}
