import { connect } from 'react-redux';
import { loginTwitter } from '../actions/login/login-action';
import TwitterLoginBase from './linked-auths/twitter-login-base';

class LoginTwitter extends TwitterLoginBase {
    doWork = (accountCred, authData) => {
        if (typeof accountCred === 'undefined'
            || typeof accountCred.email === 'undefined') {
            this.props.login.loginError = " Please add email to your twitter account!";
            return;
        }
        ({
            email: authData.email,
            profile_image_url_https: authData.image_url,
            name: authData.name
        } = accountCred);

        authData.image_url = authData.image_url.replace(/_normal/i, '');
        this.props.loginTwitter(authData);
    }
};

const mapStateToProps = (state) => ({
    login: state.login
});

const mapDispatchToProps = (dispatch) => ({
    loginTwitter: (authData) => dispatch(loginTwitter(authData))
});

export default connect(mapStateToProps, mapDispatchToProps)(LoginTwitter);
