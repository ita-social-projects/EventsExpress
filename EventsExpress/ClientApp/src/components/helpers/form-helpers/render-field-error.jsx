import React from 'react';
import FormHelperText from '@material-ui/core/FormHelperText';

export default ({ touched, errors }) => {

    if (!(touched && errors)) {
        return null;
    } else {
        return (
            <FormHelperText
                style={{ color: "#f44336" }}
            >
                {errors}
            </FormHelperText>
        );
    }
}
