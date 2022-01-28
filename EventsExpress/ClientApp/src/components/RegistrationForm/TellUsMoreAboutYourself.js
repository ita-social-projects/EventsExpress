import "./RegistrationForm.css";
import "./CheckboxDesign.css";
import React from "react";
import {
    Grid,
    Button,
    Radio,
    FormControlLabel,
    FormLabel,
} from "@material-ui/core";
import { reduxForm, Field } from "redux-form";
import parentStatusEnum from "../../constants/parentStatusEnum";
import relationShipStatusEnum from "../../constants/relationShipStatusEnum";
import theTypeOfLeisureEnum from "../../constants/theTypeOfLeisureEnum";
import reasonsForUsingTheSiteEnum from "../../constants/reasonsForUsingTheSiteEnum";
import eventTypeEnum from "../../constants/eventTypeEnum";
import { radioButton, MultiCheckbox } from "../helpers/form-helpers";

const TellUsMoreAbotYourself = (props) => {
    const { handleSubmit, previousPage } = props;

    let reasonsOpts = reasonsForUsingTheSiteEnum.map((i) => ({
        value: reasonsForUsingTheSiteEnum.indexOf(i),
        text: i,
    }));

    let eventTypeOpts = eventTypeEnum.map((i) => ({
        value: eventTypeEnum.indexOf(i),
        text: i,
    }));

    return (
        <div className="Step4">
            <form onSubmit={handleSubmit}>
                <h1 className="text-left">
                    Step 4: Tell us more about yourself.{" "}
                </h1>
                <Grid container spacing={2} className="text-left">
                    <Grid item xs={4}>
                        <div className="stepper-tellusmore-block">
                            <label className="font-weight-bold">
                                Select parent status
                            </label>
                            <Field
                                name="parentstatus"
                                component={radioButton}
                                parse={parseInt}
                            >
                                {parentStatusEnum.map((i) => (
                                    <FormControlLabel
                                        key={i.value}
                                        value={i.value}
                                        control={<Radio />}
                                        label={i.label}
                                    />
                                ))}
                            </Field>
                        </div>
                        <div className="stepper-tellusmore-block">
                            <label className="font-weight-bold">
                                Select reasons for using the site
                            </label>
                            <Field
                                options={reasonsOpts}
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
                                Select event type
                            </label>
                            <Field
                                options={eventTypeOpts}
                                component={MultiCheckbox}
                                name="eventType"
                                className="form-control mt-2"
                                placeholder="eventType"
                            />
                        </div>
                    </Grid>
                    <Grid item xs={4}>
                        <div className="stepper-tellusmore-block">
                            <label className="font-weight-bold">
                                Select relationship status
                            </label>
                            <Field
                                name="relationshipstatus"
                                component={radioButton}
                                parse={parseInt}
                            >
                                {relationShipStatusEnum.map((i) => (
                                    <FormControlLabel
                                        key={i.value}
                                        value={i.value}
                                        control={<Radio />}
                                        label={i.label}
                                    />
                                ))}
                            </Field>
                        </div>
                        <div className="stepper-tellusmore-block">
                            <label className="font-weight-bold">
                                Select the type of leisure status
                            </label>
                            <Field
                                name="thetypeofleisure"
                                component={radioButton}
                                parse={parseInt}
                            >
                                {theTypeOfLeisureEnum.map((i) => (
                                    <FormControlLabel
                                        key={i.value}
                                        value={i.value}
                                        control={<Radio />}
                                        label={i.label}
                                    />
                                ))}
                            </Field>
                        </div>
                    </Grid>
                    <Grid item xs={4}></Grid>
                </Grid>

                <div className="stepper-submit">
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
