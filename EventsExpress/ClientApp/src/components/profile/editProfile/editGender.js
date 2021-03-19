import React from "react";
import { Field, reduxForm } from "redux-form";
import SelectField from "material-ui/SelectField";
import MenuItem from "material-ui/MenuItem";
import Button from "@material-ui/core/Button";
import ErrorMessages from '../../shared/errorMessage';

const renderSelectField = ({
    input,
    label,
    meta: { touched, error },
    children,
    ...custom
}) => (
        <SelectField
            floatingLabelText={label}
            errorText={touched && error}
            {...input}
            onChange={(event, index, value) => input.onChange(value)}
            children={children}
            {...custom}
        />
    );

let EditGender = props => {
    const { handleSubmit, pristine, submitting } = props;
    return (
        <form name= "EditGender" onSubmit={handleSubmit}>
            <div>
                <Field
                    name="Gender"
                    component={renderSelectField}
                    label="Chose Gender"
                >
                    <MenuItem value="0" primaryText="Other" />
                    <MenuItem value="1" primaryText="Male" />
                    <MenuItem value="2" primaryText="Female" />
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
    form: "EditGender" // a unique identifier for this form
    
})(EditGender);
