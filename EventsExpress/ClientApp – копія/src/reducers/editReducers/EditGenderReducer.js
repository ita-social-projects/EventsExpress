import {editGender} from '../../actions/EditProfile/EditGender';

export const reducer = (
    state = {
        isEditGenderPending: false,
        isEditGenderSuccess: false,
        EditGenderError: null
    },
    action) => {
    switch (action.type) {
        case editGender.PENDING:
            return Object.assign({}, state, {
                isEditGendePending: action.isEditGendePending
            });

        case editGender.SUCCESS:
            return Object.assign({}, state, {
                isEditGendeSuccess: action.isEditGendeSuccess
            });

        case editGender.ERROR:
            return Object.assign({}, state, {
                EditGendeError: action.EditGendeError
            });

        default:
            return state;
    }
}