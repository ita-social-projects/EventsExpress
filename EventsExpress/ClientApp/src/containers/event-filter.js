import React, { Component } from 'react';
import { connect } from 'react-redux';
import { renderTextField } from '../components/helpers/helpers';
import { reduxForm, Field, getFormValues } from 'redux-form';
import EventFilter from '../components/event/event-filter';
import { get_events } from '../actions/event-list';
import history from '../history';

import get_categories from '../actions/category-list';

class EventFilterWrapper extends Component {

    componentWillMount(){
        this.props.get_categories();
    }
    
    onSubmit = (filters) => {  
        var search_string = '?page=1';
        if (filters != null) {
            if (filters.search != null) {
                search_string += '&keyWord=' + filters.search;
            }
            if (filters.dateFrom != null) {
                search_string += '&dateFrom=' + new Date(filters.dateFrom).toDateString();
            }
            if (filters.dateTo != null) {
                search_string += '&dateTo=' + new Date(filters.dateTo).toDateString();
            }
            if (filters.categories != null) {
                var categories = '';
                for (var i = 0; i < filters.categories.length; i++) {
                    categories += filters.categories[i].id + ',';
                }
                search_string += '&categories=' + categories;
            }
        }
        this.props.search(search_string); 
        history.push(window.location.pathname + search_string);
    }

    render() {
        return <>
            <EventFilter 
            all_categories={this.props.all_categories}
            onSubmit={this.onSubmit}
            form_values={this.props.form_values} 
            current_user={this.props.current_user}
            />
        </>
    }
}

const mapStateToProps = (state) => ({
    all_categories: state.categories,
    form_values: getFormValues('event-filter-form')(state),
    current_user: state.user
});

const mapDispatchToProps = (dispatch) => {
    return {
        search: (values) => dispatch(get_events(values)),
        get_categories: () => dispatch(get_categories())
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(EventFilterWrapper);