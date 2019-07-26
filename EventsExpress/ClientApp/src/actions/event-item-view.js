
import EventsExpressService from '../services/EventsExpressService';


export const GET_EVENT_PENDING = "GET_EVENT_PENDING";
export const GET_EVENT_SUCCESS = "GET_EVENT_SUCCESS";
export const GET_EVENT_ERROR = "GET_EVENT_ERROR";


const api_serv = new EventsExpressService();

export default function get_event(id) {

    return dispatch => {
        dispatch(getEventPending(true));
  
      const res = api_serv.getEvent(id);
      res.then(response => {
        if(response.error == null){
            dispatch(getEvent(response));
            
          }else{
            dispatch(getEventError(response.error));
          }
        });
    }
  }

function getEventPending(data){
    return {
        type: GET_EVENT_PENDING,
        payload: data
    } 
}  

function getEvent(data){
      return {
          type: GET_EVENT_SUCCESS,
          payload: data
      }
  }

function getEventError(data){
    return{
        type: GET_EVENT_ERROR,
        payload: data
    }
}