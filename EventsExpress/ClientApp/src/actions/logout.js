

import { reset_hub } from './chat';
export const SET_LOGOUT = "SET_LOGOUT";

  export default function logout(){
    localStorage.clear();
    return dispatch => { 
      dispatch(reset_hub());
      dispatch(setLogout());
    }
  }

  function setLogout() {
    return {
      type: SET_LOGOUT
    };
  }