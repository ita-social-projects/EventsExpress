import React from "react";
import { makeStyles } from '@material-ui/core/styles';
import InputLabel from '@material-ui/core/InputLabel';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import { renderFieldError } from '.';

export default ({ input, label, meta: { touched, error, invalid }, minWidth, children }) => {
    const useStyles = makeStyles((theme) => ({
        formControl: {  minWidth: minWidth }
    }));
    return (
        <FormControl variant="outlined" className={useStyles().formControl} >
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
