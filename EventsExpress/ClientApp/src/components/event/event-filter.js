import React, { Component } from 'react';
import { renderTextField, renderDatePicker, renderMultiselect } from '../helpers/helpers';
import { reduxForm, Field } from 'redux-form';

import Button from "@material-ui/core/Button";

class EventFilter extends Component {
    render() {
        const { all_categories } = this.props;
        return <>
            <form onSubmit={this.props.handleSubmit} className="box">
                <Field name='search' component={renderTextField} type="input" label="Search" />
                <p className="meta">
                    <span>From<br/><Field name='date_from' component={renderDatePicker} /></span>
                    <span>To<br/><Field name='date_to' component={renderDatePicker} /></span>
                </p>
                <Field
                    name="categories"
                    component={renderMultiselect}
                    data={all_categories.data}
                    valueField={"id"}
                    textField={"name"}
                    className="form-control mt-2"
                    placeholder='#hashtags'
                />
                <Button fullWidth={true} type="submit" value="Login" color="primary" disabled={this.props.submitting}>
                    Search
                </Button>
            </form>
        </>
    }
}


export default EventFilter = reduxForm({
    form: 'event-filter-form',
})(EventFilter);
