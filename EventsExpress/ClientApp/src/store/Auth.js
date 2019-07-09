const initialState = { token: "Hello" };

export const reducer = async (state, action) => {
    state = state || initialState;
    console.log(state);
    if (action.type === "LOGIN") {

        const url = 'https://localhost:44315/api/Authentication/';
        const response = await fetch(url, {
            method: 'post',
            headers: new Headers({
                'Content-Type': 'application/json'
            }),
            body: JSON.stringify({ Email: action.payload.email, Password: action.payload.password })
        });

        const login_response = await response.text();

        console.log(login_response);
        action.payload.token = login_response;
        
        return {
            ...state, [action.payload]: action.payload
        };
    }
    return state;

};