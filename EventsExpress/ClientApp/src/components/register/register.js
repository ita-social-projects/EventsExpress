import React, {Component} from "react";
import DialogActions from "@material-ui/core/DialogActions";
import Module  from '../helpers';
import { Field, reduxForm } from "redux-form";
import Button from "@material-ui/core/Button";

const { validate, renderTextField, asyncValidate } = Module;

 class Register extends Component {
  constructor(props) {
    super(props);

  }
  render() {
    const {pristine, reset , submitting} = this.props;
    return (
      <div className="register">
        <form onSubmit={this.props.handleSubmit}>
        <div>
            <Field
              name="email"
              component={renderTextField}
              label="E-mail:"
              type="email"
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
            <Field
              name="RepeatPassword"
              component={renderTextField}
              label="Repeat password:"
              type="password"
            />
          </div>
          <div>
            <DialogActions>
            <Button fullWidth={true} type="button" color="primary" disabled={pristine || submitting} onClick={reset}>
               CLEAR
        </Button >
              <Button fullWidth={true} type="submit" value="Login" color="primary">
                Sign Up
              </Button> </DialogActions>
          </div>
        </form>
      </div>
    );
  }
}

Register = reduxForm({
  // a unique name for the form
  form: "register-form",
  validate,
  asyncValidate
})(Register);

export default Register;
