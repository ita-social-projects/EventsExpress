import "./RegistrationForm.css";
import React, { Component } from "react";
import Stepper from "../stepper/Stepper";
import CompleteProfileForm from "./CompleteProfileForm";
import ConfirmForm from "./ConfirmForm";
import ChooseActivities from "./ChooseActivities";
import Success from "./Success";
import PlaceHolder from "./PlaceHolder";
import TellUsMoreAboutYourself from "./TellUsMoreAboutYourself";
import PropTypes from "prop-types";

export default class RegistrationForm extends Component {
  constructor(props) {
    super(props);
    this.nextPage = this.nextPage.bind(this);
    this.previousPage = this.previousPage.bind(this);
    this.state = {
      currentStep: 2,
    };
  }

  nextPage() {
    this.setState({ currentStep: this.state.currentStep + 1 });
  }

  previousPage() {
    this.setState({ currentStep: this.state.currentStep - 1 });
  }

  render() {
    const { onSubmit } = this.props;
    const { currentStep } = this.state;

    return (
      <>
        <div className="stepper-container-horizontal">
          <Stepper
            currentStepNumber={currentStep - 1}
            steps={stepsArray}
            stepColor="#ff9900"
          />
          <br />
          <div className="buttons-container">
            <div>
              {currentStep === 2 && (
                <CompleteProfileForm onSubmit={this.nextPage} />
              )}
              {currentStep === 3 && (
                <ChooseActivities
                  previousPage={this.previousPage}
                  onSubmit={this.nextPage}
                />
              )}
              {currentStep === 4 && (
                <TellUsMoreAboutYourself
                  previousPage={this.previousPage}
                  onSubmit={this.nextPage}
                />
              )}
              {currentStep === 5 && (
                <ConfirmForm
                  previousPage={this.previousPage}
                  onSubmit={this.nextPage}
                />
              )}
              {currentStep === 6 && <Success />}
            </div>
          </div>
        </div>
      </>
    );
  }
}

const stepsArray = [
  "Register",
  "Complete Profile",
  "Step 3",
  "Step 4",
  "Confirm",
];

RegistrationForm.propTypes = {
  onSubmit: PropTypes.func.isRequired,
};
