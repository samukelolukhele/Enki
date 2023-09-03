import { Action, createReducer, on } from '@ngrx/store';
import { loginFailure, loginSuccess } from './auth.actions';

export interface State {
  token: string | null;
  loginError?: string;
}

export const initialState: State = {
  token: null,
};

const _authReducer = createReducer(
  initialState,
  on(loginSuccess, (state, { loginSuccessResponse }) => {
    return {
      ...state,
      token: loginSuccessResponse.token,
    };
  }),
  on(loginFailure, (state, { error }) => {
    return {
      ...state,
      loginError: error,
      token: null,
    };
  })
);

export function authReducer(state: any, action: any) {
  return _authReducer(state, action);
}
