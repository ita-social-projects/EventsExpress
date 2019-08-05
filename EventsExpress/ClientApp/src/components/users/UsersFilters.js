import React, { Component } from 'react';
import { renderTextField, renderDatePicker, renderMultiselect } from '../helpers/helpers';
import { reduxForm, Field } from 'redux-form';
import Button from "@material-ui/core/Button";
import { renderSelectField } from '../helpers/helpers'
class UsersFilters extends Component {
    render() {
        return <>
            <form onSubmit={this.props.handleSubmit} className="box">
                <Field name='search' component={renderTextField} type="input" label="Search:" />
                <Field
                    name="role"
                    component={renderSelectField}
                    label="Role"
                >
                    <option value="" />
                    <option value={"Admin"}>Admin</option>
                    <option value={"User"}>User</option>
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