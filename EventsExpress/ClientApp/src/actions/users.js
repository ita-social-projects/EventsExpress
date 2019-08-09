
import EventsExpressService from '../services/EventsExpressService';


export const GET_USERS_PENDING = "GET_USERS_PENDING";
export const GET_USERS_SUCCESS = "GET_USERS_SUCCESS";
export const GET_USERS_ERROR = "GET_USERS_ERROR";


const api_serv = new EventsExpressService();

export default function get_users(filters) {
    console.log(filters);
    return dispatch => {
        dispatch(getUsersPending(true));
        dispatch(getUsersError(false));
        const res = api_serv.getUsers(filters);
      res.then(response => {
        if(response.error == null){
            dispatch(getUsers(response));
            
          }else{
            dispatch(getUsersError(response.error));
          }
        });
    }
  }

function getUsersPending(data){
    return {
        type: GET_USERS_PENDING,
        payload: data
    } 
}  

function getUsers(data){
      return {
          type: GET_USERS_SUCCESS,
          payload: data
      }
  }

function getUsersError(data){
    return{
        type: GET_USERS_ERROR,
        payload: data
    }
}