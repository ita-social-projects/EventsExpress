import EventsExpressService from '../services/EventsExpressService';

export const SET_COPY_EVENT_SUCCESS = "SET_COPY_EVENT_SUCCESS";
export const SET_COPY_EVENT_PENDING = "SET_COPY_EVENT_PENDING";
export const SET_COPY_EVENT_ERROR = "SET_COPY_EVENT_ERROR";
export const EVENT_COPY_WAS_CREATED = "EVENT_COPY_WAS_CREATED";

const api_serv = new EventsExpressService();

export default function add_copy_event(eventId) {

    return dispatch => {
      dispatch(setCopyEventPending(true));
  
      const res = api_serv.setCopyEvent(eventId);
      res.then(response => {
        if(response.error == null){
            dispatch(setCopyEventSuccess(true));
            response.text().then(x => { dispatch(copyEventWasCreated(x));} );
          }else{
            dispatch(setCopyEventError(response.error));
          }
        });
    }
  }

function copyEventWasCreated(eventId){
  return{
    type: EVENT_COPY_WAS_CREATED,
    payload: eventId
  }
}

  export function setCopyEventSuccess(eventId) {
    return {
      type: SET_COPY_EVENT_SUCCESS,
      payload: eventId
    };
  }

export function setCopyEventPending(eventId) {
  return {
    type: SET_COPY_EVENT_PENDING,
    payload: eventId
  };
}

export function setCopyEventError(eventId) {
  return {
    type: SET_COPY_EVENT_ERROR,
    payload: eventId
  };
}

