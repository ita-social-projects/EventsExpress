import initialState from '../store/initialState';

export const reducer = (state = initialState.cancel_next_eventSchedule, action) => {

    switch(action.type){
        
        case SET_CANCEL_NEXT_EVENT_PENDING:
            return {
                ...state,
                isCancelNextEventSchedulePending: action.payload
            };
        case SET_CANCEL_NEXT_EVENT_SUCCESS:
            return {
                ...state,
                isCancelNextEventSchedulePending: false,
                isCancelNextEventScheduleSuccess: action.payload
            };
        default:
            break;
    }
    return state;
};