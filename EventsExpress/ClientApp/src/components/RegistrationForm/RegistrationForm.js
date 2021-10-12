import "./RegistrationForm.css";
import React, { Component } from "react";
import Stepper from "../stepper/Stepper";
import { UserForm } from "./UserForm";
import { Button } from "@material-ui/core";


export default class App extends Component {
  constructor() {
    super();
    this.state = {
      currentStep: 2,
    };
  }

  handleClick = (clickType) => {
    const { currentStep } = this.state;
    let newStep = currentStep;
    clickType === "next" ? newStep++ : newStep--;

    if (newStep > 1 && newStep <= 6) {
      this.setState({
        currentStep: newStep,
      });
    }
  };

  render() {
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
            <UserForm currentStepNumber={currentStep} />
          </div>
          <div className="buttons-container">
            {currentStep > 2 && currentStep < 6 ? (
              <Button
                color="primary"
                variant="text"
                onClick={() => this.handleClick()}
              >
                Back
              </Button>
            ) : (
              ""
            )}

            {currentStep < 5 ? (
              <Button
                color="primary"
                variant="contained"
                size="large"
                onClick={() => this.handleClick("next")}
              >
                Continue
              </Button>
            ) : (
              ""
            )}
            {currentStep == 5  ? (
              <Button
                color="primary"
                variant="contained"
                size="large"
                onClick={() => this.handleClick("next")}
              >
                Confirm
              </Button>
            ) : (
              ""
            )}
          </div>
        </div>
      </>
    );
  }
}

const stepsArray = [
  "Register",
  "Additional info",
  "Reasons to join",
  "Concreete reasons",
  "Complete",
];
