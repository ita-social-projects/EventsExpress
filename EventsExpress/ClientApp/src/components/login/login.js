import React, { Component} from "react";
import { Field, reduxForm } from "redux-form";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import Module from '../helpers';
import { Link } from 'react-router-dom';
import RecoverPassword from '../recoverPassword/recoverPassword';



const { validate, renderTextField, asyncValidate } = Module;

class Login extends Component {
  constructor(props) {
      super(props);

    }

    openModal = () => (<RecoverPassword/>)
    
  render() {
     
      const { pristine, reset, submitting } = this.props;

    return (
      <div className="auth">
        <form onSubmit={this.props.handleSubmit}>
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
            <div>
                    <RecoverPassword />
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