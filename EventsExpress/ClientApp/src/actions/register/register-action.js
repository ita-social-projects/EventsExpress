import { AuthenticationService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';
import { createBrowserHistory } from 'history';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const SET_REGISTER_PENDING = "SET_REGISTER_PENDING";
export const SET_REGISTER_SUCCESS = "SET_REGISTER_SUCCESS";

const api_serv = new AuthenticationService();
const history = createBrowserHistory({ forceRefresh: true });

export default function register(email, password) {
  return async dispatch => {
      dispatch(getRequestInc());

    let response = await api_serv.setRegister({Email: email, Password: password});
      if(!response.ok){
        dispatch(setErrorAllertFromResponse(response));
        return Promise.reject();
      }
      dispatch(getRequestDec());
      dispatch(history.push('/registerSuccess'))
      return Promise.resolve();
  };
}

export function setRegisterPending(isRegisterPending) {
  return {
    type: SET_REGISTER_PENDING,
    isRegisterPending
  };
}

export function setRegisterSuccess(data) {
  return {
    type: SET_REGISTER_SUCCESS,
    payload: data
  };
}
