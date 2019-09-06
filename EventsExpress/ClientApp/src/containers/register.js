import React from "react";
import Register from "../components/register";
import { connect } from "react-redux";
import register from "../actions/register";
import { useAlert } from "react-alert";


class RegisterWrapper extends React.Component {
    componentDidUpdate(prevProps, prevState) {
        if (!this.props.registerError && this.props.isRegisterSuccess) {
            this.props.handleClose();
        }
    }


    submit = values => {
        this.props.register(values.email, values.password); 
  };
  render() {
      alert = useAlert;
      const {  registerError } = this.props;
    
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
      register: (email, password) => dispatch(register(email, password))
  };
};
export default connect(
  mapStateToProps,
  mapDispatchToProps
)(RegisterWrapper);