import React from "react";
import { Field, reduxForm } from "redux-form";
import Module from '../../helpers';
import Button from "@material-ui/core/Button";
import ErrorMessages from '../../shared/errorMessage';

const { validate, renderMyDatePicker } = Module;
const EditBirthday = props => {
    const { handleSubmit, pristine, reset, submitting } = props;
    return (
        <form onSubmit={handleSubmit}>
            <div>
                <Field
                    name="Birthday"
                    id="date"
                    label="Birthday"
                    type="date"
                    component={renderMyDatePicker}
                    InputLabelProps={{
                        shrink: true
                    }}
                />
                {
                    props.error &&
                    <ErrorMessages error={props.error} className="text-center" />
                }
            </div>
            <div>
                <Button type="submit" color="primary" disabled={pristine || submitting}>
                    Submit
        </Button>
                <Button type="button" color="primary" disabled={pristine || submitting} onClick={reset}>
                    Clear
        </Button>
            </div>
        </form>
    );
};

export default reduxForm({
    form: "EditBirthday", // a unique identifier for this form
    validate
})(EditBirthday);
