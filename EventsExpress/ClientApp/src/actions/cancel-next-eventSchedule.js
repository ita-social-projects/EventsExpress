import { EventScheduleService } from '../services';
import { setAlert } from './alert-action';
import {createBrowserHistory} from 'history';

export const SET_CANCEL_NEXT_EVENT_SUCCESS = "SET_CANCEL_NEXT_EVENT_SUCCESS";
export const SET_CANCEL_NEXT_EVENT_PENDING = "SET_CANCEL_NEXT_EVENT_PENDING";
export const SET_CANCEL_NEXT_EVENT_ERROR = "SET_CANCEL_NEXT_EVENT_ERROR";
export const EVENT_CANCEL_NEXT_WAS_CREATED = "EVENT_CANCEL_NEXT_WAS_CREATED";

const api_serv = new EventScheduleService();
const history = createBrowserHistory({forceRefresh:true});

export default function cancel_next_eventSchedule(eventId) {

    return dispatch => {
      dispatch(setCancelNextEventSchedulePending(true));
  
      const res = api_serv.setNextEventScheduleCancel(eventId);
      res.then(response => {
        if(response.error == null){
            dispatch(setCancelNextEventScheduleSuccess(true));
            response.text().then(x => { 
              dispatch(cancelNextEventScheduleWasCreated(x));
              dispatch(setAlert({ variant: 'success', message: 'The next event was canceled!'}));
                dispatch(history.push(`/eventSchedules`));} );
          }else{
            dispatch(setCancelNextEventScheduleError(response.error));
          }
        });
    }
  }

function cancelNextEventScheduleWasCreated(eventId){
  return{
    type: EVENT_CANCEL_NEXT_WAS_CREATED,
    payload: eventId
  }
}

  export function setCancelNextEventScheduleSuccess(eventId) {
    return {
      type: SET_CANCEL_NEXT_EVENT_SUCCESS,
      payload: eventId
    };
  }

export function setCancelNextEventSchedulePending(eventId) {
  return {
    type: SET_CANCEL_NEXT_EVENT_PENDING,
    payload: eventId
  };
}

export function setCancelNextEventScheduleError(eventId) {
  return {
    type: SET_CANCEL_NEXT_EVENT_ERROR,
    payload: eventId
  };
}

