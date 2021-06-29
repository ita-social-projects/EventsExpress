import React, { Component } from 'react';
import { Field, reduxForm } from 'redux-form';
import DialogActions from '@material-ui/core/DialogActions';
import Button from '@material-ui/core/Button';
import GoogleLogin from '../../containers/GoogleLogin';
import LoginFacebook from '../../containers/FacebookLogin';
import TwitterLogin from '../../containers/TwitterLogin';
import ErrorMessages from '../shared/errorMessage';
import { connect } from 'react-redux';
import { Redirect } from 'react-router';
import { renderTextField } from '../helpers/form-helpers';
import { emailField } from '../helpers/validators/email-field-validator';

const validate = values => {
    let errors = {}

    if (!values.password) {
        errors.password = 'Required'
    }

    var emailErrors = emailField(values);
    errors = { ...errors, ...emailErrors };

    return errors
}

class Login extends Component {

    render() {
        const { pristine, reset, submitting, error, handleSubmit } = this.props;
        const { twitterLoginEnabled } = this.props.config;

        return (
            <div className="auth">
                <form onSubmit={handleSubmit} autoComplete="off">
                    <div>
                        <Field
                            name="email"
                            component={renderTextField}
                            label="E-mail:"
                        />
                    </div>
                    <div>
                        <Field
                            name="password"
                            component={renderTextField}
                            label="Password:"
                            type="password"
                        />
                    </div>
                    <div>
                        <DialogActions>
                            <Button fullWidth={true} type="button" color="primary" disabled={pristine || submitting} onClick={reset}>
                                CLEAR
                            </Button >
                            <Button fullWidth={true} type="submit" value="Login" color="primary">
                                Sign In
                                <Redirect to='/home' />
                            </Button>
                        </DialogActions>
                    </div>
                </form>
                <div className="d-flex justify-content-around mb-3">
                    {twitterLoginEnabled && <TwitterLogin />}
                    <LoginFacebook />
                    <GoogleLogin />
                </div>
                {error &&
                    <ErrorMessages error={error} className="text-center" />
                }
            </div>
        );
    }
}

const mapStateToProps = (state) => {
    return {
        config: state.config
    }
};

Login = reduxForm({
    form: "login-form",
    validate
})(Login);

export default connect(mapStateToProps, null)(Login);
