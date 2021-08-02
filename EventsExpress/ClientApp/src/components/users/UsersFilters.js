import React, { Component } from 'react';
import { reduxForm, Field } from 'redux-form';
import Button from "@material-ui/core/Button";
import Radio from '@material-ui/core/Radio';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import { renderSelectField, renderTextField, radioButton } from '../helpers/form-helpers';

class UsersFilters extends Component {
    render() {
        return <>
            <form onSubmit={this.props.handleSubmit} className="box">
                <Field name='search' component={renderTextField} type="input" label="Search:" />
                <Field
                    minWidth={150}
                    name="role"
                    component={renderSelectField}
                    label="Role"
                >
                    <option value="" />
                    <option value={"Admin"}>Admin&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</option>
                    <option value={"User"}>User</option>
                </Field>
                <br/>
                <Field
                    minWidth={150}
                    name='PageSize'
                    component={renderSelectField}
                    label="PageSize"
                >
                    <option value="" />
                    <option value={"5"} >5&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</option>
                    <option value={"10"}>10</option>
                    <option value={"15"}>15</option>
                </Field>
                <br/>
                <Field name="status" component={radioButton}>
                    <FormControlLabel value="blocked" control={<Radio />} label="Blocked" />
                    <FormControlLabel value="active" control={<Radio />} label="Active" />
                    <FormControlLabel value="all" control={<Radio />} label="All" />
                </Field>
                <Button fullWidth={true} type="submit"  color="primary" disabled={this.props.submitting}>
                    Search
                </Button>
            </form>
        </>
    }
}

export default UsersFilters = reduxForm({
    form: 'users-filter-form',
})(UsersFilters);