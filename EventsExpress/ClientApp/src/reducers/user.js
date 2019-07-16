import initialState from '../store/initialState';

export const reducer = (state, action) => {
    state = state || initialState.user;
    switch(action.type)
        {
            case "ERROR":
                return { ...state, user: action.payload };
        }
    return state;
}