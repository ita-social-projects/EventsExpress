import React from "react";
import { Field, reduxForm } from "redux-form";

import SelectField from "material-ui/SelectField";
import MenuItem from "material-ui/MenuItem";
import asyncValidate from "./asyncValidate";
import validate from "./validate";

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

const editGender = props => {
    const { handleSubmit, pristine, submitting } = props;
    return (
        <form onSubmit={handleSubmit}>
            <div>
                <Field
                    name="chooseGender"
                    component={renderSelectField}
                    label="Chose Gender"
                >
                    <MenuItem value="ff0000" primaryText="Red" />
                    <MenuItem value="00ff00" primaryText="Green" />
                    <MenuItem value="0000ff" primaryText="Blue" />
                </Field>
            </div>

            <div>
                <button type="submit" disabled={pristine || submitting}>
                    Submit
        </button>
            </div>
        </form>
    );
};

export default reduxForm({
    form: "MaterialUiForm", // a unique identifier for this form
    
})(editGender);
