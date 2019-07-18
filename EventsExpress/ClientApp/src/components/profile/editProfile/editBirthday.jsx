import React from "react";
import { Field, reduxForm } from "redux-form";
import TextField from "material-ui/TextField";

const renderTextField = ({
    input,
    label,
    meta: { touched, error },
    ...custom
}) => (
        <TextField
            hintText={label}
            floatingLabelText={label}
            errorText={touched && error}
            {...input}
            {...custom}
        />
    );

const editBirthday = props => {
    const { handleSubmit, pristine, reset, submitting } = props;
    return (
        <form onSubmit={handleSubmit}>
            <div>
                <Field
                    id="date"
                    label="Birthday"
                    type="date"
                    component={renderTextField}
                    defaultValue="2017-05-24"
                    InputLabelProps={{
                        shrink: true
                    }}
                />
            </div>

            <div>
                <button type="submit" disabled={pristine || submitting}>
                    Submit
        </button>
                <button type="button" disabled={pristine || submitting} onClick={reset}>
                    Clear
        </button>
            </div>
        </form>
    );
};

export default reduxForm({
    form: "editBirthday" // a unique identifier for this form
})(editBirthday);
