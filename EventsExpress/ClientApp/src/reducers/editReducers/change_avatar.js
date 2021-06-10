import { changeAvatar } from '../../actions/redactProfile/avatar-change-action';
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
        case changeAvatar.UPDATE:
            return Object.assign({}, state, {
                Update: state.Update+1
            });
            
        default:
            return state;
    }
}