import { EventService } from '../services';
import { setErrorAllertFromResponse } from './alert-action';
import { getRequestInc, getRequestDec } from "./request-count-action";

export const getRate = {
    PENDING: 'GET_RATE_PENDING',
    SUCCESS: 'GET_RATE_SUCCESS',
}

export const getAverageRate = {
    PENDING: 'GET_AVERAGE_RATE_PENDING',
    SUCCESS: 'GET_AVERAGE_RATE_SUCCESS',
}

export const setRate = {
    PENDING: 'SET_RATE_PENDING',
    SUCCESS: 'SET_RATE_SUCCESS',
}

const api_serv = new EventService();

export function set_rating(data) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.setRate(data);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(setRatingSuccess(jsonRes));
        dispatch(getRatingSuccess(data.rate));
        return Promise.resolve();
    }
}

export function get_currrent_rating(data) {
    return async dispatch => {
        dispatch(getRatingPending(true));

        let response = await api_serv.getCurrentRate(data)
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getRatingSuccess(jsonRes));
        return Promise.resolve();
    }
}

export function get_average_rating(data) {
    return async dispatch => {
        dispatch(getAverageRatingPending(true));

        let response = await api_serv.getAverageRate(data)
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getAverageRatingSuccess(jsonRes));
        return Promise.resolve();
    }
}



function getAverageRatingPending(data) {
    return {
        type: getAverageRate.PENDING,
        payload: data
    }
}

function getAverageRatingSuccess(data) {
    return {
        type: getAverageRate.SUCCESS,
        payload: data
    }
}

function getRatingPending(data) {
    return {
        type: getRate.PENDING,
        payload: data
    }
}

function getRatingSuccess(data) {
    return {
        type: getRate.SUCCESS,
        payload: data
    }
}

function setRatingPending(data) {
    return {
        type: setRate.PENDING,
        payload: data
    }
}

function setRatingSuccess(data) {
    return {
        type: setRate.SUCCESS,
        payload: data
    }
}

