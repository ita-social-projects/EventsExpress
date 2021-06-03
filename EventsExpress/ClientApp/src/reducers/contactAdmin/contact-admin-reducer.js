import { contactAdmin } from '../../actions/contactAdmin/contact-admin-add-action';
import initialState from '../../store/initialState';

export const reducer = (state = initialState.contactAdmin, action) => {
    switch (action.type) {
        case contactAdmin.PENDING:
            return { ...state, isPending: true, data: action.payload }
        case contactAdmin.SUCCESS:
            return {
                ...state,
                isPending: false,
                isSucces: true,
                data: action.payload,
            }
        default:
            return state;
    }
}