import React, { Component } from "react";
import { Field, reduxForm } from "redux-form";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import Module from '../helpers';
import { connect } from 'react-redux';




const { validate, renderTextField, asyncValidate } = Module;

class RecoverPassword extends React.Component {
    constructor(props) {
        super(props)
    }

    render() {
        const { handleSubmit, pristine, reset, submitting } = this.props;
        return (
            <form onSubmit={handleSubmit}>
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
    form: "recoverPass-form",
    validate,
    asyncValidate
})(RecoverPassword);



