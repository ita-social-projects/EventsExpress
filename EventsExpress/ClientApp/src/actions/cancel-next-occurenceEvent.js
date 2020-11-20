import EventsExpressService from '../services/EventsExpressService';
import { setAlert } from './alert';
import {createBrowserHistory} from 'history';

export const SET_CANCEL_NEXT_EVENT_SUCCESS = "SET_CANCEL_NEXT_EVENT_SUCCESS";
export const SET_CANCEL_NEXT_EVENT_PENDING = "SET_CANCEL_NEXT_EVENT_PENDING";
export const SET_CANCEL_NEXT_EVENT_ERROR = "SET_CANCEL_NEXT_EVENT_ERROR";
export const EVENT_CANCEL_NEXT_WAS_CREATED = "EVENT_CANCEL_NEXT_WAS_CREATED";

const api_serv = new EventsExpressService();
const history = createBrowserHistory({forceRefresh:true});

export default function cancel_next_occurenceEvent(eventId) {

    return dispatch => {
      dispatch(setCancelNextOccurenceEventPending(true));
  
      const res = api_serv.setNextOccurenceEventCancel(eventId);
      res.then(response => {
        if(response.error == null){
            dispatch(setCancelNextOccurenceEventSuccess(true));
            response.text().then(x => { 
              dispatch(cancelNextOccurenceEventWasCreated(x));
              dispatch(setAlert({ variant: 'success', message: 'The next event was canceled!'}));
              dispatch(history.push(`/occurenceEvents`));} );
          }else{
            dispatch(setCancelNextOccurenceEventError(response.error));
          }
        });
    }
  }

function cancelNextOccurenceEventWasCreated(eventId){
  return{
    type: EVENT_CANCEL_NEXT_WAS_CREATED,
    payload: eventId
  }
}

  export function setCancelNextOccurenceEventSuccess(eventId) {
    return {
      type: SET_CANCEL_NEXT_EVENT_SUCCESS,
      payload: eventId
    };
  }

export function setCancelNextOccurenceEventPending(eventId) {
  return {
    type: SET_CANCEL_NEXT_EVENT_PENDING,
    payload: eventId
  };
}

export function setCancelNextOccurenceEventError(eventId) {
  return {
    type: SET_CANCEL_NEXT_EVENT_ERROR,
    payload: eventId
  };
}

