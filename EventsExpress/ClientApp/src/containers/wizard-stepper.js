import React, { Component } from 'react';
import { connect } from 'react-redux';
import { makeStyles } from '@material-ui/core/styles';
import Stepper from '@material-ui/core/Stepper';
import { getFormValues, reset, isPristine } from 'redux-form';
import { validateEventForm } from '../components/helpers/helpers'
import Step from '@material-ui/core/Step';
import StepLabel from '@material-ui/core/StepLabel';
import Button from '@material-ui/core/Button';
import get_categories from '../actions/category/category-list';
import Typography from '@material-ui/core/Typography'; 
import Part1 from '../components/Draft/WizardFormPart1';
import Part2 from '../components/Draft/WizardFormPart2';
import Part3 from '../components/Draft/WizardFormPart3';
import Part4 from '../components/Draft/WizardFormPart4';
import Part5 from '../components/Draft/WizardFormPart5';
import Test from '../components/Draft/WizardFormTest';
import { setEventPending, setEventSuccess, edit_event, publish_event } from '../actions/event-add-action';



const useStyles = makeStyles((theme) => ({
    root: {
        width: '100%',
    },
    button: {
        marginRight: theme.spacing(1),
    },
    instructions: {
        marginTop: theme.spacing(1),
        marginBottom: theme.spacing(1),
    },
}));


function HorizontalLinearStepper(props) {
    const classes = useStyles();
    const [activeStep, setActiveStep] = React.useState(0);
    const [skipped, setSkipped] = React.useState(new Set());
    //if you need to add new step use formSteps.push orr refaktor already existing fromSteps
    const formSteps = [
        { part: 0, Stepname: "1" },
        { part: 1, Stepname: "2" },
        { part: 2, Stepname: "3" },
        { part: 3, Stepname: "4" },
        { part: 4, Stepname: "5" },

    ];

    if (props.event && props.event.eventStatus === 3) {
        formSteps.push({
            case: 5,
            name: "publish"
        });
    }

        const getSteps = () => {

            var steppArray = [];

            formSteps.forEach(pair => steppArray.push(pair.name));

            return steppArray;
        };
    

    
     //if you need to add new step add new switch case with new step index
    const getStepContent = (step, props) => {
        switch (step) {
            case formSteps[0].part:
                return <Part1 />;
            case formSteps[1].part:
                return <Part2 />;
            case formSteps[2].part:
                return <Part3 />;
            case formSteps[3].part:
                return <Part4 />;
            case formSteps[4].part:
                return <Part5 />;
            case formSteps[5].part:
                return <form onSubmit={onPublish}>
                <div>
                    <Button
                        className="border"
                        fullWidth={true}
                        color="primary"
                        type="submit"
                    >
                        Publish
                        </Button>
                    </div>
                </form>
            default:
                return 'Unknown step';
        }
    }

    const steps = getSteps();

    const onPublish = (values) => {
        return props.publish(props.event.id);
    }
    const isStepFailed = (step) => {
        return step === 1;
    };

    const isStepOptional = (step) => {
        return false;
    };

    const isStepSkipped = (step) => {
        return skipped.has(step);
    };

    const handleNext = () => {
        let newSkipped = skipped;
        if (isStepSkipped(activeStep)) {
            newSkipped = new Set(newSkipped.values());
            newSkipped.delete(activeStep);
        }

        setActiveStep((prevActiveStep) => prevActiveStep + 1);
        setSkipped(newSkipped);
    };

    const handleBack = () => {
        setActiveStep((prevActiveStep) => prevActiveStep - 1);
    };

    const handleSkip = () => {
        if (!isStepOptional(activeStep)) {
            // You probably want to guard against something like this,
            // it should never occur unless someone's actively trying to break something.
            throw new Error("You can't skip a step that isn't optional.");
        }

        setActiveStep((prevActiveStep) => prevActiveStep + 1);
        setSkipped((prevSkipped) => {
            const newSkipped = new Set(prevSkipped.values());
            newSkipped.add(activeStep);
            return newSkipped;
        });
    };

    const handleReset = () => {
        setActiveStep(0);
    };

    return (
        <div className={classes.root}>
            <Stepper activeStep={activeStep}>
                {steps.map((label, index) => {
                    const stepProps = {};
                    const labelProps = {};
                    if (isStepOptional(index)) {
                        labelProps.optional = <Typography variant="caption">Optional</Typography>;
                    }
                    if (isStepFailed(index)) {
                        labelProps.optional = (
                            <Typography variant="caption" color="error">
                                Validation error
                            </Typography>
                        );
                    }
                    if (isStepFailed(index)) {
                        labelProps.error = true;
                    }
                    if (isStepSkipped(index)) {
                        stepProps.completed = false;
                    }
                    return (
                        <Step key={label} {...stepProps}>
                            <StepLabel {...labelProps}>{label}</StepLabel>
                        </Step>
                    );
                })}
            </Stepper>
            <div>
                {activeStep === steps.length ? (
                    <div>
                        <Typography className={classes.instructions}>
                            All steps completed - you&apos;re finished
            </Typography>
                        <Button onClick={handleReset} className={classes.button}>
                            Reset
            </Button>
                    </div>
                ) : (
                        <div>
                            <Typography className={classes.instructions}>{getStepContent(activeStep,props)}</Typography>
                            <div>
                                <Button disabled={activeStep === 0} onClick={handleBack} className={classes.button}>
                                    Back
              </Button>
                                {isStepOptional(activeStep) && (
                                    <Button
                                        variant="contained"
                                        color="primary"
                                        onClick={handleSkip}
                                        className={classes.button}
                                    >
                                        Skip
                                    </Button>
                                )}

                                <Button
                                    variant="contained"
                                    color="primary"
                                    onClick={handleNext}
                                    type=""
                                    className={classes.button}
                                >
                                    {activeStep === steps.length - 1 ? 'Finish' : 'Next'}
                                </Button>
                            </div>
                        </div>
                    )}
            </div>
            </div>
    );
}
const mapStateToProps = (state) => ({
    user_id: state.user.id,
    form_values: getFormValues('event-form')(state),
    event: state.event.data,
});
const mapDispatchToProps = (dispatch) => {
    return {
        add_event: (data) => dispatch(edit_event(data)),
        get_categories: () => dispatch(get_categories()),
        publish: (data) => dispatch(publish_event(data)),
        resetEvent: () => dispatch(resetEvent()),
        reset: () => {
            dispatch(reset('event-form'));
            dispatch(setEventPending(true));
            dispatch(setEventSuccess(false));
        }
    }
};
export default connect(mapStateToProps, mapDispatchToProps)(HorizontalLinearStepper);
