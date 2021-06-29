import React from "react";
import { Field, reduxForm } from "redux-form";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import DialogContentText from '@material-ui/core/DialogContentText';
import ErrorMessages from '../shared/errorMessage';
import { renderTextField } from '../helpers/form-helpers';
import { emailField } from '../helpers/validators/email-field-validator';

const validate = values => {
    return emailField(values)
}

class RecoverPassword extends React.Component {

    render() {
        const { handleSubmit, pristine, reset, submitting, error } = this.props;
        return (
            <form onSubmit={handleSubmit}>
                <DialogContentText>
                    If you forgot your password please enter your  <br /> email address here. We will send you new<br /> password.
                  </DialogContentText>
                <div>
                    <Field
                        name="email"
                        component={renderTextField}
                        label="E-mail:"
                    />
                    {
                        error &&
                        <ErrorMessages error={error} className="text-center" />
                    }
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
    form: "recoverPassword",
    validate
})(RecoverPassword);



