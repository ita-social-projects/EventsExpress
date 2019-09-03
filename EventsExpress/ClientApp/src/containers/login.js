import { connect } from 'react-redux';
import React, { Component } from 'react';
import Login  from '../components/login';
import login from '../actions/login';
import GoogleLogin from './GoogleLogin';
import { useAlert } from "react-alert";
import FacebookLogin from './FacebookLogin';

class LoginWrapper extends Component {
  submit = values => {
    this.props.login(values.email, values.password);
    };
   
  render() {
      alert = useAlert;
      let { loginError } = this.props.loginStatus;
      console.log(this.props.loginError);
      return <>
          <div>
              <Login onSubmit={this.submit} loginError={loginError} />
            
          <div className="row">
              <FacebookLogin />   
              <GoogleLogin />
          </div>
             
          </div>
   </>
  }
}
const mapStateToProps = state => {
    return {
        loginStatus: state.login
    }
};

const mapDispatchToProps = dispatch => {
  return {
    login: (email, password) => dispatch(login(email, password))
  };
};
export default connect(
  mapStateToProps,
  mapDispatchToProps
)(LoginWrapper);