import { UserService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';

export const blockUser = {
    PENDING: 'PENDING_BLOCK',
    SUCCESS: 'SUCCESS_BLOCK',
    UPDATE: 'UPDATE_BLOCKED'
}

export const unBlockUser = {
    PENDING: 'PENDING_UNBLOCK',
    SUCCESS: 'SUCCESS_UNBLOCK',
    UPDATE: 'UPDATE_UNBLOCKED'
}

export const changeUserRole = {
    SET_EDITED: 'SET_EDITED_USER',
    PENDING: 'PENDING_CHANGE_ROLE',
    SUCCESS: 'SUCCESS_CHANGE_ROLE',
    UPDATE: 'UPDATE_CHANGE_ROLE'
}
const api_serv = new UserService();

// ACTION CREATOR FOR USER UNBLOCK:
export function unblock_user(id) {
    return async dispatch => {
        dispatch(setUnBlockUserPending(true));

        let response = await api_serv.setUserUnblock(id);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(setUnBlockUserSuccess());
        dispatch(updateUnBlockedUser(id));
        return Promise.resolve();
    }
}

// ACTION CREATOR FOR USER BLOCK:
export function block_user(id) {
    return async dispatch => {
        dispatch(setBlockUserPending(true));

        let response = api_serv.setUserBlock(id);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(setBlockUserSuccess());
        dispatch(updateBlockedUser(id));
        return Promise.resolve();
    }
}



// ACTION CREATOR FOR CHANGE USER ROLE:
export function change_user_role(userId, newRole) {
    return async dispatch => {
        dispatch(setChangeUserRolePending(true));

        let response = await api_serv.setChangeUserRole(userId, newRole.id);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(setChangeUserRoleSuccess());
        dispatch(updateChangeUserRole({ userId: userId, newRole: newRole }));
        return Promise.resolve();
    }
}

export function set_edited_user(userId) {
    return dispatch => {
        dispatch(setEditedUser(userId));
    }
}

// change role actions

function setEditedUser(data){
    return {
        type: changeUserRole.SET_EDITED,
        payload: data
    }
}

function setChangeUserRolePending(data) {
    return {
        type: changeUserRole.PENDING,
        payload: data
    }
}  

function setChangeUserRoleSuccess() {
    return {
        type: changeUserRole.SUCCESS
    }
}  

function updateChangeUserRole(data) {
    return {
        type: changeUserRole.UPDATE,
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

function updateUnBlockedUser(id) {
    return {
        type: unBlockUser.UPDATE,
        payload: id
    }
} 