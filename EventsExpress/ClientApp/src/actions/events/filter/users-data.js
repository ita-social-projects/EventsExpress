import { UserService } from '../../../services';

export const SET_USERS = 'SET_ORGANIZERS';

export const setUsers = users => ({
    type: SET_USERS,
    payload: users
});

const userService = new UserService();

export const fetchUsers = filter => {
    return async dispatch => {
        const response = await userService.getSearchUsersShortInformation(filter);
        if (!response.ok) {
            return;
        }

        const users = await response.json();
        dispatch(setUsers(users));
    };
};
