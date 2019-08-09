
import EventsExpressService from '../services/EventsExpressService';


export const GET_EVENT_PENDING = "GET_EVENT_PENDING";
export const GET_EVENT_SUCCESS = "GET_EVENT_SUCCESS";
export const GET_EVENT_ERROR = "GET_EVENT_ERROR";
export const RESET_EVENT = "RESET_EVENT";


const api_serv = new EventsExpressService();

export default function get_event(id) {

  return dispatch => {
    dispatch(getEventPending(true));

    const res = api_serv.getEvent(id);
    res.then(response => {
      if (response.error == null) {
        dispatch(getEvent(response));

      } else {
        dispatch(getEventError(response.error));
      }
    });
  }
}

export function leave(userId, eventId) {
  return dispatch => {
    const res = api_serv.setUserFromEvent({ userId: userId, eventId: eventId });
    res.then(response => {
      if (response.error == null) {

        const res1 = api_serv.getEvent(eventId);
        res1.then(response => {
          if (response.error == null) {
            dispatch(getEvent(response));

          } else {
            dispatch(getEventError(response.error));
          }
        });
      }
    });
  }
}


export function join(userId, eventId) {
  return dispatch => {
    const res = api_serv.setUserToEvent({ userId: userId, eventId: eventId });
    res.then(response => {
      if (response.error == null) {

        const res1 = api_serv.getEvent(eventId);
        res1.then(response => {
          if (response.error == null) {
            dispatch(getEvent(response));

          } else {
            dispatch(getEventError(response.error));
          }
        });
      }
    });
  }
}

export function resetEvent(){
  return {
    type: RESET_EVENT,
    payload: {}
  }
}

function getEventPending(data) {
  return {
    type: GET_EVENT_PENDING,
    payload: data
  }
}

function getEvent(data) {
  return {
    type: GET_EVENT_SUCCESS,
    payload: data
  }
}

export function getEventError(data) {
  return {
    type: GET_EVENT_ERROR,
    payload: data
  }
}