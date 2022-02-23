import React, { useContext } from 'react';
import { reduxForm, Field } from 'redux-form';
import { Grid } from '@material-ui/core';
import { MultiCheckbox } from '../../../helpers/form-helpers';
import { RegisterStepContext } from '../../RegistrationForm';
import RadioGroup from './RadioGroup';
import StepperNavigation from '../../StepperNavigation';
import { renderTextArea } from '../../../helpers/form-helpers'
import maps from '../../maps'
import '../../RegistrationForm.css';
import './CheckboxDesign.css';

const UserPreferencesForm = () => {
    const { goToNext } = useContext(RegisterStepContext);
    
    const mapToOptions = sourceMap => {
        return Array.from(sourceMap).map(([key, value]) => ({ value: key, text: value }));
    };

    return (
        <form onSubmit={goToNext}>
            <h1>Step 4: Tell us more about yourself</h1>
            <Grid container spacing={2}>
                <Grid item xs={4}>
                    <div className="stepper-tellusmore-block">
                        <RadioGroup
                            name="parentalStatus"
                            label="Parental status"
                            source={maps.parenting}
                        />
                    </div>
                    <div className="stepper-tellusmore-block">
                        <label className="font-weight-bold">
                            Reasons for using the site
                        </label>
                        <Field
                            options={mapToOptions(maps.reasons)}
                            component={MultiCheckbox}
                            name="reasonsForUsingTheSite"
                            className="form-control mt-2"
                            placeholder="reasonsForUsingTheSiteEnum"
                        />
                    </div>
                </Grid>
                <Grid item xs={4}>
                    <div className="stepper-tellusmore-block">
                        <label className="font-weight-bold">
                            Event preferences
                        </label>
                        <Field
                            options={mapToOptions(maps.eventPreferences)}
                            component={MultiCheckbox}
                            name="eventType"
                            className="form-control mt-2"
                            placeholder="eventType"
                        />
                    </div>
                </Grid>
                <Grid item xs={4}>
                    <div className="stepper-tellusmore-block">
                        <RadioGroup
                            name="relationshipStatus"
                            label="Relationship status"
                            source={maps.relationshipStatuses}
                        />
                    </div>
                    <div className="stepper-tellusmore-block">
                        <RadioGroup
                            name="leisureType"
                            label="Preferred type of leisure"
                            source={maps.leisureTypes}
                        />
                    </div>
                </Grid>
                <Grid item xs={12}>
                    <h5>Additional information:</h5>
                </Grid>
                <Grid item xs={12}>
                    <Field
                        name="additionalInfo"
                        component={renderTextArea}
                        type="input"
                    />
                </Grid>
            </Grid>
            <div className="stepper-submit">
                <StepperNavigation />
            </div>
        </form>
    );
};

export default reduxForm({
    form: 'registrationForm',
    destroyOnUnmount: false,
    forceUnregisterOnUnmount: true,
})(UserPreferencesForm);
