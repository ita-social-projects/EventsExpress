import React, { Component } from 'react';
import { reduxForm, Field } from 'redux-form';
import Button from "@material-ui/core/Button";
import { renderDatePicker } from '../helpers/helpers';
import filterHelper from '../helpers/filterHelper';
import './event-filter.css';
import ContactUsFilterStatus from './contactUs-filter-status-component';
import issueStatusEnum from '../../constants/issueStatusEnum';

class ContactUsFilter extends Component {
    constructor(props) {
        super(props);
        this.state = {
            viewMore: false,
            needInitializeValues: true,
        };
    }

    componentDidUpdate(prevProps) {
        const initialValues = this.props.initialFormValues;

        if (!filterHelper.compareObjects(initialValues, prevProps.initialFormValues)
            || this.state.needInitializeValues) {
            this.props.initialize({
                keyWord: initialValues.keyWord,
                dateCreated: initialValues.dateCreated,
                statuses: initialValues.status,
            });
            this.setState({
                ['needInitializeValues']: false
            });
        }
    }

    render() {
        const { form_values, current_user } = this.props;
        let values = form_values || {};

        return <>
            <div className="sidebar-filter" >
                <form onSubmit={this.props.handleSubmit} className="box">
                    {this.state.viewMore &&
                        <>
                            <div className="form-group">
                                <Field
                                    name='dateCreated'
                                    label='Date created'
                                    minValue={new Date(values.dateCreated)}
                                    component={renderDatePicker}
                                />
                            </div>
                            <div className="form-group">
                                {current_user.role === "Admin" &&
                                    <Field name="status"
                                        component={ContactUsFilterStatus}
                                        options={[issueStatusEnum.Open, issueStatusEnum.InProgress, issueStatusEnum.Resolve]}
                                    />
                                }
                            </div>
                        </>
                    }
                    <div>
                        <Button
                            key={this.state.viewMore}
                            fullWidth={true}
                            color="secondary"
                            onClick={() => {
                                this.setState({ viewMore: !this.state.viewMore });
                            }}
                        >
                            {this.state.viewMore ? 'less...' : 'more filters...'}
                        </Button>
                    </div>
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

ContactUsFilter = reduxForm({
    form: 'contactUs-filter-form',
})(EventFilter);

export default ContactUsFilter;
