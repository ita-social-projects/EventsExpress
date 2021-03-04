import { TrackService } from '../../services';


export const SET_TRACKS_PENDING = "SET_TRACKS_PENDING";
export const GET_TRACKS_SUCCESS = "GET_TRACKS_SUCCESS";
export const SET_TRACKS_ERROR = "SET_TRACKS_ERROR";
export const SET_ENTITY_FILTER = "SET_ENTITY_FILTER";


const api_serv = new TrackService();

export default function getAllTracks(filter) {
    return dispatch => {
        dispatch(setTracksPending(true));
        const res = api_serv.getAll(filter);
        res.then(response => {
        if(response.error == null){
            dispatch(getTracks(response));
            
          }else{
            dispatch(setTracksError(response.error));
          }
        });
    }
  }
export const setFilterEntities = (names) => {
  return { type: SET_ENTITY_FILTER, payload: names }
}

function setTracksPending(data){
    return {
        type: SET_TRACKS_PENDING,
        payload: data
    } 
}  

function getTracks(data){
      return {
          type: GET_TRACKS_SUCCESS,
          payload: data
      }
  }

function setTracksError(data){
    return{
        type: SET_TRACKS_ERROR,
        payload: data
    }
}