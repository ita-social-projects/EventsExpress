import { changeAvatar } from '../../actions/EditProfile/change-avatar';
import initialState from '../../store/initialState';

export const reducer = (
    state = initialState.change_avatar,
    action) => {
    switch (action.type) {
        case changeAvatar.PENDING:
            return Object.assign({}, state, {
                isPending: action.payload
            });

        case changeAvatar.SUCCESS:
            return Object.assign({}, state, {
                isSuccess: action.payload
            });

        case changeAvatar.ERROR:
            return Object.assign({}, state, {
                Error: action.payload
            });
            
        default:
            return state;
    }
}