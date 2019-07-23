
import EventsExpressService from '../services/EventsExpressService';


export const SET_CATEGORIES_PENDING = "SET_CATEGORIES_PENDING";
export const GET_CATEGORIES_SUCCESS = "GET_CATEGORIES_SUCCESS";
export const SET_CATEGORIES_ERROR = "SET_CATEGORIES_ERROR";


const api_serv = new EventsExpressService();

export default function get_categories() {

    return dispatch => {
        dispatch(setCategoryPending(true));
  
      const res = api_serv.getAllCategories();
      res.then(response => {
        if(response.error == null){
            dispatch(getCategories(response));
            
          }else{
            dispatch(setCategoryError(response.error));
          }
        });
    }
  }

function setCategoryPending(data){
    return {
        type: SET_CATEGORIES_PENDING,
        payload: data
    } 
}  

function getCategories(data){
      return {
          type: GET_CATEGORIES_SUCCESS,
          payload: data
      }
  }

function setCategoryError(data){
    return{
        type: SET_CATEGORIES_ERROR,
        payload: data
    }
}