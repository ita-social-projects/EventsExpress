import React, { Component } from 'react';
import { GoogleLogin } from 'react-google-login';
import { connect } from 'react-redux';
import { googleLoginAdd } from '../../actions/redactProfile/linked-auths-add-action';
import config from '../../config';
import { withRouter } from 'react-router-dom';
import { setErrorAllert } from '../../actions/alert-action';
import '../css/Auth.css';

class GoogleLoginAdd extends Component {
    googleResponseHandler = response => {
        if (typeof response.profileObj.email === 'undefined') {
            this.props.setErrorAllert("Please add email to your google account!")
        }
        this.props.googleLoginAdd(
            response.tokenId,
            response.profileObj.email
        );
    }

    render() {
        return (
            <div>
                <GoogleLogin
                    clientId={config.GOOGLE_CLIENT_ID}
                    render={renderProps => (
                        <button className="btnGoogle" onClick={renderProps.onClick} disabled={renderProps.disabled}>
                            <i className="fab fa-google blue fa-lg"></i>
                            <span>Log in</span>
                        </button>
                    )}
                    onSuccess={this.googleResponseHandler}
                    version="3.2"
                />
            </div>
        );
    }
}

const mapDispatchToProps = dispatch => ({
    googleLoginAdd: (tokenId, email) => dispatch(googleLoginAdd(tokenId, email)),
    setErrorAllert: msg => dispatch(setErrorAllert(msg))
});

export default withRouter(connect(null, mapDispatchToProps)(GoogleLoginAdd));
