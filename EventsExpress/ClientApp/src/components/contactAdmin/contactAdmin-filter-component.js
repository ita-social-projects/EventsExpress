import React, { Component } from 'react';
import { reduxForm, Field } from 'redux-form';
import Button from "@material-ui/core/Button";
import { renderDatePicker } from '../helpers/form-helpers';
import filterHelper from '../helpers/filterHelper';
import './contactAdmin-filter.css';
import ContactAdminFilterStatus from './contactAdmin-filter-status-component';
import issueStatusEnum from '../../constants/IssueStatusEnum';

class ContactAdminFilter extends Component {
    constructor(props) {
        super(props);
        this.state = {
            needInitializeValues: true,
        };
    }

    componentDidUpdate(prevProps) {
        const initialValues = this.props.initialFormValues;

        if (!filterHelper.compareObjects(initialValues, prevProps.initialFormValues)
            || this.state.needInitializeValues) {
            this.props.initialize({
                dateFrom: initialValues.dateFrom,
                dateTo: initialValues.dateTo,
                status: initialValues.status,
            });
            this.setState({
                ['needInitializeValues']: false
            });
        }
    }

    render() {
        const { form_values } = this.props;
        let values = form_values || {};

        return <>
            <div className="sidebar-filter" >
                <form onSubmit={this.props.handleSubmit} className="box">
                    {<>
                        <div className="form-group">
                            <Field
                                name='dateFrom'
                                label='From'
                                minValue={new Date(2000, 1, 1, 12, 0, 0)}
                                component={renderDatePicker}
                            />
                        </div>
                        <div className="form-group">
                            <Field
                                name='dateTo'
                                label='To'
                                minValue={new Date(values.dateFrom)}
                                component={renderDatePicker}
                            />
                        </div>
                        <div className="form-group">
                            <Field name="status"
                                component={ContactAdminFilterStatus}
                                options={[issueStatusEnum.Open, issueStatusEnum.InProgress, issueStatusEnum.Resolve]}
                            />
                        </div>
                    </>
                    }
                    <div className="d-flex">
                        <Button
                            fullWidth={true}
                            color="primary"
                            onClick={this.props.onReset}
                            disabled={this.props.submitting}
                        >
                            Reset
                        </Button>
                        <Button
                            fullWidth={true}
                            type="submit"
                            color="primary"
                            disabled={this.props.submitting}
                        >
                            Search
                        </Button>
                    </div>
                </form>
            </div>
        </>
    }
}

ContactAdminFilter = reduxForm({
    form: 'contactAdmin-filter-form',
})(ContactAdminFilter);

export default ContactAdminFilter;
