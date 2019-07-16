import React from "react";
import Register from "../components/register";
import { connect } from "react-redux";
import register from "../actions/register";

class RegisterWrapper extends React.Component {
  submit = values => {
    console.log(values);
    this.props.register(values.email, values.password);
  };
  render() {
    return <Register onSubmit={this.submit} />;
  }
}
const mapStateToProps = state => {
  return {
    isRegisterPending: state.register.isRegisterPending,
    isRegisterSuccess: state.register.isRegisterSuccess,
    registerError: state.register.registerError
  };
};

const mapDispatchToProps = dispatch => {
  return {
    register: (email, password) => dispatch(register(email, password))
  };
};
export default connect(
  mapStateToProps,
  mapDispatchToProps
)(RegisterWrapper);