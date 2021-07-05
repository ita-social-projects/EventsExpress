import initialState from '../store/initialState';

export const reducer = (state = initialState.add_copy_event, action) => {

    switch (action.type) {
        case SET_COPY_EVENT_PENDING:
            return {
                ...state,
                isCopyEventPending: action.payload
            };
        case SET_COPY_EVENT_SUCCESS:
            return {
                ...state,
                isCopyEventPending: false,
                isCopyEventSuccess: action.payload
            };
        default:
            break;
    }
    return state;
};