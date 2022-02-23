import React from 'react'
import { Field } from 'redux-form';
import Radio from '@material-ui/core/Radio';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import { radioButton } from '../../../helpers/form-helpers';

const RadioGroup = ({ name, label, source }) => (
    <Field
        name={name}
        component={radioButton}
        parse={parseInt}
    >
        <label className="font-weight-bold">
            {label}
        </label>
        {Array.from(source).map(([key, value]) => (
            <FormControlLabel
                key={value}
                value={key}
                control={<Radio />}
                label={value}
            />
        ))}
    </Field>
);

export default RadioGroup;
