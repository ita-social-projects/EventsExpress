import React from 'react';
import { Router, Route } from 'react-router-dom';
import { connect } from 'react-redux';

import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import Header from '../shared/Header';

export const actionCreators = {
    login: ( email, password ) => ({ type: "LOGIN", payload: { email, password} })
}


//        (dispatch, getState) => {
//        console.log("LOGIN");
//        dispatch({ type: "LOGIN",  login});
//        const url = 'https://localhost:44315/api/Authentication/';
//        const response = fetch(url, {
//            method: 'post',
//            headers: new Headers({
//                'Content-Type': 'application/json'
//            }),
//            body: JSON.stringify({ Email: getState.login, Password: getState.password })
//        });
//        const login_response = response.json();

//        dispatch({ type: "LOGIN_RESPONSE", login, login_response });
//    },
//};


export const reducer = (state, action) => {

    console.log("entry");
    if (action.type === "LOGIN") {

        const url = 'https://localhost:44315/api/Authentication/';
        const response = fetch(url, {
            method: 'post',
            headers: new Headers({
                'Content-Type': 'application/json'
            }),
            body: JSON.stringify({ Email: action.payload.email, Password: action.payload.password })
        });
        const login_response = response.json();

        console.log(login_response);
        return { ...state, login: action.login };
    }
    
};


export default class LoginForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            login: "",
            password: "",
            token: ""
        }
    }



    updateLoginValue = (event) => {
        this.setState({ login: event.target.value });
        
    }

    updatePasswordValue = (event) => {
        this.setState({ password: event.target.value });
        
    }

    sendMyData = (event) => {

        event.preventDefault();
        fetch('https://localhost:44315/api/Authentication/', {
            method: 'post',
            headers: new Headers({
                'Content-Type': 'application/json'
            }),
            body: JSON.stringify({ Email: this.state.login, Password: this.state.password })
        })
            .then((response) => response.json())
            .then((responseJson) => {
                this.setState({ token: responseJson });
            })
            .catch((error) => {
                console.error(error);
            });
    }

    

    render = () =>

        <div className="signup-form">
            <form method='post'>
                <h2>Login</h2>
                <hr />
                <div className="form-group">
                    <div className="input-group">
                        <span className="input-group-addon"><i class="fa fa-paper-plane"></i></span>
                        <input type="email" value={this.state.login} onChange={this.updateLoginValue} className="form-control" name="email" placeholder="Email Address" required="required" />
			        </div>
                </div>
                <div className="form-group">
                    <div className="input-group">
                        <span className="input-group-addon"><i class="fa fa-lock"></i></span>
                        <input type="password" value={this.state.password} onChange={this.updatePasswordValue} className="form-control" name="password" placeholder="Password" required="required" />
			        </div>
                </div>
                <LinkContainer to={'/account/register'} exact>
                    <NavItem>
                        <Glyphicon /> Register
                    </NavItem>
                </LinkContainer>
                <div className="form-group">
                    <button onClick={actionCreators["LOGIN"]} className="btn btn-primary btn-lg">Login</button>
                </div>
            </form>

            <button onClick={this.reducer(null, actionCreators.login)}>Send</button>
        </div>



}