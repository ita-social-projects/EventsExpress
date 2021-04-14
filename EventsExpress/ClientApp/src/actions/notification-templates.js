import NotificationTemplateService from '../services/NotificationTemplateService'
import { setErrorAllertFromResponse } from './alert-action';

export const GET_TEMPLATES_SUCCESS = 'GET_TEMPLATES_SUCCESS';
export const GET_TEMPLATE_SUCCESS = 'GET_TEMPLATE_SUCCESS';
export const SET_TEMPLATE_SUCCESS = 'SET_TEMPLATE_SUCCESS';

const api_serv = new NotificationTemplateService();

export function get_all_templates(pageNumber, pageSize) {
    return async dispatch => {
        const response = await api_serv.getAll(pageNumber, pageSize);
        if(!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        const jsonRes = await response.json();
        dispatch(getTemplates(jsonRes));
        return Promise.resolve();
    }
}

export function get_template(id) {
    return async dispatch => {
        const response = await api_serv.getByIdAsync(id);
        if(!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        const jsonRes = await response.json();
        dispatch(getTemplate(jsonRes));
        return Promise.resolve();
    }
}

export function update_template(template) {
    return async dispatch => {
        const response = await api_serv.updateAsync(template);
        if(!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        const jsonRes = await response.json();
        dispatch(setTemplate(jsonRes));
        return Promise.resolve();
    }
}

function getTemplates(data) {
    return {
        type: GET_TEMPLATES_SUCCESS,
        payload: data
    }
}

function getTemplate(data) {
    return {
        type: GET_TEMPLATE_SUCCESS,
        payload: data
    }
}

function setTemplate(data) {
    return {
        type: SET_TEMPLATE_SUCCESS,
        payload: data
    }
}
