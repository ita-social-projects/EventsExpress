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
                <Field
                    name="Periodicity"
                    component={renderSelectField}
                    label="Periodicity"
                >
                    <MenuItem value="1" primaryText="Daily" />
                    <MenuItem value="2" primaryText="Weekly" />
                    <MenuItem value="3" primaryText="Monthly" />
                    <MenuItem value="4" primaryText="Yearly" />
                </Field>
        </form>
    );
};

export default reduxForm({
    form: "EditPeriodicity" // a unique identifier for this form
    
})(EditPeriodicity);
