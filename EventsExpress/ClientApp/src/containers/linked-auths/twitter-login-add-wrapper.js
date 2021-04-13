import { connect } from 'react-redux';
import { twitterLoginAdd } from '../../actions/redactProfile/linked-auths-add-action';
import { setErrorAlert } from '../../actions/alert-action';
import TwitterLoginBase from './twitter-login-base';

class TwitterLoginAdd extends TwitterLoginBase {
    doWork = (accountCred, authData) => {
        if (typeof accountCred === 'undefined'
            || typeof accountCred.email === 'undefined') {
            this.props.setErrorAlert(" Please add email to your twitter account!");
            return;
        }
        ({
            email: authData.email,
            profile_image_url_https: authData.image_url,
            name: authData.name
        } = accountCred);

        authData.image_url = authData.image_url.replace(/_normal/i, '');
        this.props.twitterLoginAdd(authData.email);
    }
}

const mapDispatchToProps = (dispatch) => ({
    twitterLoginAdd: email => dispatch(twitterLoginAdd(email)),
    setErrorAlert: msg => dispatch(setErrorAlert(msg))
});

export default connect(null, mapDispatchToProps)(TwitterLoginAdd);
