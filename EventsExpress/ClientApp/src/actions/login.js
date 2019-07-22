import EventsExpressService from '../services/EventsExpressService';


export const SET_LOGIN_PENDING = "SET_LOGIN_PENDING";
export const SET_LOGIN_SUCCESS = "SET_LOGIN_SUCCESS";
export const SET_LOGIN_ERROR = "SET_LOGIN_ERROR";
export const SET_USER = "SET_USER";


const api_serv = new EventsExpressService();

export default function login(email, password) {

  return dispatch => {
    dispatch(setLoginPending(true));

    const res = api_serv.setLogin({Email: email, Password: password});
    res.then(response => {
      if(response.error == null){
          dispatch(setUser(response));
          
          dispatch(setLoginSuccess(true));
          
          localStorage.setItem('token', response.token);
        }else{
          dispatch(setLoginError(response.error));
        }
      });
  }
}




  export function setUser(data) {
    return {
      type: SET_USER,
      payload: data
    };
  }
  

function setLoginPending(isLoginPending) {
  return {
    type: SET_LOGIN_PENDING,
    isLoginPending
  };
}

function setLoginSuccess(isLoginSuccess) {
  return {
    type: SET_LOGIN_SUCCESS,
    isLoginSuccess
  };
}

function setLoginError(loginError) {
  return {
    type: SET_LOGIN_ERROR,
    loginError
  };
}
