export const SET_LOGIN_PENDING = "SET_LOGIN_PENDING";
export const SET_LOGIN_SUCCESS = "SET_LOGIN_SUCCESS";
export const SET_LOGIN_ERROR = "SET_LOGIN_ERROR";

export default function login(email, password) {
  return dispatch => {
    dispatch(setLoginPending(false));
    callLoginApi(email, password, error => {
      if (!error) {
        dispatch(setLoginSuccess(true));
      } else {
        dispatch(setLoginError(error));
      }
    });
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

function callLoginApi(email, password, callback) {
  setTimeout(() => {
    if (email === "admin@example.com" && password === "admin") {
      return callback(null);
    } else {
      return callback(new Error("Invalid email and password"));
    }
  }, 1000);
}