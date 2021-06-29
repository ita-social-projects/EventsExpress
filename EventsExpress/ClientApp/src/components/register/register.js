import React, {Component} from "react";
import DialogActions from "@material-ui/core/DialogActions";
import {Field, reduxForm} from "redux-form";
import Button from "@material-ui/core/Button";
import { minLength6, maxLength15 } from '../helpers/validators/min-max-length-validators'
import { renderTextField } from '../helpers/form-helpers';
import { emailField } from '../helpers/validators/email-field-validator';

const validate = values => {
    let errors = {};
    const requiredFields = [
        'password',
        'RepeatPassword',
    ];
    requiredFields.forEach(field => {
        if (!values[field]) {
            errors[field] = 'Required'
        }
    });

    if (values.password !== values.RepeatPassword) {
        errors.RepeatPassword = 'Passwords do not match';
    }

    if (values.newPassword !== values.repeatPassword) {
        errors.repeatPassword = 'Passwords do not match';
    }

    var emailErrors = emailField(values);
    errors = { ...errors, ...emailErrors };

    return errors;
}

class Register extends Component {

    render() {
        const {pristine, reset, submitting} = this.props;
        return (
            <div className="register">
                <form onSubmit={this.props.handleSubmit}>
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
                </form>
            </div>
        );
    }
}

Register = reduxForm({
    form: "register-form",
    validate
})(Register);

export default Register;
