import { UserService } from '../../../services';

export const SET_USERS = 'SET_ORGANIZERS';

export const setUsers = organizers => ({
    type: SET_USERS,
    payload: organizers
});

const userService = new UserService();

export const fetchUsers = filter => {
    return async dispatch => {
        const response = await userService.getSearchUsers(filter);
        if (!response.ok) {
            return;
        }

        const { items: organizers } = await response.json();
        dispatch(setUsers(organizers));
    };
};
