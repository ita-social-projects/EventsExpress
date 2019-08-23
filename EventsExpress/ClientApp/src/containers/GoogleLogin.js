import React, { Component } from 'react';
import { GoogleLogin } from 'react-google-login';
import { connect } from "react-redux";
import {setUser } from "../actions/login";
import config from '../config.json';
import { withRouter } from "react-router-dom";
import { initialConnection } from '../actions/chat';
import { getUnreadMessages } from '../actions/chats';

class LoginGoogle extends Component {

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
                    this.props.getUnreadMessages(user.id);
                    this.props.initialConnection();
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
}

const mapStateToProps = (state) => {
    return {
       login: state.login
    }
};

const mapDispatchToProps = (dispatch) => {
    return {
        setUser: (data) => { dispatch(setUser(data)); },
        initialConnection: () => { dispatch(initialConnection()); },
        getUnreadMessages: (data) => { dispatch(getUnreadMessages(data));}
    }
};

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(LoginGoogle));