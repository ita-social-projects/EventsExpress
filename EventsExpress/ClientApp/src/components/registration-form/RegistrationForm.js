import "./RegistrationForm.css";
import React, { Component } from "react";
import PropTypes from "prop-types";
import Stepper from "../stepper/Stepper";
import ConfirmationView from "./steps/confirm/ConfirmationView";
import CompleteProfileForm from "./steps/complete-profile/CompleteProfileForm";
import UserPreferencesForm from "./steps/preferences/UserPreferencesForm";
import ChooseActivities from "./steps/activities/ChooseActivities";
import SuccessPage from "./steps/success/SuccessPage";

const stepsArray = [
    "Register",
    "Complete Profile",
    "Choose Activities",
    "Preferences",
    "Confirm",
  ];

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
                <UserPreferencesForm
                  previousPage={this.previousPage}
                  onSubmit={this.nextPage}
                />
              )}
              {currentStep === 5 && (
                <ConfirmationView
                  previousPage={this.previousPage}
                  onSubmit={this.nextPage}
                />
              )}
              {currentStep === 6 && (
                <SuccessPage />
              )}
            </div>
          </div>
        </div>
      </>
    );
  }
}

RegistrationForm.propTypes = {
  onSubmit: PropTypes.func.isRequired,
};
