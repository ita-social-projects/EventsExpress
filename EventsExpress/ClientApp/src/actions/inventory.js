import EventsExpressService from '../services/EventsExpressService';

export const SET_INVENTAR_SUCCESS = "SET_INVENTAR_SUCCESS";
export const SET_INVENTAR_PENDING = "SET_INVENTAR_PENDING";
export const SET_INVENTAR_ERROR = "SET_INVENTAR_ERROR";
export const INVENTAR_WAS_CREATED = "INVENTAR_WAS_CREATED";

const api_serv = new EventsExpressService();

export default function add_inventar(data) {

    return dispatch => {
      dispatch(setInventarPending(true));
  
      const res = api_serv.setInventarToEvent(data);
      res.then(response => {
        if(response.error == null){
            dispatch(setInventarSuccess(true));
            response.text().then(x => { dispatch(inventarWasCreated(x));} );
          }else{
            dispatch(setInventarError(response.error));
          }
        });
    }
}

export function get_unitofmeasuring() {
    return api_serv.getUnitsOfMeasuring();
}

function inventarWasCreated(eventId){
    return{
      type: INVENTAR_WAS_CREATED,
      payload: eventId
    }
}
  
export function setInventarSuccess(data) {
    return {
      type: SET_INVENTAR_SUCCESS,
      payload: data
    };
}
  
export function setInventarPending(data) {
    return {
      type: SET_INVENTAR_PENDING,
      payload: data
    };
}
  
export function setInventarError(data) {
    return {
      type: SET_INVENTAR_ERROR,
      payload: data
    };
}
  