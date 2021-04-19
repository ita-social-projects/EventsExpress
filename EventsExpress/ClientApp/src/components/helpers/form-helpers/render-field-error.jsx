import React from 'react';
import FormHelperText from '@material-ui/core/FormHelperText';

export default ({ touched, error }) => {
    if (!(touched && error)) {
        return null;
    } else {
        return (
            <FormHelperText
                style={{ color: "#f44336" }}
            >
                {error}
            </FormHelperText>
        );
    }
}
