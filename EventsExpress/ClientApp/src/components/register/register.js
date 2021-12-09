import React, {Component} from "react";
import DialogActions from "@material-ui/core/DialogActions";
import {Field, reduxForm} from "redux-form";
import Button from "@material-ui/core/Button";
import { minLength6, maxLength15 } from '../helpers/validators/min-max-length-validators'
import { renderTextField } from '../helpers/form-helpers';
import { isValidEmail } from '../helpers/validators/email-address-validator';
import { fieldIsRequired } from '../helpers/validators/required-fields-validator';

const validate = values => {
    let errors = {};
    const requiredFields = [
        'password',
        'email',
        'RepeatPassword',
    ];

    if (values.password !== values.RepeatPassword) {
        errors.RepeatPassword = 'Passwords do not match';
    }

    if (values.newPassword !== values.repeatPassword) {
        errors.repeatPassword = 'Passwords do not match';
    }

    return {
        ...errors,
        ...fieldIsRequired(values, requiredFields),
        ...isValidEmail(values.email)
    }
}

class Register extends Component {

    render() {
        const {pristine, reset, submitting, error} = this.props;

        return (
            <div className="register">
                <form onSubmit={this.props.handleSubmit(this.props.onSubmit)}>
                    <div>
                        <Field
                            name="email"
                            component={renderTextField}
                            label="E-mail:"
                            type="email"
                        />
                    </div>
                    <div>
                        <Field
                            name="password"
                            component={renderTextField}
                            label="Password:"
                            type="password"
                            validate={[maxLength15, minLength6]}
                        />
                    </div>
                    <div>
                        <Field
                            name="RepeatPassword"
                            component={renderTextField}
                            label="Repeat password:"
                            type="password"
                            validate={[maxLength15, minLength6]}
                        />
                    </div>
                    <div>
                        <DialogActions>
                            <Button
                                fullWidth={true}
                                type="button" color="primary"
                                disabled={pristine || submitting}
                                onClick={reset}
                            >
                                CLEAR
                            </Button>
                            <Button fullWidth={true} type="submit" color="primary">
                                Sign Up
                            </Button>
                        </DialogActions>
                    </div>
                    {error && <p className="text-danger text-center">{error}</p>}
                </form>
            </div>
        );
    }
}

export default reduxForm({
    form: "register-form",
    validate
})(Register);
