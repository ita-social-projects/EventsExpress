import React, { Component } from 'react';
import MenuItem from '@material-ui/core/MenuItem';
import FormHelperText from '@material-ui/core/FormHelperText';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import { reduxForm, Field } from 'redux-form';

class ContactUs extends Component{
    constructor(props){
        super(props)
    }

    render(){
        return(
            <form>
                <div className="text text-2 pl-md-4" >
                    <FormControl className={classes.formControl}>
                        <InputLabel htmlFor="age-simple">Age</InputLabel>
                        <Select
                        value={values.age}
                        onChange={handleChange}
                        inputProps={{
                            name: 'age',
                            id: 'age-simple',
                        }}
                        >
                        <MenuItem value={10}>Ten</MenuItem>
                        <MenuItem value={20}>Twenty</MenuItem>
                        <MenuItem value={30}>Thirty</MenuItem>
                        </Select>
                    </FormControl>
                    <Field name='description' component={renderTextArea} type="input" label="Description" />
                    
                </div>
                <Button fullWidth={true} type="submit" color="primary" disabled={this.props.submitting}>
                        Send
                </Button>
            </form>
        )
    }
}

export default reduxForm({
    form: "ContactUs", // a unique identifier for this form
    validate
})(ContactUs);