import NotificationTemplateService from '../services/NotificationTemplateService'
import { setErrorAllertFromResponse } from './alert-action';

export const GET_TEMPLATES_SUCCESS = 'GET_TEMPLATES_SUCCESS';
export const GET_TEMPLATE_SUCCESS = 'GET_TEMPLATE_SUCCESS';
export const GET_TEMPLATE_PROPERTIES_SUCCESS = 'GET_TEMPLATE_PROPERTIES_SUCCESS';

const api_serv = new NotificationTemplateService();

export function get_all_templates() {
    return async dispatch => {
        const response = await api_serv.getAll();

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

export function get_template_properties(template_id) {
    return async dispatch => {
        const response = await api_serv.getProperties(template_id);

        if(!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }

        const jsonRes = await response.json();
        dispatch(getProperties(jsonRes));

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

function getProperties(data) {
    return {
        type: GET_TEMPLATE_PROPERTIES_SUCCESS,
        payload: data
    }
}
