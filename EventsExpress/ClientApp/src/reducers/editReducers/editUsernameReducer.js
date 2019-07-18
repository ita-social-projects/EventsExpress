import {
      SET_EDITUSERNAME_PENDING,
      SET_EDITUSERNAME_SUCCESS,
      SET_EDITUSERNAME_ERROR
} from '../../actions/EditProfile/editUsername';

export const reducer = (
    state = {
        isEditUsernamePending: false,
        isEditUsernameSuccess: false,
        EditUsernameError: null
    },
    action) => {
    switch (action.type) {
        case SET_EDITUSERNAME_PENDING:
            return Object.assign({}, state, {
                isEditUsernamePending: action.isEditUsernamePending
            });

        case SET_EDITUSERNAME_SUCCESS:
            return Object.assign({}, state, {
                isEditUsernameSuccess: action.isEditUsernameSuccess
            });

        case SET_EDITUSERNAME_ERROR:
            return Object.assign({}, state, {
                EditUsernameError: action.EditUsernameError
            });

        default:
            return state;
    }
}