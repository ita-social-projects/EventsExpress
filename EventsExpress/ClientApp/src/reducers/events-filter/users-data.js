import { SET_USERS } from '../../actions/events/filter/users-data';

const initialState = [];

export const usersDataReducer = (state = initialState, action) => {
    switch (action.type) {
        case SET_USERS:
            return action.payload;
        default:
            return state;
    }
};
