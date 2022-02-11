import React from 'react';
import { Grid, Button } from '@material-ui/core';
import { reduxForm, Field } from 'redux-form';
import moment from 'moment';
import ChangeAvatarWrapper from '../../../../containers/editProfileContainers/change-avatar';
import genders from '../../../../constants/GenderConstants';
import {
    renderDatePicker,
    renderTextField,
    renderSelectField,
    parseEuDate
} from '../../../helpers/form-helpers';

// TODO: extract styles
const CompleteProfileForm = ({ handleSubmit }) => (
    <div style={{ width: '97%', padding: '10px' }}>
        <form onSubmit={handleSubmit}>
            <Grid container spacing={3}>
                <Grid item sm={6}>
                    <h1 style={{ fontSize: 20 }}>
                        Step 2: Complete your profile
                    </h1>
                </Grid>
                <Grid item sm={6} />
                <Grid item sm={3}>
                    Choose your avatar:
                </Grid>
                <Grid item sm={9}>
                    <ChangeAvatarWrapper />
                </Grid>
                <Grid item sm={12} />
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
                <Grid item sm={2}></Grid>
                <Grid item sm={4}>
                    <Field
                        minWidth={140}
                        name="gender"
                        component={renderSelectField}
                        label="Gender"
                    >
                        {genders.map((value, index) => (
                            <option value={index}>{value}</option> 
                        ))}
                    </Field>
                </Grid>
                <Grid item sm={12}>
                    <Button
                        type="submit"
                        className="next"
                        color="primary"
                        variant="contained"
                        size="large"
                    >
                        Continue
                    </Button>
                </Grid>
            </Grid>
        </form>
    </div>
);

export default reduxForm({
    form: 'registrationForm',
    destroyOnUnmount: false,
    forceUnregisterOnUnmount: true,
})(CompleteProfileForm);
