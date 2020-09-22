import React, { Component } from 'react';
import { GoogleLogin } from 'react-google-login';
import { connect } from "react-redux";
import { loginGoogle } from '../actions/login';
import config from '../config.json';
import { withRouter } from "react-router-dom";

class LoginGoogle extends Component {
    render() {
        const responseGoogle = (response) => {
            if (typeof response.profileObj.email === 'undefined') {
                this.props.login.loginError = " Please add email to your google account!"
            }
            this.props.loginGoogle(
                response.tokenId,
                response.profileObj.email,
                response.profileObj.name,
                response.profileObj.imageUrl
            );
        }

        return (
            <div>
                <GoogleLogin
                    clientId={config.GOOGLE_CLIENT_ID}
                    buttonText="Google"
                    onSuccess={responseGoogle}
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
        loginGoogle: (tokenId, email, name, imageUrl) => dispatch(loginGoogle(tokenId, email, name, imageUrl))
    }
};

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(LoginGoogle));