import React, { Component } from "react";
import { Field, reduxForm } from "redux-form";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import renderTextField from '../helpers';

class Login extends Component {
  
 


render() {
  return (
    <div className="auth">
      <form onSubmit={this.props.handleSubmit}>
      <Field
            name="firstName"
            component="input"
            type="text"
            placeholder="First Name"
          />
        
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
          <DialogActions>
            <Button type="submit" value="Login" color="primary">
              Sign In
            </Button>
          </DialogActions>
        </div>    
      </form>
    </div>
  );
}
}



Login = reduxForm({
  form: 'login-form',
  enableReinitialize : true 
})(Login);

export default Login;