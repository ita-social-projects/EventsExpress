import {
    SET_EDITGENDER_PENDING,
    SET_EDITGENDER_SUCCESS,
    SET_EDITGENDER_ERROR
} from '../../actions/EditProfile/EditGender';

export const reducer = (
    state = {
        isEditGenderPending: false,
        isEditGenderSuccess: false,
        EditGenderError: null
    },
    action) => {
    switch (action.type) {
        case SET_EDITGENDER_PENDING:
            return Object.assign({}, state, {
                isEditGendePending: action.isEditGendePending
            });

        case SET_EDITGENDER_SUCCESS:
            return Object.assign({}, state, {
                isEditGendeSuccess: action.isEditGendeSuccess
            });

        case SET_EDITGENDER_ERROR:
            return Object.assign({}, state, {
                EditGendeError: action.EditGendeError
            });

        default:
            return state;
    }
}