import React from "react";
import Select from '@material-ui/core/Select';
import FormControl from '@material-ui/core/FormControl';
import InputLabel from '@material-ui/core/InputLabel';
import { renderFieldError } from '.';

export default ({ input, label, text, data, meta: { touched, error }, children, ...custom }) => {
    return (
        <FormControl error={touched && error}>
            <InputLabel htmlFor="age-native-simple">{text}</InputLabel>
            <Select
                native
                {...input}
                onBlur={() => input.onBlur()}
                {...custom}
                inputProps={{
                    name: text.toLowerCase() + 'Id',
                    id: 'age-native-simple'
                }}
            >
                {data.map(x => <option key={x.value} value={x.value}>{x.label}</option>)}
            </Select>
            {renderFieldError({ touched, error })}
        </FormControl>
    );
}
