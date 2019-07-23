import React from "react";
import Register from "../components/register";
import { connect } from "react-redux";
import register from "../actions/register";
import { setRegisterSuccess, setRegisterError } from '../actions/register'

class RegisterWrapper extends React.Component {
    componentDidUpdate(prevProps, prevState) {
        if (!this.props.registerError && this.props.isRegisterSuccess) {
            this.props.handleClose();
            this.props.reset();
        }
    }


    submit = values => {
        console.log(values);
        this.props.register(values.email, values.password); 
  };
  render() {

    const { registerError } = this.props;
    
    return <>
        <Register onSubmit={this.submit} />

      {registerError && 
              <p className="text-danger text-center">{registerError}</p>
              }
      </>;
    
  }
}
const mapStateToProps = state => {
  return state.register;
};

const mapDispatchToProps = dispatch => {
  return {
      register: (email, password) => dispatch(register(email, password)),
      reset: () => { dispatch(setRegisterSuccess(false)); dispatch(setRegisterError(null)); }
  };
};
export default connect(
  mapStateToProps,
  mapDispatchToProps
)(RegisterWrapper);