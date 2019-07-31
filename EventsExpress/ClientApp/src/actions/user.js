import EventsExpressService from '../services/EventsExpressService';

export const blockUser = {
    PENDING: 'PENDING_BLOCK',
    SUCCESS: 'SUCCESS_BLOCK',
    ERROR: 'ERROR_BLOCK',
    UPDATE: 'UPDATE_BLOCKED'
}

export const unBlockUser = {
    PENDING: 'PENDING_UNBLOCK',
    SUCCESS: 'SUCCESS_UNBLOCK',
    ERROR: 'ERROR_UNBLOCK',
    UPDATE: 'UPDATE_UNBLOCKED'
}

const api_serv = new EventsExpressService();


export function unblock_user(id) {
    return dispatch => {
        dispatch(setUnBlockUserPending(true));

        const res = api_serv.setUserUnblock(id);

        res.then(response => {
            if (response.error == null) {
                dispatch(setUnBlockUserSuccess());
                dispatch(updateUnBlockedUser(id));
            } else {
                dispatch(setUnBlockUserError(response.error));
            }
        });
    }
}

export function block_user(id) {

    return dispatch => {
        dispatch(setBlockUserPending(true));

        const res = api_serv.setUserBlock(id);

        res.then(response => {
            if (response.error == null) {
                dispatch(setBlockUserSuccess());
                dispatch(updateBlockedUser(id));
            } else {
                dispatch(setBlockUserError(response.error));
            }
        });
    }
}


// block User actions
function setBlockUserPending(data) {
    return {
        type: blockUser.PENDING,
        payload: data
    }
}  

function setBlockUserSuccess() {
    return {
        type: blockUser.SUCCESS
    }
}

function setBlockUserError(data) {
    return {
        type: blockUser.ERROR,
        payload: data
    }
} 

function updateBlockedUser(id) {
    return {
        type: blockUser.UPDATE,
        payload: id
    }
} 

// unBlock User actions
function setUnBlockUserPending(data) {
    return {
        type: unBlockUser.PENDING,
        payload: data
    }
}

function setUnBlockUserSuccess() {
    return {
        type: unBlockUser.SUCCESS
    }
}

function setUnBlockUserError(data) {
    return {
        type: unBlockUser.ERROR,
        payload: data
    }
}

function updateUnBlockedUser(id) {
    return {
        type: unBlockUser.UPDATE,
        payload: id
    }
} 