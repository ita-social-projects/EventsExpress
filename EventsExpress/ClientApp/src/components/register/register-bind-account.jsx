import React, { Component } from "react";
import DialogActions from "@material-ui/core/DialogActions";
import { Field, reduxForm, getFormValues } from "redux-form";
import { connect } from 'react-redux';
import Button from "@material-ui/core/Button";
import { minLength6, maxLength15 } from '../helpers/validators/min-max-length-validators'
import { renderTextField } from '../helpers/form-helpers';
import { isValidEmail } from '../helpers/validators/email-address-validator';
import { fieldIsRequired } from '../helpers/validators/required-fields-validator';

const validate = values => {
    const requiredFields = [
        'password',
        'email',
        'type'
    ];

    return {
        ...fieldIsRequired(values, requiredFields),
        ...isValidEmail(values.email)
    }
}

class RegisterBindAccount extends Component {
    render() {
        const { pristine, submitting } = this.props;
        return (
            <>
                <div className="row">
                    <h5 className="m-3">Already have an account?</h5>
                </div>
                <div className="row">
                    <form onSubmit={this.props.handleSubmit} className="col-md-6">
                        <div className="form-group">
                            <Field
                                name="email"
                                component={renderTextField}
                                label="E-mail:"
                                type="email"
                            />
                        </div>
                        <div className="form-group">
                            <Field
                                name="password"
                                component={renderTextField}
                                label="Password:"
                                type="password"
                                validate={[maxLength15, minLength6]}
                            />
                        </div>
                        <div className="form-group">
                            <DialogActions>
                                <Button fullWidth={true} type="submit" color="primary" disabled={pristine || submitting}>
                                    Bind
                                </Button>
                            </DialogActions>
                        </div>
                    </form>
                </div>
            </>
        );
    }
}

const mapStateToProps = state => {
    const profile = state.routing.location.state.profile;
    const props = {
        form_values: getFormValues('register-bind-account-form')(state),
    };

    if (!profile) {
        return props;
    }

    return {
        ...props,
        initialValues: {
            type: profile.type,
        },
    };
};

export default connect(mapStateToProps)(reduxForm({
    form: 'register-bind-account-form',
    validate,
    enableReinitialize: true,
})(RegisterBindAccount));
