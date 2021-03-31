import React from "react";
import { Field, reduxForm } from "redux-form";
import Button from "@material-ui/core/Button";
import { renderSelectField } from '../../helpers/form-helpers'

const EditGender = props => {
    const { handleSubmit, pristine, submitting } = props;
    return (
        <form onSubmit={handleSubmit}>
            <div>
                <Field
                    name="gender"
                    component={renderSelectField}
                    label="Gender"
                >
                    <option aria-label="None" value="" />
                    <option value="0">Other</option>
                    <option value="1">Male</option>
                    <option value="2">Female</option>
                </Field>
            </div>

            <div>
                <Button type="submit" color="primary" disabled={pristine || submitting}>
                    Submit
        </Button>
            </div>
        </form>
    );
};

export default reduxForm({
    form: "EditGender" // a unique identifier for this form
    
})(EditGender);
