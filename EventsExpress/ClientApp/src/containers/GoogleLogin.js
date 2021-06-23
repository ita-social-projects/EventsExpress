import React, { Component } from 'react';
import Login from "../components/google-login";
import { connect } from 'react-redux';
import { loginGoogle } from '../actions/login/login-action';
import { withRouter } from 'react-router-dom';
import './css/Auth.css';

class LoginGoogle extends Component {

    googleResponseHandler = (response) => {
        if (typeof response.profileObj.email === 'undefined') {
            this.props.login.loginError = "Please add email to your google account!"
        }

        this.props.loginGoogle(
            response.tokenId,
            response.profileObj.email,
            response.profileObj.name,
            response.profileObj.imageUrl
        );
    }

    render() {
        return <Login
            googleClientId={this.props.config.googleClientId}
            googleResponseHandler={this.googleResponseHandler}
        />
    }
}

const mapStateToProps = (state) => {
    return {
        login: state.login,
        config: state.config
    }
};

const mapDispatchToProps = (dispatch) => {
    return {
        loginGoogle: (tokenId, email, name, imageUrl) => dispatch(loginGoogle(tokenId, email, name, imageUrl))
    }
};

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(LoginGoogle));
