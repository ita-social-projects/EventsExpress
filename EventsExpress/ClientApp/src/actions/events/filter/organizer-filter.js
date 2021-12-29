export const GET_ORGANIZERS = 'GET_ORGANIZERS';
export const SET_SELECTED_ORGANIZERS = 'SET_SELECTED_ORGANIZERS';
export const DELETE_ORGANIZER_FROM_SELECTED = 'DELETE_ORGANIZER_FROM_SELECTED ';

export const setSelectedOrganizers = value => ({
    type: SET_SELECTED_ORGANIZERS,
    payload: value
});

export const deleteOrganizerFromSelected = organizer => ({
    type: DELETE_ORGANIZER_FROM_SELECTED,
    payload: organizer
});
