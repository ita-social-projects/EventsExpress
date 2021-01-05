
import UnitOfMeasuringService from '../../services/UnitOfMeasuringService';
const api_serv = new UnitOfMeasuringService();

export const SET_UNITS_OF_MEASURING_PENDING="SET_UNITS_OF_MEASURING_PENDING";
export const GET_UNITS_OF_MEASURING_SUCCESS="GET_UNITS_OF_MEASURING_SUCCESS";
export const SET_UNITS_OF_MEASURING_ERROR="SET_UNITS_OF_MEASURING_ERROR";

export default function get_unitsOfMeasuring() {
    // return dispatch => {
    //     const res = api_serv.getUnitsOfMeasuring();
    //     res.then(response => {
    //         dispatch(getUnitsOfMeasuring(response))
    //     });
    // }
    return dispatch => {
        dispatch(setUnitOfMeasuringPending(true));
  
      const res = api_serv.getUnitsOfMeasuring();
      res.then(response => {
        if(response.error == null){
            dispatch(getUnitsOfMeasuring(response));
            
          }else{
            dispatch(setUnitOfMeasuringError(response.error));
          }
        });
    }
}

  function setUnitOfMeasuringPending(data){
      return {
          type: SET_UNITS_OF_MEASURING_PENDING,
          payload: data
      } 
  }  
  
  function getUnitsOfMeasuring(data){
        return {
            type: GET_UNITS_OF_MEASURING_SUCCESS,
            payload: data
        }
    }
  
  function setUnitOfMeasuringError(data){
      return{
          type: SET_UNITS_OF_MEASURING_ERROR,
          payload: data
      }
  }