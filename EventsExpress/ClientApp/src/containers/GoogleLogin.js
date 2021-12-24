import React, { Component } from 'react';
import GoogleLogin from "../components/google-login";
import { connect } from 'react-redux';
import { loginGoogle } from '../actions/login/login-action';
import { withRouter } from 'react-router-dom';
import { setErrorAlert } from '../actions/alert-action';
import './css/Auth.css';

class LoginGoogle extends Component {

    googleResponseHandler = (response) => {
        if (typeof response.profileObj.email === 'undefined') {
            this.props.login.loginError = "Please add email to your google account!"
        }

        this.props.loginGoogle(
            response.tokenId,
            response.profileObj
        );
    }
    googleResponseOnFailure = (response) =>{
        this.props.setErrorAlert("You cannot login via google because third party cookies are blocked in your browser, you can enable it on browser settings.");
    }

    render() {
        return <GoogleLogin
                    googleClientId={this.props.config.googleClientId}
                    googleResponseHandler={this.googleResponseHandler}
                    googleResponseOnFailure={this.googleResponseOnFailure}
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
        loginGoogle: (tokenId, profile) => dispatch(loginGoogle(tokenId, profile)),
        setErrorAlert: msg => dispatch(setErrorAlert(msg)),
    }
};

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(LoginGoogle));
