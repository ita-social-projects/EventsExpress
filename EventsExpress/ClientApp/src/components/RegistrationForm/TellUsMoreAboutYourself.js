import "./RegistrationForm.css";
import React from "react";
import { Grid, Button } from "@material-ui/core";
import { reduxForm, Field } from "redux-form";
import parentStatusEnum from '../../constants/parentStatusEnum';
import relationShipStatusEnum from '../../constants/relationShipStatusEnum';
import theTypeOfLeisureEnum from '../../constants/theTypeOfLeisureEnum';
import reasonsForUsingTheSiteEnum from '../../constants/reasonsForUsingTheSiteEnum';
import eventTypeEnum from '../../constants/eventTypeEnum';
import { renderCheckbox, radioButton } from '../helpers/form-helpers';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Radio from '@material-ui/core/Radio';

const TellUsMoreAbotYourself = (props) => {
    const { handleSubmit, previousPage } = props;


    return (
        <div className="Step4">
            <form onSubmit={handleSubmit}>
                <h1 className="text-left">Step4: Tell us more about yourself. </h1>
                <Grid
                    container spacing={2}
                    className="text-left">
                    <Grid item xs={4}>
                        <div class="stepper-tellusmore-block">
                            <Field
                                name="parentstatus"
                                component={radioButton}
                                parse={String} >
                                <label className="font-weight-bold">Select parent status</label>
                                <FormControlLabel
                                    value={parentStatusEnum.Kids}
                                    control={<Radio />}
                                    label={parentStatusEnum.Kids} />
                                <FormControlLabel
                                    value={parentStatusEnum.NoKids}
                                    control={<Radio />}
                                    label={parentStatusEnum.NoKids} />
                            </Field>
                        </div>
                        <div class="stepper-tellusmore-block">
                            <label className="font-weight-bold">Select reasons for using the site</label>
                            <Field
                                name='isBeMoreActive'
                                component={renderCheckbox}
                                type="checkbox"
                                label={reasonsForUsingTheSiteEnum.BeMoreActive}
                            />
                            <Field
                                name='isDevelopASkill'
                                component={renderCheckbox}
                                type="checkbox"
                                label={reasonsForUsingTheSiteEnum.DevelopASkill}
                            />
                            <Field
                                name='isMeetPeopleLikeMe'
                                component={renderCheckbox}
                                type="checkbox"
                                label={reasonsForUsingTheSiteEnum.MeetPeopleLikeMe}
                            />
                        </div>
                    </Grid>
                    <Grid item xs={4}>
                        <div class="stepper-tellusmore-block">
                            <label className="font-weight-bold">Select event type</label>
                            <Field
                                name='isAnyDistance'
                                component={renderCheckbox}
                                type="checkbox"
                                label={eventTypeEnum.AnyDistance}
                            />
                            <Field
                                name='isFree'
                                component={renderCheckbox}
                                type="checkbox"
                                label={eventTypeEnum.Free}
                            />
                            <Field
                                name='isNearMe'
                                component={renderCheckbox}
                                type="checkbox"
                                label={eventTypeEnum.NearMe}
                            />
                            <Field
                                name='isOffline'
                                component={renderCheckbox}
                                type="checkbox"
                                label={eventTypeEnum.Offline}
                            />
                            <Field
                                name='isOnline'
                                component={renderCheckbox}
                                type="checkbox"
                                label={eventTypeEnum.Online}
                            />
                            <Field
                                name='isPaid'
                                component={renderCheckbox}
                                type="checkbox"
                                label={eventTypeEnum.Paid}
                            />
                        </div>
                    </Grid>
                    <Grid item xs={4}>
                        <div class="stepper-tellusmore-block">
                            <Field
                                name="relationshipstatus"
                                component={radioButton}
                                parse={String} >
                                <label className="font-weight-bold">Select relationship status</label>
                                <FormControlLabel
                                    value={relationShipStatusEnum.Single}
                                    control={<Radio />}
                                    label={relationShipStatusEnum.Single} />
                                <FormControlLabel
                                    value={relationShipStatusEnum.InARelationship}
                                    control={<Radio />}
                                    label={relationShipStatusEnum.InARelationship} />
                            </Field>
                        </div>
                        <div class="stepper-tellusmore-block">
                            <Field
                                name="thetypeofleisure"
                                component={radioButton}
                                parse={String} >
                                <label className="font-weight-bold">Select the type of leisure status</label>
                                <FormControlLabel
                                    value={theTypeOfLeisureEnum.Active}
                                    control={<Radio />}
                                    label={theTypeOfLeisureEnum.Active} />
                                <FormControlLabel
                                    value={theTypeOfLeisureEnum.Passive}
                                    control={<Radio />}
                                    label={theTypeOfLeisureEnum.Passive} />
                            </Field>
                        </div>
                    </Grid>
                <Grid item xs={4}></Grid>
            </Grid>


            <div class="stepper-submit">
                <form onSubmit={handleSubmit}>
                    <Grid container spacing={3}>
                        <Grid item sm={12} justify="center">
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
                </form>
                </div>
            </form>
        </div>

    );
};

export default reduxForm({
    form: "registrationForm",
    destroyOnUnmount: false,
    forceUnregisterOnUnmount: true,
})(TellUsMoreAbotYourself);
