import PhotoService from '../services/PhotoService';
import { setErrorAllertFromResponse } from './alert-action';

export const getEventPreviewPhoto = {
    PENDING: 'GET_EVENT_PREVIEW_PHOTO_PENDING',
    SUCCESS: 'GET_EVENT_PREVIEW_PHOTO_SUCCESS',
}

export const getEventFullPhoto = {
    PENDING: 'GET_EVENT_FULL_PHOTO_PENDING',
    SUCCESS: 'GET_EVENT_FULL_PHOTO_SUCCESS',
}

export const getUserPhoto = {
    PENDING: 'GET_USER_PHOTO_PENDING',
    SUCCESS: 'GET_USER_PHOTO_SUCCESS',
}

const photoService = new PhotoService();

export function get_event_preview_photo(id) {
    return async dispatch => {
        dispatch(getEventPreviewPhotoPending(true));
        let response = await photoService.getPreviewEventPhoto(id);
        if (!response.ok) {
            dispatch(getEventPreviewPhotoSuccess(null));
            return Promise.reject();
        }
        let blobRes = await response.blob();
        dispatch(getEventPreviewPhotoSuccess(blobRes));
        return Promise.resolve();
    }
}

export function get_event_full_photo(data) {
    return async dispatch => {
        dispatch(getPhotoPending(true));

        let response = await api_serv.getCurrentRate(data)
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getPhotoSuccess(jsonRes));
        return Promise.resolve();
    }
}

export function get_user_photo(data) {
    return async dispatch => {
        dispatch(getAveragePhotoPending(true));

        let response = await api_serv.getAverageRate(data)
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getAveragePhotoSuccess(jsonRes));
        return Promise.resolve();
    }
}



function getEventPreviewPhotoPending(data) {
    return {
        type: getEventPreviewPhoto.PENDING,
        payload: data
    }
}

function getEventPreviewPhotoSuccess(data) {
    return {
        type: getEventPreviewPhoto.SUCCESS,
        payload: data
    }
}

function getEventFullPhotoPending(data) {
    return {
        type: getEventFullPhoto.PENDING,
        payload: data
    }
}

function getEventFullPhotoSuccess(data) {
    return {
        type: getEventFullPhoto.SUCCESS,
        payload: data
    }
}

function getUserPhotoPending(data) {
    return {
        type: getUserPhoto.PENDING,
        payload: data
    }
}

function getUserPhotoSuccess(data) {
    return {
        type: getUserPhoto.SUCCESS,
        payload: data
    }
}

