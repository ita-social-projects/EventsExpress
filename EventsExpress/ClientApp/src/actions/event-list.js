
import EventsExpressService from '../services/EventsExpressService';


export const SET_EVENTS_PENDING = "SET_EVENTS_PENDING";
export const GET_EVENTS_SUCCESS = "GET_EVENTS_SUCCESS";
export const SET_EVENTS_ERROR = "SET_EVENTS_ERROR";


const api_serv = new EventsExpressService();

export default function get_events(filters="?page=1") {
    console.log(filters);
    return dispatch => {
        dispatch(setEventPending(true));

      const res = api_serv.getAllEvents(filters);
      res.then(response => {
        if(response.error == null){
            dispatch(getEvents(response));
            
          }else{
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

function setEventError(data){
    return{
        type: SET_EVENTS_ERROR,
        payload: data
    }
}