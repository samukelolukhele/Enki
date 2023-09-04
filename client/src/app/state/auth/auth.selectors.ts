import { createFeatureSelector, createSelector } from '@ngrx/store';
import { State } from './auth.reducer';

const selectLogin = createFeatureSelector<State>('auth');

export const selectToken = createSelector(selectLogin, (state) => state.token);

export const selectIsLoggedIn = createSelector(
  selectLogin,
  (state) => state.isLoggedIn
);

export const selectLoginError = createSelector(
  selectLogin,
  (state) => state.loginError
);
