import React, { Component } from 'react';
import FacebookLogin from 'react-facebook-login';
import { connect } from 'react-redux';
import config from '../config';
import { loginFacebook } from '../actions/login';
import './css/Auth.css';

class LoginFacebook extends Component {
    render() {
        const responseFacebook = (response) => {
            if (typeof response.email === 'undefined') {
                this.props.login.loginError = " Please add email to your facebook account!"
            }
            this.props.loginFacebook(response.email, response.name, response.picture.data.url);
        }

        return (
            <div>
                <FacebookLogin
                    appId={config.FACEBOOK_CLIENT_ID}
                    autoLoad={false}
                    fields="name,email,picture"
                    callback={responseFacebook}
                    cssClass="btnFacebook"
                    icon={<i className="fab fa-facebook fa-lg"></i>}
                    textButton="&nbsp;&nbsp;Log in"
                    version="3.1"
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
        loginFacebook: (email, name, picture) => dispatch(loginFacebook(email, name, picture))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(LoginFacebook);
