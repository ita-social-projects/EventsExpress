import React, { Component } from "react";
import { Field, reduxForm } from "redux-form";
import Button from "@material-ui/core/Button";
import { renderTextField, renderDatePicker } from '../helpers/helpers';
import { renderSelectField, renderPhoneInput } from '../helpers/form-helpers';
import moment from "moment";
import { isValidPhoneNumber } from 'react-phone-number-input'

const validate = values => {
    const errors = {}
    const requiredFields = [
        'email',
        'birthday',
        'userName',
        'phone',
        'gender'
    ]
    requiredFields.forEach(field => {
        if (!values[field]) {
            errors[field] = 'Required'
        }
    })
    if (values.email && !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.email)) {
        errors.email = 'Invalid email address'
    }
    if (values.phone && !isValidPhoneNumber(values.phone)) {
        errors.phone = 'Invalid phone number'
    }
    if (values.gender && values.gender >= 3) {
        errors.gender = 'Invalid gender'
    }
    return errors
}

class RegisterComplete extends Component {

    render() {
        const { pristine, submitting, handleSubmit } = this.props;
        return (
            <>
                <div className="row">
                    <h5 className="m-3">Please, complete your registration</h5>
                </div>
                <div className="row">
                    <form onSubmit={handleSubmit} class="col-md-6">
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
                                name="userName"
                                component={renderTextField}
                                label="User name"
                            />
                        </div>
                        <div class="row">
                            <div className="form-group col">
                                <Field
                                    name="birthday"
                                    id="date"
                                    label="Birthday"
                                    minValue={moment(new Date()).subtract(115, 'years')}
                                    maxValue={moment(new Date()).subtract(15, 'years')}
                                    component={renderDatePicker}
                                />
                            </div>
                            <div className="form-group col" >
                                <Field
                                    name="gender"
                                    component={renderSelectField}
                                    label="Gender"
                                    parse = {Number}
                                >
                                    <option aria-label="None" value={0} />
                                    <option value={1}>Male</option>
                                    <option value={2}>Female</option>
                                    <option value={3}>Other</option>
                                </Field>
                            </div>
                        </div>
                        <div className="form-group">
                            <Field
                                component={renderPhoneInput}
                                name='phone'
                                label='Phone'
                            />
                        </div>
                        <div className="form-group">
                            <Button fullWidth={true} type="submit" color="primary" disabled={pristine || submitting}>
                                Complete
                            </Button>
                        </div>
                    </form>
                </div>
            </>
        );
    }
}

RegisterComplete = reduxForm({
    form: "register-complete-form",
    validate
})(RegisterComplete);

export default RegisterComplete;
