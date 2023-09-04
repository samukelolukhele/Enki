import { Action, createReducer, on } from '@ngrx/store';
import { loginFailure, loginSuccess } from './auth.actions';

export interface State {
  token: string | null;
  isLoggedIn: boolean;
  loginError?: string;
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
    };
  }),
  on(loginFailure, (state, { error }) => {
    return {
      ...state,
      loginError: error,
      token: null,
      isLoggedIn: false,
    };
  })
);

export function authReducer(state: any, action: any) {
  return _authReducer(state, action);
}
