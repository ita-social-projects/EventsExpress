import React from "react";
import { makeStyles } from '@material-ui/core/styles';
import InputLabel from '@material-ui/core/InputLabel';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import { renderFieldError } from '.';

const useStyles210 = makeStyles((theme) => ({
    formControl: {
        minWidth: 210,
    },
}));

const useStyles100 = makeStyles((theme) => ({
    formControl: {
        minWidth: 100,
    },
}));

export default ({ input, label, meta: { touched, error, invalid }, children, fullWidth }) => {
    const classes = useStyles210();
    const classesInventories = useStyles100();
    return (
        <FormControl variant="outlined" className={fullWidth ? classes.formControl : classesInventories.formControl} >
            <InputLabel>{label}</InputLabel>
            <Select
                {...input}
                native
                fullWidth={true}
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
