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

export const changeUserRole = {
    PENDING: 'PENDING_CHANGE_ROLE',
    SUCCESS: 'SUCCESS_CHANGE_ROLE',
    ERROR: 'ERROR_CHANGE_ROLE',
    UPDATE: 'UPDATE_CHANGE_ROLE'
}
const api_serv = new EventsExpressService();

// ACTION CREATOR FOR USER UNBLOCK:
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

// ACTION CREATOR FOR USER BLOCK:
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

// ACTION CREATOR FOR CHANGE USER ROLE:
export function change_user_role(userId, newRoleId) {
    return dispatch => {
        dispatch(setChangeUserRolePending(true));

        const res = api_serv.setChangeUserRole(userId, newRoleId);

        console.log(res);

        res.then(response => {
            if (response.error == null) {
                dispatch(setChangeUserRoleSuccess());
                dispatch(updateChangeUserRole({ userId: userId, newRoleId: newRoleId }));
            } else {
                dispatch(setChangeUserRoleError(response.error));
            }
        });
    }
}

// change role actions
function setChangeUserRolePending(data) {
    return {
        type: blockUser.PENDING,
        payload: data
    }
}  

function setChangeUserRoleSuccess() {
    return {
        type: blockUser.PENDING
    }
}  

function setChangeUserRoleError(data) {
    return {
        type: blockUser.PENDING,
        payload: data
    }
}  

function updateChangeUserRole(data) {
    return {
        type: blockUser.PENDING,
        payload: data
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