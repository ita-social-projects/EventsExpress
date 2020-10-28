import React, { Component } from 'react';
import { connect } from 'react-redux';
import { getFormValues, reset } from 'redux-form';
import EventFilter from '../components/event/event-filter';
import { updateEventsFilters } from '../actions/event-list';
import get_categories from '../actions/category-list';
import history from '../history';

class EventFilterWrapper extends Component {
    componentWillMount() {
        this.props.get_categories();
    }

    onReset = () => {
        this.props.reset_filters();
        this.props.events.searchParams = {
            page: '1',
            keyWord: undefined,
            dateFrom: undefined,
            dateTo: undefined,
            categories: undefined,
            status: undefined,
        };

        this.props.updateEventsFilters(this.props.events.searchParams);
    }

    onSubmit = (filters) => {
        Object.keys(filters).forEach(function (key) {
            switch (key) {
                case 'dateFrom':
                case 'dateTo':
                    this.props.events.searchParams[key] = new Date(filters[key]).toDateString();
                    break;
                case 'categories':
                    let categories = '';
                    filters[key].forEach(function (category) {
                        categories += `${category.id},`;
                    });
                    this.props.events.searchParams[key] = categories;
                    break;
                case 'status':
                    if (filters[key] === "all") {
                        this.props.events.searchParams[key] = 'All=true';
                    }
                    if (filters[key] === 'blocked') {
                        this.props.events.searchParams[key] = 'Blocked=true';
                    }
                    if (filters[key] === 'unblocked') {
                        this.props.events.searchParams[key] = 'Unblocked=true';
                    }
                    break;
                default:
                    this.props.events.searchParams[key] = filters[key];
            }
        }.bind(this));

        this.props.updateEventsFilters(this.props.events.searchParams);
    }

    render() {
        return <>
            <EventFilter
                all_categories={this.props.all_categories}
                onSubmit={this.onSubmit}
                onReset={this.onReset}
                form_values={this.props.form_values}
                current_user={this.props.current_user}
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

export default connect(mapStateToProps, mapDispatchToProps)(EventFilterWrapper);
