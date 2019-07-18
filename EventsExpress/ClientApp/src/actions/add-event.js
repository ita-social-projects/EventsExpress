import EventsExpressService from '../services/EventsExpressService';
import get_events from './event-list';
export const SET_EVENT_SUCCESS = "SET_EVENT_SUCCESS";
export const SET_EVENT_PENDING = "SET_EVENT_PENDING";
export const SET_EVENT_ERROR = "SET_EVENT_ERROR";

const api_serv = new EventsExpressService();

export default function add_event(data) {

    return dispatch => {
      dispatch(setEventPending(true));
  
      const res = api_serv.setEvent(data);
      res.then(response => {
        if(response.error == null){
            
            dispatch(setEventSuccess(true));
            get_events();
          }else{
            dispatch(setEventError(response.error));
          }
        });
    }
  }


  function setEventSuccess(data) {
    return {
      type: SET_EVENT_SUCCESS,
      payload: data
    };
  }

  function setEventPending(data) {
    return {
      type: SET_EVENT_PENDING,
      payload: data
    };
  }

  function setEventError(data) {
    return {
      type: SET_EVENT_ERROR,
      payload: data
    };
  }
  
