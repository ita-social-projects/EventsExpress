import React from "react";
import { makeStyles } from '@material-ui/core/styles';
import InputLabel from '@material-ui/core/InputLabel';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import { renderFieldError } from './';

const useStyles = makeStyles((theme) => ({
    formControl: {
        minWidth: 210,
    },
}));

export default ({ input, label, meta: { touched, error, invalid }, children }) => {
    const classes = useStyles();
    return (
        <FormControl variant="outlined" className={classes.formControl} >
            <InputLabel>{label}</InputLabel>
            <Select
                {...input}
                native
                value={input.value}
                onChange={input.onChange}
                label={label}
                children={children}
                error={touched && invalid}
            >
            </Select>
            
            {renderFieldError({ touched, error })}
        </FormControl>
    );
}
