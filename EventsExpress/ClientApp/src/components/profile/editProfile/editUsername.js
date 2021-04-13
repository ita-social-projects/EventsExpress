import React from 'react';
import { Field, reduxForm } from 'redux-form';
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import ErrorMessages from '../../shared/errorMessage';

const renderTextField = (
    { input, label, meta: { touched, error }, ...custom },
) => (
        <TextField
            hintText={label}
            floatingLabelText={label}
            errorText={touched && error}
            {...input}
            {...custom}
        />
    );

const EditUsername = props => {
    const { handleSubmit, pristine, reset, submitting } = props;
    return (
        <form onSubmit={handleSubmit}>
            <div>
                <Field
                    name="UserName"
                    component={renderTextField}
                    label="UserName"
                />
                {
                    props.error &&
                    <ErrorMessages error={props.error} className="text-center" />
                }
            </div>
            <div>
                <Button type="submit" color="primary" disabled={pristine || submitting}>Submit</Button>
                <Button type="button" color="primary" disabled={pristine || submitting} onClick={reset}>
                    Clear
                </Button>
            </div>
        </form>
    );
};

export default reduxForm({
    form: 'EditUsername',
    
})(EditUsername);