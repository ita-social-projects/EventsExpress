import React, {Component} from 'react';
import {reduxForm, Field} from 'redux-form';
import Button from "@material-ui/core/Button";
import {
    renderTextField,
    renderDatePicker,
    renderMultiselect
} from '../helpers/helpers';
import eventHelper from '../helpers/eventHelper';
import MapModal from './map-modal';
import './event-filter.css';
import DisplayMap from '../event/map/display-map';
import EventFilterStatus from './event-filter-status';
import eventStatusEnum from '../../constants/eventStatusEnum';

class EventFilter extends Component {
    constructor(props) {
        super(props);
        this.state = {
            viewMore: false,
            needInitializeValues: true,
        };
    }

    componentDidUpdate(prevProps) {
        const initialValues = this.props.initialFormValues;

        if (!eventHelper.compareObjects(initialValues, prevProps.initialFormValues)
            || this.state.needInitializeValues) {
            this.props.initialize({
                keyWord: initialValues.keyWord,
                dateFrom: initialValues.dateFrom,
                dateTo: initialValues.dateTo,
                categories: initialValues.categories,
                statuses: initialValues.statuses,
                radius: initialValues.radius,
                selectedPos: initialValues.selectedPos != null ? initialValues.selectedPos : {lat: null, lng: null},
            });
            this.setState({
                ['needInitializeValues']: false
            });
        }
    }

    render() {
        const {all_categories, form_values, current_user} = this.props;
        let values = form_values || {};
        let options = [
            {value: eventStatusEnum.Active, text: "Active"},
            {value: eventStatusEnum.Blocked, text: "Blocked"},
            {value: eventStatusEnum.Canceled, text: "Canceled"}
        ];

        return <>
            <div className="sidebar-filter">
                <form onSubmit={this.props.handleSubmit} className="box">
                    <div className="form-group">
                        <Field
                            name='keyWord'
                            component={renderTextField}
                            type="input"
                            label="Keyword"
                        />
                    </div>
                    {this.state.viewMore &&
                    <>
                        <div className="form-group">
                            <Field
                                name='dateFrom'
                                label='From'
                                minValue={new Date()}
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
                        <div className="form-group">
                            {current_user.roles.includes("Admin") &&
                            <Field name="statuses"
                                   component={EventFilterStatus}
                                   options={options}
                            />
                            }
                        </div>

                        <div>
                            <MapModal
                                initialize={this.props.initialize}
                                initialValues={this.props.initialFormValues}
                                values={values}
                                reset={this.props.onReset}/>
                        </div>

                        <div className="d-flex">

                            {values.selectedPos &&
                            values.selectedPos.lat &&
                            <div>
                                <p>
                                    Radius:
                                    {values.radius} km
                                </p>
                                <p>
                                    Location:
                                </p>
                                <DisplayMap
                                    location={{latitude: values.selectedPos.lat, longitude: values.selectedPos.lng}}
                                />
                            </div>
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
                                this.setState({viewMore: !this.state.viewMore});
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
                        {current_user.id &&
                        <Button
                            fullWidth={true}
                            color="primary"
                            onClick={this.props.onLoadUserDefaults}
                            disabled={this.props.submitting}
                        >
                            Favorite
                        </Button>
                        }
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

EventFilter = reduxForm({
    form: 'event-filter-form',
})(EventFilter);

export default EventFilter;
