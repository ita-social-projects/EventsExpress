import {editUsername} from '../../actions/editProfile/editUsername';

export const reducer = (
    state = {
        isEditUsernamePending: false,
        isEditUsernameSuccess: false,
        EditUsernameError: null
    },
    action) => {
    switch (action.type) {
        case editUsername.PENDING:
            return Object.assign({}, state, {
                isEditUsernamePending: action.isEditUsernamePending
            });

        case editUsername.SUCCESS:
            return Object.assign({}, state, {
                isEditUsernameSuccess: action.isEditUsernameSuccess
            });

        case editUsername.ERROR:
            return Object.assign({}, state, {
                EditUsernameError: action.EditUsernameError
            });

        default:
            return state;
    }
}