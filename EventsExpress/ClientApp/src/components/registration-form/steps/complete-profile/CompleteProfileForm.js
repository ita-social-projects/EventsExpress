import React, { useContext } from 'react';
import { Grid } from '@material-ui/core';
import { reduxForm, Field } from 'redux-form';
import moment from 'moment';
import genders from '../../../../constants/GenderConstants';
import { RegisterStepContext } from '../../RegistrationForm';
import StepperNavigation from '../../StepperNavigation';
import {
    renderDatePicker,
    renderTextField,
    renderSelectField,
    parseEuDate
} from '../../../helpers/form-helpers';

const CompleteProfileForm = () => {
    const { goToNext } = useContext(RegisterStepContext);

    return (
        <form onSubmit={goToNext}>
            <Grid container spacing={3}>
                <Grid item sm={12}>
                    <h1 style={{ fontSize: 20, textAlign: 'left' }}>
                        Step 2: Complete your profile
                    </h1>
                </Grid>
                {/*TODO: change avatar */}
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
                <Grid item sm={2} />
                <Grid item sm={4}>
                    <Field
                        name="birthDate"
                        label="Birth Date"
                        minValue={moment().subtract(115, 'years')}
                        maxValue={moment().subtract(14, 'years')}
                        component={renderDatePicker}
                        parse={parseEuDate}
                    />
                    {/* TODO: look how to reuse component from user profile */}
                </Grid>
                <Grid item xs={3}>
                    <Field
                        name="country"
                        variant="outlined"
                        component={renderTextField}
                        type="input"
                        label="Country"
                    />
                </Grid>
                <Grid item xs={3}>
                    <Field
                        name="city"
                        variant="outlined"
                        component={renderTextField}
                        type="input"
                        label="City"
                    />
                </Grid>
                {/* TODO: replace country and city fields with leaflet map */}
                <Grid item sm={2}></Grid>
                <Grid item sm={4}>
                    <Field
                        minWidth={140}
                        name="gender"
                        component={renderSelectField}
                        label="Gender"
                    >
                        {genders.map((value, index) => (
                            <option key={value} value={index}>{value}</option> 
                        ))}
                    </Field>
                </Grid>
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

export default reduxForm({
    form: 'registrationForm',
    destroyOnUnmount: false,
    forceUnregisterOnUnmount: true,
})(CompleteProfileForm);
