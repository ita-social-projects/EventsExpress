import "./RegistrationForm.css";
import "./CheckboxDesign.css";
import React from "react";
import { Grid, Button } from "@material-ui/core";
import { reduxForm, Field } from "redux-form";
import parentStatusEnum from '../../constants/parentStatusEnum';
import relationShipStatusEnum from '../../constants/relationShipStatusEnum';
import theTypeOfLeisureEnum from '../../constants/theTypeOfLeisureEnum';
import reasonsForUsingTheSiteEnum from '../../constants/reasonsForUsingTheSiteEnum';
import eventTypeEnum from '../../constants/eventTypeEnum';
import { radioButton, MultiCheckbox } from '../helpers/form-helpers';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Radio from '@material-ui/core/Radio';

const TellUsMoreAbotYourself = (props) => {
    const { handleSubmit, previousPage } = props;
                                                        
    let options1 = [
        { value: reasonsForUsingTheSiteEnum.BeMoreActive, text: "Be more active" },
        { value: reasonsForUsingTheSiteEnum.DevelopASkill, text: "Develop a skill" },
        { value: reasonsForUsingTheSiteEnum.MeetPeopleLikeMe, text: "Meet people like me" }
    ];

    let options2 = [
        { value: eventTypeEnum.AnyDistance, text: "Any distance" },
        { value: eventTypeEnum.Free, text: "Free" },
        { value: eventTypeEnum.NearMe, text: "Near me" },
        { value: eventTypeEnum.Offline, text: "Offline" },
        { value: eventTypeEnum.Online, text: "Online" },
        { value: eventTypeEnum.Paid, text: "Paid" }
    ];

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
                                parse={parseInt} >
                                <label className="font-weight-bold">Select parent status</label>
                                <FormControlLabel
                                    value={parentStatusEnum.Kids}
                                    control={<Radio />}
                                    label={"Kids"} />
                                <FormControlLabel
                                    value={parentStatusEnum.NoKids}
                                    control={<Radio />}
                                    label={"No Kids"} />
                            </Field>
                        </div>
                        <div class="stepper-tellusmore-block">
                            <label className="font-weight-bold">Select reasons for using the site</label>
                            <Field
                                options={options1}
                                component={MultiCheckbox}
                                name="reasonsForUsingTheSite"
                                className="form-control mt-2"
                                placeholder='reasonsForUsingTheSiteEnum'
                            />
                        </div>
                    </Grid>
                    <Grid item xs={4}>
                        <div class="stepper-tellusmore-block">
                            <label className="font-weight-bold">Select event type</label>
                            <Field
                                options={options2}
                                component={MultiCheckbox}
                                name="eventType"
                                className="form-control mt-2"
                                placeholder='eventType'
                            />
                        </div>
                    </Grid>
                    <Grid item xs={4}>
                        <div class="stepper-tellusmore-block">
                            <Field
                                name="relationshipstatus"
                                component={radioButton}
                                parse={parseInt} >
                                <label className="font-weight-bold">Select relationship status</label>
                                <FormControlLabel
                                    value={relationShipStatusEnum.Single}
                                    control={<Radio />}
                                    label={"Single"} />
                                <FormControlLabel
                                    value={relationShipStatusEnum.InARelationship}
                                    control={<Radio />}
                                    label={"In a relationship"} />
                            </Field>
                        </div>
                        <div class="stepper-tellusmore-block">
                            <Field
                                name="thetypeofleisure"
                                component={radioButton}
                                parse={parseInt} >
                                <label className="font-weight-bold">Select the type of leisure status</label>
                                <FormControlLabel
                                    value={theTypeOfLeisureEnum.Active}
                                    control={<Radio />}
                                    label={"Active"} />
                                <FormControlLabel
                                    value={theTypeOfLeisureEnum.Passive}
                                    control={<Radio />}
                                    label={"Passive"} />
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
