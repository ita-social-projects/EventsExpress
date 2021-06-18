import React, { Component } from 'react';
import { GoogleLogin } from 'react-google-login';
import { connect } from 'react-redux';
import { googleLoginAdd } from '../../actions/redactProfile/linked-auths-add-action';
import { withRouter } from 'react-router-dom';
import { setErrorAlert } from '../../actions/alert-action';
import '../css/Auth.css';

class GoogleLoginAdd extends Component {

    responseGoogle = response => {
        if (typeof response.profileObj.email === 'undefined') {
            this.props.setErrorAlert("Please add email to your google account!")
        }
        this.props.googleLoginAdd(
            response.tokenId,
            response.profileObj.email
        );
    }

    failedGoogle = response => {
        console.log('Google auth failed')
        console.log(response);
    }

    render() {

        return (
            <div>
                <GoogleLogin
                    clientId={this.props.config.googleClientId}
                    render={renderProps => (
                        <button className="btnGoogle" onClick={renderProps.onClick} disabled={renderProps.disabled}>
                            <i className="fab fa-google blue fa-lg" />
                            <span>Log in</span>
                        </button>
                    )}
                    onSuccess={this.responseGoogle}
                    onFailure={this.failedGoogle}
                    version="3.2"
                />
            </div>
        );
    }
}

const mapStateToProps = (state) => {
    return {
        config: state.config
    }
};

const mapDispatchToProps = dispatch => ({
    googleLoginAdd: (tokenId, email) => dispatch(googleLoginAdd(tokenId, email)),
    setErrorAlert: msg => dispatch(setErrorAlert(msg)),
});

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(GoogleLoginAdd));
