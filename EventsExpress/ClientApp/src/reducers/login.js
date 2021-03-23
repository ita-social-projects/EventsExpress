import {
  SET_LOGIN_PENDING,
  SET_LOGIN_SUCCESS
} from "../actions/login/login-action";
import { SET_LOGOUT } from '../actions/login/logout-action';
import initialState from '../store/initialState';

export const reducer = (
  state = initialState.login,
  action
) => {
  switch (action.type) {
    case SET_LOGIN_PENDING:
      return Object.assign({}, state, {
        isLoginPending: action.isLoginPending
      });

    case SET_LOGIN_SUCCESS:
      return Object.assign({}, state, {
        isLoginSuccess: action.isLoginSuccess,
        loginError: null,
        isLoginPending: false,
      });

    case SET_LOGOUT:
      return Object.assign({}, state, {
        isLoginSuccess: !action.isLoginSuccess
      });
    default:
      return state;
  }
}
