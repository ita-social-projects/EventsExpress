import EventsExpressService from '../services/EventsExpressService';
import get_event from './event-item-view';
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
          }else{
            dispatch(setEventError(response.error));
          }
        });
    }
  }

  
export function edit_event(data) {

  return dispatch => {
    dispatch(setEventPending(true));

    const res = api_serv.setEvent(data);
    res.then(response => {
      if(response.error == null){
          
          dispatch(setEventSuccess(true));
          dispatch(get_event(data.id));
        }else{
          dispatch(setEventError(response.error));
        }
      });
  }
}

  export function setEventSuccess(data) {
    return {
      type: SET_EVENT_SUCCESS,
      payload: data
    };
  }

  export function setEventPending(data) {
    return {
      type: SET_EVENT_PENDING,
      payload: data
    };
  }

  export function setEventError(data) {
    return {
      type: SET_EVENT_ERROR,
      payload: data
    };
  }
  
