import { SubmissionError } from 'redux-form';
import { EventService } from '../services';
import get_event from './event-item-view';
import { buildValidationState } from '../components/helpers/helpers.js'
import { createBrowserHistory } from 'history';

export const SET_EVENT_SUCCESS = "SET_EVENT_SUCCESS";
export const SET_EVENT_PENDING = "SET_EVENT_PENDING";
export const SET_EVENT_ERROR = "SET_EVENT_ERROR";
export const EVENT_WAS_CREATED = "EVENT_WAS_CREATED";

const api_serv = new EventService();
const history = createBrowserHistory({ forceRefresh: true });


export default function add_event(data) {

  return dispatch => {
    dispatch(setEventPending(true));

    return api_serv.setEvent(data).then(response => {
      if (response.error == null) {
        dispatch(setEventSuccess(true));



          //return response.json().then(x => {
          //    response.text().then(x => {
          //        dispatch(eventWasCreated(x));
          //    }),
          //    dispatch(history.push(`/editEvent/${x.id}`));
          //    return Promise.resolve('success');
          //});
          return response.json().then(x => {
              dispatch(history.push(`/editEvent/${x.id}`));
              return Promise.resolve('success');
          })
          
      } else {
        throw new SubmissionError(buildValidationState(response.error));
      }
    });
  }
}

export function edit_event(data) {
  return dispatch => {
    dispatch(setEventPending(true));

    return api_serv.editEvent(data).then(response => {
      if (response.error == null) {
        dispatch(setEventSuccess(true));
        dispatch(get_event(data.id));
        return Promise.resolve('success');
      } else {
        throw new SubmissionError(buildValidationState(response.error));
      }
    });
  }
}

function eventWasCreated(eventId) {
  return {
    type: EVENT_WAS_CREATED,
    payload: eventId
  }
}

export function setEventSuccess(data) {
  return {
    type: SET_EVENT_SUCCESS,
    payload: data
  };
}

export function setEventPending(data) {
  return {
    type: SET_EVENT_PENDING,
    payload: data
  };
}

export function setEventError(data) {
  return {
    type: SET_EVENT_ERROR,
    payload: data
  };
}

