import {
  SET_LOGIN_PENDING,
  SET_LOGIN_SUCCESS,
  SET_LOGIN_ERROR
} from "../actions/login";

import { 
  SET_LOGOUT }
  from '../actions/logout';

export const reducer = (
  state = {
    isLoginSuccess: false,
    isLoginPending: false,
    loginError: null
  },
  action
) => {
  switch (action.type) {
    case SET_LOGIN_PENDING:
      return Object.assign({}, state, {
        isLoginPending: action.isLoginPending
      });

    case SET_LOGIN_SUCCESS:
      return Object.assign({}, state, {
        isLoginSuccess: action.isLoginSuccess
      });

    case SET_LOGIN_ERROR:
      return Object.assign({}, state, {
        loginError: action.loginError
      });

    case SET_LOGOUT:
      return Object.assign({}, state, {
        isLoginSuccess: !action.isLoginSuccess
      });

    default:
      return state;
  }
}