import EventsExpressService from '../services/EventsExpressService';

export const getRate = {
    PENDING: 'GET_RATE_PENDING',
    SUCCESS: 'GET_RATE_SUCCESS',
    ERROR: 'GET_RATE_ERROR',
}

export const getAverageRate = {
    PENDING: 'GET_AVERAGE_RATE_PENDING',
    SUCCESS: 'GET_AVERAGE_RATE_SUCCESS',
    ERROR: 'GET_AVERAGE_RATE_ERROR',
}

export const setRate = {
    PENDING: 'SET_RATE_PENDING',
    SUCCESS: 'SET_RATE_SUCCESS',
    ERROR: 'SET_RATE_ERROR',
}

const api_serv = new EventsExpressService();


export function set_rating(data) {
    return dispatch => {
        dispatch(setRatingPending(true));

        const res = api_serv.setRate(data);      
        res.then(response => {
            if (response.error == null) {
                dispatch(setRatingSuccess(response));
                dispatch(getRatingSuccess(data.rate));
                
            } else {
                dispatch(setRatingError(response.error));
            }
        });
    }
}


export function get_currrent_rating(data) {
    return dispatch => {
        dispatch(getRatingPending(true));

        const res = api_serv.getCurrentRate(data);
        
        res.then(response => {
            if (response.error == null) {
                dispatch(getRatingSuccess(response));
            } else {
                dispatch(getRatingError(response.error));
            }
        });
    }
}


export function get_average_rating(data) {
    return dispatch => {
        dispatch(getAverageRatingPending(true));

        const res = api_serv.getAverageRate(data);
        
        res.then(response => {
            if (response.error == null) {
                dispatch(getAverageRatingSuccess(response));
            } else {
                dispatch(getAverageRatingError(response.error));
            }
        });
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

function getAverageRatingError(data) {
    return {
        type: getAverageRate.ERROR,
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

function getRatingError(data) {
    return {
        type: getRate.ERROR,
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

export function setRatingError(data) {
    return {
        type: setRate.ERROR,
        payload: data
    }
} 

