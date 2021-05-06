import { GET_CONTACTUS_SUCCESS, SET_CONTACTUS_PENDING } from '../../actions/contactUs/contact-us-issue-status-action';
import initialState from '../../store/initialState';

export const reducer = (state = initialState.contactUs, action) => {
    switch (action.type) {
        case SET_CONTACTUS_PENDING:
            return {
                ...state,
                isPending: true,
                data: action.payload
            }
        case GET_CONTACTUS_SUCCESS:

        //    let stateChangeStatus = { ...state };
          //  stateChangeStatus.data.issueStatus = action.payload.issueStatus;
            return {
         //       stateChangeEvent,
                isPending: false,
            }
        default:
            return state;
    }
}
