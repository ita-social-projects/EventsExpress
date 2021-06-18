import React, { Component } from 'react';
import { renderTextField, radioButton } from '../helpers/helpers';
import { reduxForm, Field } from 'redux-form';
import Button from "@material-ui/core/Button";
import { renderSelectField } from '../helpers/helpers'

class UsersFilters extends Component {
    render() {
        return <>
            <form onSubmit={this.props.handleSubmit} className="box">
                <Field name='search' component={renderTextField} type="input" label="Search:" />
                <Field
                    fullWidth={true}
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
                <Field name="status" component={radioButton} />
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