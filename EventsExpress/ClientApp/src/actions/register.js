import EventsExpressService from '../services/EventsExpressService';


export const SET_REGISTER_PENDING = "SET_REGISTER_PENDING";
export const SET_REGISTER_SUCCESS = "SET_REGISTER_SUCCESS";
export const SET_REGISTER_ERROR = "SET_REGISTER_ERROR";


const api_serv = new EventsExpressService();


export default function register(email, password) {
  return dispatch => {
    dispatch(setRegisterPending(false));

    const res = api_serv.setResource('Authentication/login', {Email: email, Password: password});
    res.then(response => {
      if(response.error == null){
          dispatch(setUser(response));
          
          dispatch(setLoginSuccess(true));
        }else{
          dispatch(setLoginError(response.error));
        }
      });

  };
}

function setRegisterPending(isRegisterPending) {
  return {
    type: SET_REGISTER_PENDING,
    isRegisterPending
  };
}

function setRegisterSuccess(isRegisterSuccess) {
  return {
    type: SET_REGISTER_SUCCESS,
    isRegisterSuccess
  };
}

function setRegisterError(registerError) {
  return {
    type:SET_REGISTER_ERROR,
    registerError
  };
}

function callRegisterApi(email, password, callback) {
  setTimeout(() => {
    if (email !== "admin@example.com" && password) {
      return callback(null);
    } else {
      return callback(new Error("This email is already exsist"));
    }
  }, 1000);
}