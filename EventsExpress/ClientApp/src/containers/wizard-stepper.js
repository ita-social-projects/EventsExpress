import React, { Component, useState } from 'react';
import { connect } from 'react-redux';
import { makeStyles } from '@material-ui/core/styles';
import Stepper from '@material-ui/core/Stepper';
import { getFormValues, reset, isPristine, submit } from 'redux-form';
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
import Publish from '../components/Draft/WizardFromPublish';
import RSB from '../components/Draft/RemoteSubmitButton'
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
const formSteps = [
    { part: 0, Stepname: "1", component: <Part1 />, formName: 'Part1' },
    { part: 1, Stepname: "2", component: <Part2 />, formName: 'Part2' },
    { part: 2, Stepname: "3", component: <Part3 />, formName: 'Part3' },
    { part: 3, Stepname: "4", component: <Part4 /> },
    { part: 4, Stepname: "5", component: <Part5 />, formName: 'Part5' },
    { part: 5, Stepname: "6", component: <Publish /> },

];

class HorizontalLinearStepper extends Component {

    constructor(props) {
        super(props);
        this.state = {
            activeStep: 0,
            isLastStep:false,
        };
    }

        //if you need to add new step use formSteps.push orr refaktor already existing fromSteps
       
        renderPublish = () => {
            if (this.props.event && this.props.event.eventStatus === 3) {
                formSteps.push({
                    part: 5,
                    Stepname: "publish",
                    component: <Publish />,
                });
            }
        }
        getSteps = () => {

            var steppArray = [];

            formSteps.forEach(pair => steppArray.push(pair.name));

            return steppArray;
        };




        getStepContent = (step, props) => {
            const curStep = formSteps.filter(pair => pair.part === step);
            if (curStep && curStep[0].component && curStep[0].part != null && curStep[0].Stepname) {
                return curStep[0].component;
            }
            return 'Unknown step';
        };

        renderErrors = (error) => {
            const keys = Object.keys(error);
            let i = 0;
            while (i < keys.length) {
                if (keys[i] === "title" || keys[i] === "description" || keys[i] === "dateFrom" || keys[i] === "dateTo" || keys[i] === "categories") {
                    this.errorStep.push(0);
                }
                if (keys[i] === "photoId" || keys[i] === "photo") {
                    this.errorStep.push(1);
                }
                if (keys[i] === "eventLocation" || keys[i] === "location.type") {
                    this.errorStep.push(2);
                }
                if (keys[i] === "maxParticipants") {
                    this.errorStep.push(4);
                }
                i++;
            }
        };

        steps = this.getSteps();

        errorStep = [];

        isStepFailed = (step) => {
            let j = 0;
            while (j < this.errorStep.length) {
                if (step === this.errorStep[j]) {
                    return step === this.errorStep[j];
                }
                j++;
            }
            if (this.errorStep) {
                this.errorStep = [];
            }
        };

        isStepOptional = (step) => {
            return false;
        };
 
        changeStep = (newStep) => {
            let newState = {
                activeStep: newStep,
                isLastStep: newStep === 5,
            };
            this.props.submitFormByName(formSteps[this.state.activeStep].formName);
            this.setState(newState);
        }
        handleNext = () => {
            this.changeStep(this.state.activeStep + 1)
        };
    

        handleBack = () => {
            this.changeStep(this.state.activeStep - 1)
        };


        handleReset = () => {
            setActiveStep(0);
        };
        render()
        {

            return (
                <div className={useStyles.root}>
                    <Stepper activeStep={this.state.activeStep}>
                        {this.steps.map((label, index) => {
                            const stepProps = {};
                            const labelProps = {};
                            <ul>
                                {this.renderErrors(this.props.errors)}
                            </ul>
                            if (this.isStepOptional(index)) {
                                labelProps.optional = <Typography variant="caption">Optional</Typography>;
                            }
                            if (this.isStepFailed(index)) {
                                labelProps.optional = (
                                    <Typography variant="caption" color="error">
                                        Validation error
                                    </Typography>
                                );
                            }
                            if (this.isStepFailed(index)) {
                                labelProps.error = true;
                            }
                            return (
                                <Step key={label} {...stepProps}>
                                    <StepLabel {...labelProps}>{label}</StepLabel>
                                </Step>
                            );
                        })}
                    </Stepper>
                    <div>
                        {this.state.activeStep === this.steps.length ? (
                            <div>
                                <Typography className={useStyles.instructions}>
                                    All steps completed - you&apos;re finished
            </Typography>
                                <Button onClick={handleReset} className={useStyles.button}>
                                    Reset
            </Button>
                            </div>
                        ) : (
                                <div>
                                    {/*<Typography className={classes.instructions}>{getStepsTest()}</Typography>*/}
                                    <Typography className={useStyles.instructions}>

                                        {formSteps.map((s, i) => <>
                                            <div className={this.state.activeStep !== i ? 'd-none' : ' '}>
                                                { s.component }
                                            </div>
                                        </>)
                                        }

                                    </Typography>
                                    <div>
                                        <Button disabled={this.state.activeStep === 0} onClick={this.handleBack} className={useStyles.button}>
                                            Back
                                        </Button>
                                        <Button onClick={this.handleNext} className={useStyles.button}>Next</Button>
                                    </div>
                                </div>
                            )}
                    </div>
                </div>
            );
        }
}

const mapStateToProps = (state) => ({
    user_id: state.user.id,
    form_values: getFormValues('event-form')(state),
    event: state.event.data,
    errors: state.publishErrors.data
});
const mapDispatchToProps = (dispatch) => {
    return {
        submitFormByName: (name) => dispatch(submit(name)),
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
