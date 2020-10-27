import EventsExpressService from '../services/EventsExpressService';
export const SET_REGISTER_PENDING = "SET_REGISTER_PENDING";
export const SET_REGISTER_SUCCESS = "SET_REGISTER_SUCCESS";
export const SET_REGISTER_ERROR = "SET_REGISTER_ERROR";

const api_serv = new EventsExpressService();

export default function register(email, password) {
  return dispatch => {
    dispatch(setRegisterPending(true));

    const res = api_serv.setRegister({Email: email, Password: password});
    res.then(response => {
      if(response.error == null){
          dispatch(setRegisterSuccess(true));
        }else{
          dispatch(setRegisterError(response.error));
        }
      });
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

export function setRegisterError(registerError) {
  return {
    type:SET_REGISTER_ERROR,
    registerError
  };
}
