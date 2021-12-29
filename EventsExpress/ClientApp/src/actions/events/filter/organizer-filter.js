import { UserService } from '../../../services';

export const SET_ORGANIZERS = 'SET_ORGANIZERS';
export const SET_SELECTED_ORGANIZERS = 'SET_SELECTED_ORGANIZERS';
export const DELETE_ORGANIZER_FROM_SELECTED = 'DELETE_ORGANIZER_FROM_SELECTED ';

export const setOrganizers = organizers => ({
    type: SET_ORGANIZERS,
    payload: organizers
});

export const setSelectedOrganizers = organizers => ({
    type: SET_SELECTED_ORGANIZERS,
    payload: organizers
});

export const deleteOrganizerFromSelected = organizer => ({
    type: DELETE_ORGANIZER_FROM_SELECTED,
    payload: organizer
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
