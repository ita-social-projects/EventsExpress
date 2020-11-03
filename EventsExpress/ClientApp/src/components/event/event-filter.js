import React, { Component } from 'react';
import { renderTextField, renderDatePicker, renderMultiselect, radioButton } from '../helpers/helpers';
import { reduxForm, Field } from 'redux-form';
import Button from "@material-ui/core/Button";
import Radio from '@material-ui/core/Radio';
import './event-filter.css';

class EventFilter extends Component {
    constructor(props) {
        super(props);

        this.state = { viewMore: false }
    }

    //toggleMode = val => !val;

    render() {
        const { all_categories, form_values,current_user } = this.props;
        let values = form_values || {};
        
        return <>
            <div className="sidebar-filter" >
                <form onSubmit={this.props.handleSubmit} className="box">
                    <div className="form-group">
                        <Field 
                            name='search' 
                            component={renderTextField} 
                            type="input" 
                            label="Keyword" 
                        />
                    </div>          

                    {this.state.viewMore && <> 
                        
                        <div className="form-group">
                            <div>From</div>
                            <Field 
                                name='dateFrom' 
                                component={renderDatePicker} 
                            />
                        </div>
                        <div className="form-group">
                            <div>To</div>
                            <Field 
                                name='dateTo' 
                                defaultValue={values.dateFrom} 
                                minValue={values.dateFrom} 
                                component={renderDatePicker} 
                            />
                        </div>
                        
                        <div className="form-group">
                            <Field
                                name="categories"
                                component={renderMultiselect}
                                data={all_categories.data}
                                valueField={"id"}
                                textField={"name"}
                                className="form-control mt-2"
                                placeholder='#hashtags'
                            />
                        </div>

                        {current_user.role=="Admin" && (
                            <div className="form-group">
                                <Field name="status" component={radioButton}>
                                    <Radio value="true" label="All" />
                                    <Radio value="true" label="Unblocked" />
                                    <Radio value="true" label="Blocked" />
                                </Field>
                            </div>
                        )}

                    </>}          
                    
                    <div>
                        <Button
                            key={this.state.viewMore} 
                            fullWidth={true} 
                            color="info" 
                            onClick={() => {this.setState({viewMore: !this.state.viewMore})}}
                        >
                            {this.state.viewMore ? 'less...' : 'more filters...'}
                        </Button>
                    </div>
                    <div className="d-flex">
                        <Button fullWidth={true} color="primary" onClick={this.props.onReset} disabled={this.props.pristine || this.props.submitting}>
                            Reset
                        </Button>

                        <Button fullWidth={true} type="submit" color="primary" disabled={this.props.pristine || this.props.submitting}>
                            Search
                        </Button>
                    </div>

                </form>
            </div>
        </>
    }
}


export default EventFilter = reduxForm({
    form: 'event-filter-form'
})(EventFilter);
