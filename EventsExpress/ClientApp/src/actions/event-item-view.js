
import { EventService } from '../services';


export const GET_EVENT_PENDING = "GET_EVENT_PENDING";
export const GET_EVENT_SUCCESS = "GET_EVENT_SUCCESS";
export const GET_EVENT_ERROR = "GET_EVENT_ERROR";
export const RESET_EVENT = "RESET_EVENT";


export const blockEvent = {
  PENDING: 'PENDING_BLOCK',
  SUCCESS: 'SUCCESS_BLOCK',
  ERROR: 'ERROR_BLOCK',
  UPDATE: 'UPDATE_BLOCKED'
}

export const unBlockEvent = {
  PENDING: 'PENDING_UNBLOCK',
  SUCCESS: 'SUCCESS_UNBLOCK',
  ERROR: 'ERROR_UNBLOCK',
  UPDATE: 'UPDATE_UNBLOCKED'
}

export const cancelEvent = {
    PENDING: 'PENDING_CANCEL',
    SUCCESS: 'SUCCESS_CANCEL',
    ERROR: 'ERROR_CANCEL',
    UPDATE: 'UPDATE_CANCEL',
    SET_EVENT_CANCELATION_MODAL_STATUS: "TOGLE_EVENT_CANCELATION_MODAL_STATUS"
}

const api_serv = new EventService();

export default function get_event(id) {

  return dispatch => {
    dispatch(getEventPending(true));

    const res = api_serv.getEvent(id);
    res.then(response => {
      if (response.error == null) {
        dispatch(getEvent(response));

      } else {
        dispatch(getEventError(response.error));
      }
    });
  }
}

export function leave(userId, eventId) {
  return dispatch => {
    api_serv.setUserFromEvent({ userId: userId, eventId: eventId }).then(response => {
      if (response.error == null) {
        api_serv.getEvent(eventId).then(response => {
          if (response.error == null) {
            dispatch(getEvent(response));

          } else {
            dispatch(getEventError(response.error));
          }
        });
      }
    });
  }
}


export function join(userId, eventId) {
  return dispatch => {
    api_serv.setUserToEvent({ userId: userId, eventId: eventId }).then(response => {
      if (response.error == null) {
        api_serv.getEvent(eventId).then(response => {
          if (response.error == null) {
            dispatch(getEvent(response));

          } else {
            dispatch(getEventError(response.error));
          }
        });
      }
    });
  }
}

// ACTION APPROVER FOR USER
export function approveUser(userId, eventId, buttonAction) {
  return dispatch => {
    const res = api_serv.setApprovedUser({ userId: userId, eventId: eventId, buttonAction: buttonAction });
    res.then(response => {
      if (response.error == null) {

        const res1 = api_serv.getEvent(eventId);
        res1.then(response => {
          if (response.error == null) {
            dispatch(getEvent(response));

          } else {
            dispatch(getEventError(response.error));
          }
        });
      }
    });
  }
}

export function deleteFromOwners(userId, eventId) {
  return dispatch => {
    api_serv.onDeleteFromOwners({ userId: userId, eventId: eventId}).then(response =>{
      if(response.error == null)
      {
        api_serv.getEvent(eventId).then(response => {
          if(response.error == null){
            dispatch(getEvent(response));
          } else {
            dispatch(getEventError(response.error));
          }
        })
      }
    })
  }
}

export function promoteToOwner(userId, eventId) {
  return dispatch => {
    api_serv.onPromoteToOwner({ userId: userId, eventId: eventId}).then(response =>{
      if(response.error == null)
      {
        api_serv.getEvent(eventId).then(response => {
          if(response.error == null){
            dispatch(getEvent(response));
          } else {
            dispatch(getEventError(response.error));
          }
        })
      }
    })
  }
}

// ACTION CREATOR FOR EVENT UNBLOCK:
export function unblock_event(id) {
  return dispatch => {
      dispatch(setUnBlockEventPending(true));

      const res = api_serv.setEventUnblock(id);

      res.then(response => {
          if (response.error == null) {
              dispatch(setUnBlockEventSuccess());
              dispatch(updateUnBlockedEvent(id));
          } else {
              dispatch(setUnBlockEventError(response.error));
          }
      });
  }
}

// ACTION CREATOR FOR USER BLOCK:
export function block_event(id) {
  return dispatch => {
      dispatch(setBlockEventPending(true));

      const res = api_serv.setEventBlock(id);

      res.then(response => {
          if (response.error == null) {
              dispatch(setBlockEventSuccess());
              dispatch(updateBlockedEvent(id));
          } else {
              dispatch(setBlockEventError(response.error));
          }
      });
  }
}

// ACTION CREATOR FOR EVENT CANCELATION:
export function cancel_event(eventId, reason) {
    return dispatch => {
        dispatch(setCancelEventPending(true));

        const res = api_serv.setEventCancel({EventId: eventId, Reason: reason});

        res.then(response => {
            if (response.error == null) {
                dispatch(setCancelEventSuccess(eventId));
                dispatch(updateCancelEvent(eventId));
                dispatch(setEventCanelationModalStatus(false));
            } else {
                dispatch(setCancelEventError(response.error));
            }
        });
    }
}

export function resetEvent(){
  return {
    type: RESET_EVENT,
    payload: {}
  }
}

function getEventPending(data) {
  return {
    type: GET_EVENT_PENDING,
    payload: data
  }
}

function getEvent(data) {
  return {
    type: GET_EVENT_SUCCESS,
    payload: data
  }
}

export function getEventError(data) {
  return {
    type: GET_EVENT_ERROR,
    payload: data
  }
}
//block Event actions

function setBlockEventPending(data) {
  return {
      type: blockEvent.PENDING,
      payload: data
  }
}  

function setBlockEventSuccess() {
  return {
      type: blockEvent.SUCCESS
  }
}

function setBlockEventError(data) {
  return {
      type: blockEvent.ERROR,
      payload: data
  }
} 

function updateBlockedEvent(id) {
  return {
      type: blockEvent.UPDATE,
      payload: id
  }
} 

// unBlock User actions
function setUnBlockEventPending(data) {
  return {
      type: unBlockEvent.PENDING,
      payload: data
  }
}

function setUnBlockEventSuccess() {
  return {
      type: unBlockEvent.SUCCESS
  }
}

function setUnBlockEventError(data) {
  return {
      type: unBlockEvent.ERROR,
      payload: data
  }
}

function updateUnBlockedEvent(id) {
  return {
      type: unBlockEvent.UPDATE,
      payload: id
  }
} 

// cancelEvent User actions

function setCancelEventSuccess() {
    return {
        type: cancelEvent.SUCCESS        
    }
}

function setCancelEventPending(data) {
    return {
        type: cancelEvent.PENDING,
        payload: data
    }
}

function setCancelEventError(data) {
    return {
        type: cancelEvent.ERROR,
        payload: data
    }
}

function updateCancelEvent(data) {
    return {
        type: cancelEvent.UPDATE,
        payload: data
    }
}

export function setEventCanelationModalStatus(data) {
    return {
        type: cancelEvent.SET_EVENT_CANCELATION_MODAL_STATUS,
        payload: data
    }
}