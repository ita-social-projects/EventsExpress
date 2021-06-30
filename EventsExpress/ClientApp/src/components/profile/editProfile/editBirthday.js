import React from "react";
import { Field, reduxForm } from "redux-form";
import Button from "@material-ui/core/Button";
import moment from "moment";
import ErrorMessages from '../../shared/errorMessage';
import { renderDatePicker } from '../../helpers/form-helpers';
import { fieldIsRequired } from '../../helpers/validators/required-fields-validator';

const validate = values => {
    const errors = {}
    const requiredFields = [
        'birthday'
    ]

    if (new Date(values.Birthday).getTime() >= Date.now()) {
        errors.Birthday = 'Date is incorrect';
    }
    return {
        ...fieldIsRequired(values, requiredFields),
        ...errors
    }
}

const EditBirthday = props => {
    const minValue = moment(new Date()).subtract(115, 'years')
    const maxValue = moment(new Date()).subtract(15, 'years')
    const { handleSubmit, pristine, reset, submitting } = props;
    return (
        <form onSubmit={handleSubmit}>
            <div>
                <Field
                    name="birthday"
                    id="date"
                    label="Birthday"
                    minValue={minValue}
                    maxValue={maxValue}
                    component={renderDatePicker}
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
    form: "EditBirthday",
    validate
})(EditBirthday);
