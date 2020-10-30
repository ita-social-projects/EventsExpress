import React from "react";
import { Field, reduxForm } from "redux-form";

import SelectField from "material-ui/SelectField";
import MenuItem from "material-ui/MenuItem";
import Button from "@material-ui/core/Button";

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

const EditPeriodicity = props => {
    const { handleSubmit, pristine, submitting } = props;
    return (
        <form onSubmit={handleSubmit}>
            <div>
                <Field
                    name="Periodicity"
                    component={renderSelectField}
                    label="Chose Periodicity"
                >
                    <MenuItem value="0" primaryText="NotPeriodic" />
                    <MenuItem value="1" primaryText="Daily" />
                    <MenuItem value="2" primaryText="Weekly" />
                    <MenuItem value="3" primaryText="Monthly" />
                    <MenuItem value="4" primaryText="Yearly" />
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
    form: "EditPeriodicity" // a unique identifier for this form
    
})(EditPeriodicity);
