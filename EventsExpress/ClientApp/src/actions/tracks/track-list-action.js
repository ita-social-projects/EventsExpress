import {TrackService} from '../../services';
import {setErrorAllertFromResponse} from "../alert-action";

export const SET_TRACKS_PENDING = "SET_TRACKS_PENDING";
export const GET_TRACKS_SUCCESS = "GET_TRACKS_SUCCESS";
export const SET_ENTITY_FILTER = "SET_ENTITY_FILTER";
export const GET_ENTITY_NAMES = "GET_ENTITY_NAMES";

const api_serv = new TrackService();

export default function getAllTracks(filter) {
    return async dispatch => {
        dispatch(setTracksPending(true));
        let response = await api_serv.getAll(filter);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getTracks(jsonRes));
        return Promise.resolve();
    }
}

export function getEntityNames() {
    return async dispatch => {
        let response = await api_serv.getEntityNames();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getNames(jsonRes));
        return Promise.resolve();
    }
}

function getNames(names) {
    return {type: GET_ENTITY_NAMES, payload: names}
}

export const setFilterEntities = (names) => {
    return {type: SET_ENTITY_FILTER, payload: names}
}

function setTracksPending(data) {
    return {type: SET_TRACKS_PENDING, payload: data}
}

function getTracks(data) {
    return {type: GET_TRACKS_SUCCESS, payload: data}
}
