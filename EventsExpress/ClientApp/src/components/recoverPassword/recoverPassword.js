import React, { Component } from "react";
import { Field, reduxForm } from "redux-form";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import Module from '../helpers';
import { connect } from 'react-redux';
import DialogContentText from '@material-ui/core/DialogContentText';



const { validate, renderTextField, asyncValidate } = Module;

class RecoverPassword extends React.Component {
    constructor(props) {
        super(props)
    }

    render() {
        const { handleSubmit, pristine, reset, submitting } = this.props;
        return (
            <form onSubmit={handleSubmit}>
                <DialogContentText>
                    If you forgot your password please enter your  <br /> email address here. We will send you new<br/> password.
                    
                  </DialogContentText>
                <div>
                    <Field
                        name="email"
                        component={renderTextField}
                        label="E-mail:"
                        
                    />
                </div>
                <div>
                    <DialogActions className="d-flex flex-column ">

                        <div className="d-flex justify-content-around w-100">
                            <Button fullWidth={true} type="button" color="primary" disabled={pristine || submitting} onClick={reset}>
                                CLEAR
                                    </Button >
                            <Button fullWidth={true} type="submit" color="primary">
                                Submit
                                    </Button>
                        </div>
                        


                    </DialogActions>
                </div>
            </form>
            )
    }
}


export default reduxForm({
    // a unique name for the form
    form: "recoverPassword",
    validate,
    asyncValidate
})(RecoverPassword);



