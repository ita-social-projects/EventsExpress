export const SET_EVENTS_LAYOUT = 'SET_EVENTS_LAYOUT';

export function SetEventsLayout(data) {
    console.log("Set events layout: ", data);
    return dispatch => {
        dispatch(setLayout(data));
    }
}

export const setLayout = layout => ({
    type: SET_EVENTS_LAYOUT,
    payload: layout
});