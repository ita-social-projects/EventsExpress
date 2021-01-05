import React, { Component } from "react";
import { Field, reduxForm } from "redux-form";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import Module from '../helpers';
import Modalwind2 from '../recoverPassword/modalwind2';
import GoogleLogin from '../../containers/GoogleLogin';
import LoginFacebook from '../../containers/FacebookLogin';
import TwitterLogin from '../../containers/TwitterLogin';

const { validate, renderTextField, asyncValidate } = Module;

class Login extends Component {
  openModal = () => (<Modalwind2 />)

  render() {
    const { pristine, reset, submitting, loginError } = this.props;

    return (
      <div className="auth">
        <form onSubmit={this.props.handleSubmit} autoComplete="off">
          <div>
            <Field
              name="email"
              component={renderTextField}
              label="E-mail:"
            />
          </div>
          <div>
            <Field
              name="password"
              component={renderTextField}
              label="Password:"
              type="password"
            />
          </div>
          <div>
            <DialogActions>
              <Button fullWidth={true} type="button" color="primary" disabled={pristine || submitting} onClick={reset}>
                CLEAR
              </Button >
              <Button fullWidth={true} type="submit" value="Login" color="primary">
                Sign In
              </Button>
            </DialogActions>
          </div>
        </form>
        <div className="d-flex justify-content-around mb-3">
          <TwitterLogin />
          <LoginFacebook />
          <GoogleLogin />
        </div>
        <div className="text-center">
          {loginError &&
            <p className="text-danger text-center">{loginError}</p>
          }
          <Modalwind2 />
        </div>
      </div>
    );
  }
}

Login = reduxForm({
  // a unique name for the form
  form: "login-form",
  validate,
  asyncValidate
})(Login);

export default Login;
