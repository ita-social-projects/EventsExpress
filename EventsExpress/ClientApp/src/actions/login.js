import { requestTOKEN, receiveTOKEN, receiveERROR } from '../constants/index';

export const actionCreators = {
    login: (email, password) => async (dispatch) => {

        console.log('reqested with: ' + email);
        dispatch({ type: requestTOKEN });

        const url = 'https://localhost:44315/api/Authentication/';
        const reqestOptions = {
            method: 'post',
            headers: new Headers({
                'Content-Type': 'application/json'
            }),
            body: JSON.stringify({ Email: email, Password: password })
        };

        const response = await fetch(url, reqestOptions);

        console.log(response);


        if (response.ok) {
            const token = await response.text();

            console.log(token);

            dispatch({ type: receiveTOKEN, payload: token });
        }
        else {

            const error = await response.text();
            console.log(error);

            dispatch({ type: receiveERROR, payload: error });
        }

    }


};