import {TrackService} from '../../services';
import { setErrorAllertFromResponse } from "../alert-action";
import { getRequestInc, getRequestDec } from "../request-count-action";

export const GET_TRACKS_DATA = "GET_TRACKS_DATA";
export const GET_ENTITY_NAMES = "GET_ENTITY_NAMES";
export const RESET_TRACKS = "RESET_TRACKS";

const api_serv = new TrackService();

export default function getAllTracks(filter) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.getAll(filter);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getRequestDec());
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

function getTracks(data) {
    return {type: GET_TRACKS_DATA, payload: data}
}

export function reset_tracks() {
    return {
        type: RESET_TRACKS
    }
}