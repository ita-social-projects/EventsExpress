import { connect } from 'react-redux';
import { twitterLoginAdd } from '../../actions/editProfile/linked-auths-add-action';
import { setErrorAllert } from '../../actions/alert-action';
import TwitterLoginBase from './twitter-login-base';

class TwitterLoginAdd extends TwitterLoginBase {
    doWork = (accountCred, authData) => {
        if (typeof accountCred === 'undefined'
            || typeof accountCred.email === 'undefined') {
            this.props.setErrorAllert(" Please add email to your twitter account!");
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
    setErrorAllert: msg => dispatch(setErrorAllert(msg))
});

export default connect(null, mapDispatchToProps)(TwitterLoginAdd);
