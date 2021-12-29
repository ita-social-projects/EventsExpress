import { UserService } from '../../../services';

export const SET_ORGANIZERS = 'SET_ORGANIZERS';

export const setOrganizers = organizers => ({
    type: SET_ORGANIZERS,
    payload: organizers
});

const userService = new UserService();

export const fetchOrganizers = filter => {
    return async dispatch => {
        const response = await userService.getSearchUsers(filter);
        if (!response.ok) {
            return;
        }

        const { items: organizers } = await response.json();
        dispatch(setOrganizers(organizers));
    };
};
