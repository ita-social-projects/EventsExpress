import React from "react";
import { Field, reduxForm } from "redux-form";
import Button from "@material-ui/core/Button";
import { renderSelectField } from '../../helpers/form-helpers'
import ErrorMessages from '../../shared/errorMessage';

let EditGender = props => {
    const { handleSubmit, pristine, submitting } = props;
    return (
        <form name= "EditGender" onSubmit={handleSubmit}>
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
                {
                    props.error &&
                    <ErrorMessages error={props.error} className="text-center" />
                }
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
    form: "EditGender"    
})(EditGender);
