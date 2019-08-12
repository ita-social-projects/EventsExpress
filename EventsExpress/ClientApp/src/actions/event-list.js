
import EventsExpressService from '../services/EventsExpressService';


export const SET_EVENTS_PENDING = "SET_EVENTS_PENDING";
export const GET_EVENTS_SUCCESS = "GET_EVENTS_SUCCESS";
export const SET_EVENTS_ERROR = "SET_EVENTS_ERROR";


const api_serv = new EventsExpressService();

export function get_events(filters="?page=1") {
    return dispatch => {
        dispatch(setEventPending(true));
        dispatch(setEventError(false));
      const res = api_serv.getAllEvents(filters);
      res.then(response => {
        if(response.error == null){
            dispatch(getEvents(response));
            
        } else {
            dispatch(setEventError(response.error));
          }
        });
    }
  }


export  function get_eventsForAdmin(filters = "?page=1") {
    console.log(filters);
    return dispatch => {
        dispatch(setEventPending(true));
        dispatch(setEventError(false));
        const res = api_serv.getAllEventsForAdmin(filters);
        res.then(response => {
            if (response.error == null) {
                dispatch(getEvents(response));

            } else {
                dispatch(setEventError(response.error));
            }
        });
    }
}
function setEventPending(data){
    return {
        type: SET_EVENTS_PENDING,
        payload: data
    } 
}  

function getEvents(data){
      return {
          type: GET_EVENTS_SUCCESS,
          payload: data
      }
  }

export function setEventError(data ){
    return{
        type: SET_EVENTS_ERROR,
        payload: data
    }
}