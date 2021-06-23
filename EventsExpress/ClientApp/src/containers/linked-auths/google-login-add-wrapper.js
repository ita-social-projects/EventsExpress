import React, { Component } from 'react';
import GoogleLogin from '../../components/google-login';
import { connect } from 'react-redux';
import { googleLoginAdd } from '../../actions/redactProfile/linked-auths-add-action';
import { withRouter } from 'react-router-dom';
import { setErrorAlert } from '../../actions/alert-action';
import '../css/Auth.css';

class GoogleLoginAdd extends Component {

    googleResponseHandler = response => {
        if (typeof response.profileObj.email === 'undefined') {
            this.props.setErrorAlert("Please add email to your google account!")
        }

        this.props.googleLoginAdd(
            response.tokenId,
            response.profileObj.email
        );
    }

    render() {
        return <GoogleLogin
            googleClientId={this.props.config.googleClientId}
            googleResponseHandler={this.googleResponseHandler}
        />
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
