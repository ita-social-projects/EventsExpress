import EventsExpressService from '../services/EventsExpressService';
import { SET_EVENT_SUCCESS } from './add-event';


export const SET_COUNTRY_PENDING = "SET_COUNTRY_PENDING";
export const SET_COUNTRY_SUCCESS = "SET_COUNTRY_SUCCESS";
export const SET_COUNTRY_ERROR = "SET_COUNTRY_ERROR";


const api_serv = new EventsExpressService();

export default function get_countries() {

    return dispatch => {
        dispatch(setCountryPending(true));
  
      const res = api_serv.getCountries();
      res.then(response => {
        if(response.error == null){
            dispatch(getCountriesSuccess(response));
            
          }else{
            dispatch(setCountryError(response.error));
          }
        });
    }
  }

  
function setCountryPending(data){
    return {
        type: SET_COUNTRY_PENDING,
        payload: data
    } 
}  

export function setCountryError(data){
    return {
        type: SET_COUNTRY_ERROR,
        payload: data
    } 
}  


function getCountriesSuccess(data){
    return {
        type: SET_COUNTRY_SUCCESS,
        payload: data
    } 
}  