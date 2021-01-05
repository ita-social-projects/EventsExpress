import { LocationService } from '../services';
export const GET_CITY_PENDING = "GET_CITY_PENDING";
export const GET_CITY_SUCCESS = "GET_CITY_SUCCESS";
export const GET_CITY_ERROR = "GET_CITY_ERROR";


const api_serv = new LocationService();

export default function get_cities(country) {

    return dispatch => {
        dispatch(getCityPending(true));
  
      const res = api_serv.getCities(country);
      res.then(response => {
        if(response.error == null){
            dispatch(getCitySuccess(response));
            
          }else{
            dispatch(getCityError(response.error));
          }
        });
    }
  }

  
function getCityPending(data){
    return {
        type: GET_CITY_PENDING,
        payload: data
    } 
}  

export function getCityError(data){
    return {
        type: GET_CITY_ERROR,
        payload: data
    } 
}  


function getCitySuccess(data){
    return {
        type: GET_CITY_SUCCESS,
        payload: data
    } 
}  