

import { reset_hub } from './chat';
import { resetNotification } from './chats';
export const SET_LOGOUT = "SET_LOGOUT";

  export default function logout(){
    localStorage.clear();
    return dispatch => { 
      dispatch(reset_hub());
      dispatch(setLogout());
      dispatch(resetNotification());
    }
  }

  function setLogout() {
    return {
      type: SET_LOGOUT
    };
  }