import React, { Component } from 'react';
import { GoogleLogin } from 'react-google-login';
import { connect } from "react-redux";
import {setUser } from "../actions/login";
import config from '../config.json';
import { withRouter, Redirect } from "react-router-dom";


class LoginGoogle extends Component {

    onFailure = (error) => {
        alert(error);
    };

    googleResponse = (response) => {
        console.log(response);
        const tokenBlob = new Blob([JSON.stringify({ tokenId: response.tokenId }, null, 2)], { type: 'application/json' });
        const options = {
            method: 'POST',
            body: tokenBlob,
            mode: 'cors',
            cache: 'default'
        };
        fetch(config.GOOGLE_AUTH_CALLBACK_URL, options)
            .then(r => {
                r.json().then(user => {
                    const token = user.token;
                    console.log(token);
                    localStorage.setItem('token', token);
                    this.props.setUser(user);
                });
            })
    };
  
    render() {
      
            return (
            <div>
                       <GoogleLogin
                        clientId={config.GOOGLE_CLIENT_ID}
                        buttonText="Google Login"
                        onSuccess={this.googleResponse}
                        version="3.2" 
                /> 
            </div>
        );
    }
};

const mapStateToProps = (state) => {
    return {
       login: state.login
    }
};

const mapDispatchToProps = (dispatch) => {
    return {
        setUser: (data) => { dispatch(setUser(data)); }
    }
};

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(LoginGoogle));