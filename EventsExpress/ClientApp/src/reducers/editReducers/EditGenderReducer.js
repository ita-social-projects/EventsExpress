import { editGender } from '../../actions/editProfile/gender-edit-action';

export const reducer = (
    state = {
        isEditGenderPending: false,
        isEditGenderSuccess: false,
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

        default:
            return state;
    }
}