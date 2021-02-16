

import NotificationTypeService from '../../services/NotificationTypeService';
export const SET_NOTIFICATION_TYPES_PENDING = "SET_NOTIFICATION_TYPES_PENDING";
export const GET_NOTIFICATION_TYPES_SUCCESS = "GET_NOTIFICATION_TYPES_SUCCESS";
export const SET_NOTIFICATION_TYPES_ERROR = "SET_NOTIFICATION_TYPES_ERROR";


const api_serv = new NotificationTypeService();

export default function get_notificationTypes() {

    return dispatch => {
        dispatch(setNotificationTypesPending(true));
  
      const res = api_serv.getAllNotificationTypes();
      res.then(response => {
        if(response.error == null){
            dispatch(getNotificationTypes(response));
            
          }else{
            dispatch(setNotificationTypesError(response.error));
          }
        });
    }
  }

function setNotificationTypesPending(data){
    return {
        type: SET_NOTIFICATION_TYPES_PENDING,
        payload: data
    } 
}  

function getNotificationTypes(data){
      return {
          type: GET_NOTIFICATION_TYPES_SUCCESS,
          payload: data
      }
  }

function setNotificationTypesError(data){
    return{
        type: SET_NOTIFICATION_TYPES_ERROR,
        payload: data
    }
}