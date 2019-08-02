import React, { Component } from 'react';
import { connect } from 'react-redux';
import { renderTextField } from '../components/helpers/helpers';
import { reduxForm, Field } from 'redux-form';
import EventFilter from '../components/event/event-filter';
import get_events from '../actions/event-list';

class EventFilterWrapper extends Component {

    onSubmit = (filters) => {
        console.log(filters);       
        var search_string = '';
        if (filters != null) {
            if (filters.search != null) {
                search_string += '&keyWord=' + filters.search;
            }
            if (filters.date_from != null) {
                search_string += '&dateFrom=' + new Date(filters.date_from).toDateString();
            }
            if (filters.date_to != null) {
                search_string += '&dateTo=' + new Date(filters.date_to).toDateString();
            }
            if (filters.categories != null) {
                var categories = '';
                for (var i = 0; i < filters.categories.length; i++) {
                    categories += filters.categories[i].id + ',';
                }
                search_string += '&categories=' + categories;
            }
        }
        this.props.search(window.location.search + search_string); 
        console.log(window.location.search + search_string)
        window.location.search += search_string;
        
    }

    render() {
        return <>
            <EventFilter 
            all_categories={this.props.all_categories}
            onSubmit={this.onSubmit} />
        </>
    }
}

const mapStateToProps = (state) => ({
    all_categories: state.categories
});

const mapDispatchToProps = (dispatch) => {
    return {
        search: (values) => dispatch(get_events(values))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(EventFilterWrapper);