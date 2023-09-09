import { Action, ActionReducer, MetaReducer } from '@ngrx/store';
import { merge, pick } from 'lodash-es';

/**
 *
 *
 * @param {*} state
 * @param {string} localStorageKey
 * @return {void} Sets state value to local storage with its entered key
 */
function saveState(state: any, localStorageKey: string): void {
  return localStorage.setItem(localStorageKey, JSON.stringify(state));
}

/**
 *
 *
 * @param {string} localStorageKey
 * @return {*} Returns state stored in local storage
 */
function getSavedState(localStorageKey: string) {
  return JSON.parse(localStorage.getItem(localStorageKey) || '{}');
}

// Key from state which we'd like to save.
const stateKeys: string[] = [
  'auth.isLoggedIn',
  'auth.token',
  'auth.loginError',
];

const localStorageKey = '__app_storage__'; // Key for local storage

export function storageMetaReducer<S, A extends Action = Action>(
  reducer: ActionReducer<S, A>
) {
  let onInit = true; // after refresh...

  return function (state: S, action: A): S {
    console.log(state);
    const nextState = reducer(state, action);

    //init the application state.

    if (onInit) {
      onInit = false;
      const savedState = getSavedState(localStorageKey);
      return merge(nextState, savedState);
    }

    const stateToSave = pick(nextState, stateKeys);
    saveState(nextState, localStorageKey);
    return nextState;
  };
}

export const metaReducers: MetaReducer<any>[] = [storageMetaReducer];
