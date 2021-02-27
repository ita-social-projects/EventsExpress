import React, { Component } from 'react';
import { connect } from 'react-redux';
import { getFormValues, reset } from 'redux-form';
import EventFilter from '../components/event/event-filter';
import { updateEventsFilters } from '../actions/event-list-action';
import get_categories from '../actions/category/category-list';
import eventHelper from '../components/helpers/eventHelper';

class EventFilterWrapper extends Component {
    componentWillMount() {
        this.props.get_categories();
    }

    onReset = () => {
        this.props.reset_filters();
        this.props.updateEventsFilters(eventHelper.getDefaultEventFilter());
    }

    onLoadUserDefaults = () => {
        this.props.reset_filters();
        const defaultFilter = {
            ...eventHelper.getDefaultEventFilter(),
            categories: this.props.current_user.categories.map(item => item.id),
        };

        this.props.updateEventsFilters(defaultFilter);
    }

    onSubmit = (filters) => {
        filters = eventHelper.trimUndefinedKeys(filters);
        Object.entries(filters).forEach(function ([key, value]) {
            switch (key) {
                case 'page':
                    this.props.events.filter[key] = 1;
                case 'dateFrom':
                case 'dateTo':
                    this.props.events.filter[key] = new Date(value).toDateString();
                    break;
                case 'categories':
                    this.props.events.filter[key] = value.map(item => item.id);
                    break;
                case 'radius':
                    this.props.events.filter[key] = value;
                    break;
                case 'selectedPos':
                    var x = value.lat;
                    var y = value.lng;
                    this.props.events.filter['x'] = x;
                    this.props.events.filter['y'] = y;
                    this.props.events.filter[key] = { lat: x, lng: y }
                    break;
                default:
                    this.props.events.filter[key] = value;
                    break;
            }
        }.bind(this));

        this.props.updateEventsFilters(this.props.events.filter);
    }

    buildInitialFormValues = () => {
        const filter = eventHelper.trimUndefinedKeys(this.props.events.filter);
        let values = Object.assign({}, filter);

        if (filter.categories.length) {
            values.categories = this.props.all_categories.data.filter(item =>
                filter.categories.some(filterItem => filterItem === item.id)
            );
        }

        return values;
    };
    render() {
        const initialFormValues = this.buildInitialFormValues();
        return <>
            <EventFilter
                all_categories={this.props.all_categories}
                onLoadUserDefaults={this.onLoadUserDefaults}
                onSubmit={this.onSubmit}
                onReset={this.onReset}
                form_values={this.props.form_values}
                current_user={this.props.current_user}
                initialFormValues={initialFormValues}
            />
        </>
    }
}

const mapStateToProps = (state) => ({
    all_categories: state.categories,
    events: state.events,
    form_values: getFormValues('event-filter-form')(state),
    current_user: state.user
});

const mapDispatchToProps = (dispatch) => {
    return {
        updateEventsFilters: (value) => dispatch(updateEventsFilters(value)),
        get_categories: () => dispatch(get_categories()),
        reset_filters: () => {
            dispatch(reset('event-filter-form'));
        },
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EventFilterWrapper);
