import React, { Component } from 'react';
import { GoogleLogin } from 'react-google-login';
import { connect } from 'react-redux';
import { loginGoogle } from '../actions/login/login-action';
import { withRouter } from 'react-router-dom';
import './css/Auth.css';

class LoginGoogle extends Component {
    render() {
        const responseGoogle = (response) => {
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


        return (
            <div>
                <GoogleLogin
                    clientId={this.props.config.keys.googleClientId}
                    render={renderProps => (
                        <button className="btnGoogle" onClick={renderProps.onClick} disabled={renderProps.disabled}>
                            <i className="fab fa-google blue fa-lg"></i>
                            <span>Log in</span>
                        </button>
                    )}
                    onSuccess={responseGoogle}
                    version="3.2"
                />
            </div>
        );
    }
};

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