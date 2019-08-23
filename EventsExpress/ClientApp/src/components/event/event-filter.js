import React, { Component } from 'react';
import { renderTextField, renderDatePicker, renderMultiselect, radioButton } from '../helpers/helpers';
import { reduxForm, Field } from 'redux-form';
import Module from '../helpers';
import Button from "@material-ui/core/Button";
import Radio from '@material-ui/core/Radio';
const { validate } = Module;

class EventFilter extends Component {
    
    render() {
        const { all_categories, form_values,current_user } = this.props;
        let values = form_values || {};
        return <>
            <form onSubmit={this.props.handleSubmit} className="box">
                <Field name='search' component={renderTextField} type="input" label="Search" />
                    <span>From<br/><Field name='dateFrom' component={renderDatePicker} /></span>              
                    {values.dateFrom != null &&
                <span>To<br/><Field name='dateTo' defaultValue={values.dateFrom} minValue={values.dateFrom} component={renderDatePicker} /></span>
              }
                <Field
                    name="categories"
                    component={renderMultiselect}
                    data={all_categories.data}
                    valueField={"id"}
                    textField={"name"}
                    className="form-control mt-2"
                    placeholder='#hashtags'
                />
                {
                    (current_user.role=="Admin")?
                        <Field name="status" component={radioButton}>
                            <Radio value="true" label="All" />
                            <Radio value="true" label="Unblocked" />
                            <Radio value="true" label="Blocked" />
                        </Field>
                        :null
                }
                <Button fullWidth={true} type="submit" value="Login" color="primary" disabled={this.props.submitting}>
                    Search
                </Button>
            </form>
        </>
    }
}


export default EventFilter = reduxForm({
    form: 'event-filter-form'
})(EventFilter);
