import React, { Component } from 'react';
import FacebookLogin from 'react-facebook-login';
import { connect } from "react-redux";
import { loginFacebook } from '../actions/login';
import './css/Auth.css'
class LoginFacebook extends Component {
   
    render() {
        
        const responseFacebook = (response) => {
            if (typeof response.email === 'undefined') {
                this.props.login.loginError=" Please add email to your facebook account!"
            }
            this.props.loginFacebook(response.email, response.name)
            console.log(response);
        }
        let { loginError } = this.props.login;
        return (


            <div>
                <FacebookLogin
                    appId="418450132121252"
                    fields="name,email,picture"
                    callback={responseFacebook}
                    cssClass="btnFacebook"
                    icon={<img src="https://img.icons8.com/ios/24/000000/facebook-f.png"/>}
                    version="3.1"
                    textButton="&nbsp;&nbsp;Facebook"/>
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
                loginFacebook: (email, name) => dispatch(loginFacebook(email, name))
            }
        };

export default connect(mapStateToProps, mapDispatchToProps)(LoginFacebook);