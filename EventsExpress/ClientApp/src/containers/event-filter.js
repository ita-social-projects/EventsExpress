import React, { Component } from 'react';
import { connect } from 'react-redux';
import { getFormValues, reset } from 'redux-form';
import EventFilter from '../components/event/event-filter';
import { updateEventsFilters } from '../actions/event-list';
import get_categories from '../actions/category-list';
import initialState from '../store/initialState';

class EventFilterWrapper extends Component {
    componentWillMount() {
        this.props.get_categories();
    }

    onReset = () => {
        this.props.reset_filters();
        let defaultFilter = {
            ...initialState.events.filter,
        };

        this.props.updateEventsFilters(defaultFilter);
    }

    onLoadUserDefaults = () => {
        this.props.reset_filters();
        let defaultFilter = {
            ...initialState.events.filter,
            categories: this.props.current_user.categories.map(item => item.id),
        };

        this.props.updateEventsFilters(defaultFilter);
    }

    onSubmit = (filters) => {
        filters = JSON.parse(JSON.stringify(filters));
        Object.entries(filters).forEach(function ([key, value]) {
            switch (key) {
                case 'dateFrom':
                case 'dateTo':
                    this.props.events.filter[key] = new Date(value).toDateString();
                    break;
                case 'categories':
                    this.props.events.filter[key] = value.map(item => item.id);
                    break;
                default:
                    this.props.events.filter[key] = value;
            }
        }.bind(this));

        this.props.updateEventsFilters(this.props.events.filter);
    }

    initialFormValues = (() => {
        const filter = JSON.parse(JSON.stringify(this.props.events.filter));
        let values = Object.assign({}, filter);

        if (filter.categories.length) {
            values.categories = this.props.all_categories.data.filter(item =>
                filter.categories.some(filterItem => filterItem === item.id)
            );
        }

        return values;
    }).call(this);

    render() {
        return <>
            <EventFilter
                all_categories={this.props.all_categories}
                onLoadUserDefaults={this.onLoadUserDefaults}
                onSubmit={this.onSubmit}
                onReset={this.onReset}
                form_values={this.props.form_values}
                current_user={this.props.current_user}
                initialFormValues={this.initialFormValues}
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
