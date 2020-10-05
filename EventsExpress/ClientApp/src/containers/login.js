import { connect } from 'react-redux';
import React, { Component, Fragment } from 'react';
import Login from '../components/login';
import login from '../actions/login';
import GoogleLogin from './GoogleLogin';
import { useAlert } from "react-alert";
import TwitterLogin from './TwitterLogin';

class LoginWrapper extends Component {
  submit = values => {
    this.props.login(values.email, values.password);
  };

  render() {
    alert = useAlert;
    let { loginError } = this.props.loginStatus;

    return <>
      <div>
        <Login onSubmit={this.submit} loginError={loginError} />

        <div className="d-flex justify-content-around">
          <TwitterLogin />
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
