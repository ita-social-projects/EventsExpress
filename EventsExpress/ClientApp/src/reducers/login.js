import { SET_LOGIN_PENDING, SET_LOGIN_SUCCESS, SET_LOGIN_ERROR} from "../actions/login";
import { SET_LOGOUT } from '../actions/logout';
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

    case SET_LOGIN_ERROR:
      return Object.assign({}, state, {
        loginError: action.loginError,
        isLoginSuccess: false,
        isLoginPending: false
      });

    case SET_LOGOUT:
      return Object.assign({}, state, {
        isLoginSuccess: !action.isLoginSuccess
          });
    default:
      return state;
  }
}