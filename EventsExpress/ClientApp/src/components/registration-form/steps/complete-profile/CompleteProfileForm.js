import React from 'react';
import { connect } from 'react-redux';
import { reduxForm, Field } from 'redux-form';
import moment from 'moment';
import { Grid } from '@material-ui/core';
import { isValidPhoneNumber } from 'react-phone-number-input';
import genders from '../../../../constants/GenderConstants';
import StepperNavigation from '../../StepperNavigation';
import { fieldIsRequired } from '../../../helpers/validators/required-fields-validator';
import { isValidEmail } from '../../../helpers/validators/email-address-validator';
import {
    renderDatePicker,
    renderTextField,
    renderSelectField,
    parseEuDate,
    renderPhoneInput
} from '../../../helpers/form-helpers';

const validate = values => {
    let errors = {};
    const requiredFields = [
        'birthday',
        'firstName',
        'lastName',
        'phone',
        'gender',
    ];

    if (values.firstName) {
        if (values.firstName.length < 3) {
            errors.firstName = 'First name is too short';
        } else if (values.firstName.length > 50) {
            errors.firstName = 'First name is too long';
        }
    }

    if (values.lastName) {
        if (values.lastName.length < 3) {
            errors.lastName = 'Last name is too short';
        } else if (values.lastName.length > 50) {
            errors.lastName = 'Last name is too long';
        }
    }

    if (values.phone && !isValidPhoneNumber(values.phone)) {
        errors.phone = 'Invalid phone number';
    }

    if (values.gender && values.gender > 3) {
        errors.gender = 'Invalid gender';
    }

    return {
        ...errors,
        ...fieldIsRequired(values, requiredFields),
        ...isValidEmail(values.email),
    };
};

const CompleteProfileForm = ({ handleSubmit }) => {
    return (
        <form onSubmit={handleSubmit}>
            <Grid container spacing={3}>
                <Grid item sm={12}>
                    <h1 style={{ fontSize: 20, textAlign: 'left' }}>
                        Step 2: Complete your profile
                    </h1>
                </Grid>
                {/*TODO: change avatar */}
                <Grid item sm={6}>
                    <Field
                        name="email"
                        variant="filled"
                        component={renderTextField}
                        label="Email"
                        InputProps={{
                            readOnly: true,
                        }}
                    />
                </Grid>
                <Grid item sm={6} />
                <Grid item sm={3}>
                    <Field
                        name="firstName"
                        variant="outlined"
                        component={renderTextField}
                        type="input"
                        label="First Name"
                    />
                </Grid>
                <Grid item sm={3}>
                    <Field
                        name="lastName"
                        variant="outlined"
                        component={renderTextField}
                        type="input"
                        label="Last Name"
                    />
                </Grid>
                <Grid item xs={3}>
                    <Field
                        minWidth={140}
                        name="gender"
                        component={renderSelectField}
                        label="Gender"
                        parse={Number}
                    >
                        {genders.map((value, index) => (
                            <option key={value} value={index}>{value}</option> 
                        ))}
                    </Field>
                </Grid>
                <Grid item sm={3} />
                <Grid item sm={4}>
                    <Field
                        name="birthday"
                        label="Birth Date"
                        minValue={moment().subtract(115, 'years')}
                        maxValue={moment().subtract(14, 'years')}
                        component={renderDatePicker}
                        parse={parseEuDate}
                    />
                </Grid>
                <Grid item sm={4}>
                    <Field
                        name="phone"
                        variant="outlined"
                        component={renderPhoneInput}
                        label="Phone"
                    />
                </Grid>
                <Grid item xs={4} />
                {/* TODO: add leaflet map to choose location */}
            </Grid>
            <div className="stepper-submit">
                <StepperNavigation
                    showBack={false}
                    confirmWhenSkipping={true}
                />
            </div>
        </form>
    );
};

const mapStateToProps = state => {
    const profile = state.routing.location.state.profile;
    if (!profile) {
        return {};
    }

    return {
        initialValues: {
            email: profile.email,
            firstName: profile.firstName || profile.name,
            lastName: profile.lastName,
            birthday: profile.birthday,
            gender: profile.gender || 0,
        },
    };
};

export default connect(mapStateToProps)(
    reduxForm({
        form: 'registrationForm',
        validate,
        destroyOnUnmount: false,
        forceUnregisterOnUnmount: true,
        enableReinitialize: true,
        initialValues: {
            gender: 0,
        },
})(CompleteProfileForm));
