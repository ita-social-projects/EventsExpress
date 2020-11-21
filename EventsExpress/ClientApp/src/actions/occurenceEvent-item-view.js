
import EventsExpressService from '../services/EventsExpressService';


export const GET_OCCURENCE_EVENT_PENDING = "GET_OCCURENCE_EVENT_PENDING";
export const GET_OCCURENCE_EVENT_SUCCESS = "GET_OCCURENCE_EVENT_SUCCESS";
export const GET_OCCURENCE_EVENT_ERROR = "GET_OCCURENCE_EVENT_ERROR";
export const RESET_OCCURENCE_EVENT = "RESET_OCCURENCE_EVENT";

const api_serv = new EventsExpressService();

export default function getOccurenceEvent(id) {

  return dispatch => {
    dispatch(getOccurenceEventPending(true));

    const res = api_serv.getOccurenceEvent(id);
    res.then(response => {
      if (response.error == null) {
        dispatch(get_OccurenceEvent(response));
      } else {
        dispatch(getOccurenceEventError(response.error));
      }
    });
  }
}

export function resetOccurenceEvent(){
  return {
    type: RESET_OCCURENCE_EVENT,
    payload: {}
  }
}

function getOccurenceEventPending(data) {
  return {
    type: GET_OCCURENCE_EVENT_PENDING,
    payload: data
  }
}

function get_OccurenceEvent(data) {
  return {
    type: GET_OCCURENCE_EVENT_SUCCESS,
    payload: data
  }
}

export function getOccurenceEventError(data) {
  return {
    type: GET_OCCURENCE_EVENT_ERROR,
    payload: data
  }
}
