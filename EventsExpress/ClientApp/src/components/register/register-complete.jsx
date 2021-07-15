import React, {Component} from "react";
import { Field, reduxForm, getFormValues } from "redux-form";
import { connect } from 'react-redux';
import Button from "@material-ui/core/Button";
import { renderSelectField, renderPhoneInput, renderDatePicker, renderTextField} from '../helpers/form-helpers';
import moment from "moment";
import { isValidPhoneNumber } from 'react-phone-number-input';
import { isValidEmail } from '../helpers/validators/email-address-validator';
import { fieldIsRequired } from '../helpers/validators/required-fields-validator';

const validate = values => {
    let errors = {}
    const requiredFields = [
        'birthday',
        'userName',
        'email',
        'phone',
        'gender'
    ]

    if (values.phone && !isValidPhoneNumber(values.phone)) {
        errors.phone = 'Invalid phone number'
    }
    if (values.gender && values.gender > 3) {
        errors.gender = 'Invalid gender'
    }

    return {
        ...errors,
        ...fieldIsRequired(values, requiredFields),
        ...isValidEmail(values.email)
    }
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
                    <form onSubmit={handleSubmit} className="col-md-6">
                        <div className="form-group">
                            < Field
                                name="email"
                                component={renderTextField}
                                label="E-mail:"
                                type="email"
                            />
                        </div>
                        <div className="form-group">
                            < Field
                                name="userName"
                                component={renderTextField}
                                label="User name"
                            />
                        </div>
                        <div className="row">
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
                            <div className="form-group col">
                                <Field
                                    minWidth={210}
                                    name="gender"
                                    component={renderSelectField}
                                    label="Gender"
                                    parse={Number}
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

const mapStateToProps = (state) => {
    const profile = 'profile' in state.routing.location.state ? state.routing.location.state.profile : null;
    if (profile)
        return {
            initialValues: {
                email: 'email' in profile ? profile.email : null,
                userName: 'name' in profile ? profile.name : null,
                birthday: 'birthday' in profile ? profile.birthday: null,
                gender: 'gender' in profile ? profile.gender : null
            },
            form_values: getFormValues('register-complete-form')(state),
        }
    else return
}

export default connect(mapStateToProps)(reduxForm({
    form: "register-complete-form",
    validate,
    enableReinitialize: true
})(RegisterComplete));
