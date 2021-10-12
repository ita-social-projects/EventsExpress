import React, { Component } from "react";
import AdditionalInfoForm from "./AdditionalInfoForm";
import ConfirmForm from "./ConfirmForm";
import Success from "./Success";
import PropTypes from "prop-types";

export class UserForm extends Component {
  constructor() {
    super();
    this.state = {
      firstName: "",
      lastName: "",
      country: "",
      city: "",
      gender: "",
      birthDate: "",
    };
  }

  // Handle fields change
  handleChange = (input) => (e) => {
    this.setState({ [input]: e.target.value });
  };

  render() {
    const { currentStepNumber } = this.props;
    const { firstName, lastName, city, country, gender, birthDate } = this.state;
    const values = { firstName, lastName, city, country, gender };
    switch (currentStepNumber) {
      case 1:
        return <h1>1 stage</h1>;
      case 2:
        return (
          <AdditionalInfoForm
            handleChange={this.handleChange}
            values={values}
          />
        );
      case 3:
        return <h1>3 stage</h1>;
      case 4:
        return <h1>4 stage</h1>;
      case 5:
        return (
          <ConfirmForm
            values={values}
          />
        );
      case 6:
        return <Success/>;
      default:
        return <h1>Something went wrong</h1>;
    }
  }
}

export default UserForm;

UserForm.propTypes = {
  currentStepNumber: PropTypes.number.isRequired,
};
