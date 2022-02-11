import React from 'react';
import { Grid, Button } from '@material-ui/core';
import { reduxForm, Field } from 'redux-form';
import parentStatusEnum from '../../../../constants/parentStatusEnum';
import relationShipStatusEnum from '../../../../constants/relationShipStatusEnum';
import theTypeOfLeisureEnum from '../../../../constants/theTypeOfLeisureEnum';
import reasonsForUsingTheSiteEnum from '../../../../constants/reasonsForUsingTheSiteEnum';
import eventTypeEnum from '../../../../constants/eventTypeEnum';
import { radioButton, MultiCheckbox } from '../../../helpers/form-helpers';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Radio from '@material-ui/core/Radio';
import '../../RegistrationForm.css';
import './CheckboxDesign.css';

const reasonsOptions = [
    { value: reasonsForUsingTheSiteEnum.BeMoreActive, text: 'Be more active' },
    { value: reasonsForUsingTheSiteEnum.DevelopASkill, text: 'Develop a skill' },
    { value: reasonsForUsingTheSiteEnum.MeetPeopleLikeMe, text: 'Meet people like me' },
];

const eventPreferencesOptions = [
    { value: eventTypeEnum.AnyDistance, text: 'Any distance' },
    { value: eventTypeEnum.Free, text: 'Free' },
    { value: eventTypeEnum.NearMe, text: 'Near me' },
    { value: eventTypeEnum.Offline, text: 'Offline' },
    { value: eventTypeEnum.Online, text: 'Online' },
    { value: eventTypeEnum.Paid, text: 'Paid' },
];

// TODO: rewrite styles with hooks
const UserPreferencesForm = ({ handleSubmit, previousPage }) => (
    <div className="Step4">
        <form onSubmit={handleSubmit}>
            <h1 className="text-left">
                Step 4: Tell us more about yourself
            </h1>
            <Grid container spacing={2} className="text-left">
                <Grid item xs={4}>
                    <div className="stepper-tellusmore-block">
                        <Field
                            name="parentstatus"
                            component={radioButton}
                            parse={parseInt}
                        >
                            <label className="font-weight-bold">
                                Parental status
                            </label>
                            <FormControlLabel
                                value={parentStatusEnum.Kids}
                                control={<Radio />}
                                label={'With kids'}
                            />
                            <FormControlLabel
                                value={parentStatusEnum.NoKids}
                                control={<Radio />}
                                label={'No kids'}
                            />
                        </Field>
                    </div>
                    <div className="stepper-tellusmore-block">
                        <label className="font-weight-bold">
                            Reasons for using the site
                        </label>
                        <Field
                            options={reasonsOptions}
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
                            options={eventPreferencesOptions}
                            component={MultiCheckbox}
                            name="eventType"
                            className="form-control mt-2"
                            placeholder="eventType"
                        />
                    </div>
                </Grid>
                <Grid item xs={4}>
                    <div className="stepper-tellusmore-block">
                        <Field
                            name="relationshipstatus"
                            component={radioButton}
                            parse={parseInt}
                        >
                            <label className="font-weight-bold">
                                Relationship status
                            </label>
                            <FormControlLabel
                                value={relationShipStatusEnum.Single}
                                control={<Radio />}
                                label={'Single'}
                            />
                            <FormControlLabel
                                value={relationShipStatusEnum.InARelationship}
                                control={<Radio />}
                                label={'In a relationship'}
                            />
                        </Field>
                    </div>
                    <div className="stepper-tellusmore-block">
                        <Field
                            name="thetypeofleisure"
                            component={radioButton}
                            parse={parseInt}
                        >
                            <label className="font-weight-bold">
                                Preffered type of leisure
                            </label>
                            <FormControlLabel
                                value={theTypeOfLeisureEnum.Active}
                                control={<Radio />}
                                label={'Active'}
                            />
                            <FormControlLabel
                                value={theTypeOfLeisureEnum.Passive}
                                control={<Radio />}
                                label={'Passive'}
                            />
                        </Field>
                    </div>
                </Grid>
                <Grid item xs={4}></Grid>
            </Grid>
            <div className="stepper-submit">
                <Grid container spacing={3}>
                    <Grid item sm={12}>
                        <Button
                            type="button"
                            className="previous"
                            onClick={previousPage}
                            color="primary"
                            variant="text"
                            size="large"
                        >
                            Back
                        </Button>
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
            </div>
        </form>
    </div>
);

export default reduxForm({
    form: 'registrationForm',
    destroyOnUnmount: false,
    forceUnregisterOnUnmount: true,
})(UserPreferencesForm);
