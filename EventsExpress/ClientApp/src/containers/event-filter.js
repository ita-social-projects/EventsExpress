import React, { Component } from 'react';
import { connect } from 'react-redux';
import {  getFormValues, reset } from 'redux-form';
import EventFilter from '../components/event/event-filter';
import { get_events,get_eventsForAdmin } from '../actions/event-list';
import history from '../history';

import get_categories from '../actions/category-list';

class EventFilterWrapper extends Component {

    componentWillMount(){
        this.props.get_categories();
    }

    onReset = () => {
        this.props.reset_filters();
        var search_string = '?page=1';
        if(window.location.search != search_string){
        if(this.props.current_user.role=="Admin"){
            this.props.AdminSearch(search_string); 
        }
        else{
            this.props.search(search_string); 
        }
        history.push(window.location.pathname + search_string);   
    }
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
            if(filters.status=="all"){
                search_string+='&All='+true;
            }
            if (filters.status == 'blocked') {
                search_string += '&Blocked=' + true;
            }
            if (filters.status == 'unblocked') {
                search_string += '&Unblocked=' + true;
            }
        }
        if(this.props.current_user.role=="Admin"){
            this.props.AdminSearch(search_string); 
        }
        else{
            this.props.search(search_string); 
        }
        history.push(window.location.pathname + search_string);
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
    form_values: getFormValues('event-filter-form')(state),
    current_user: state.user
});

const mapDispatchToProps = (dispatch) => {
    return {
        search: (values) => dispatch(get_events(values)),
        get_categories: () => dispatch(get_categories()),
        AdminSearch: (values) => dispatch(get_eventsForAdmin(values)),
        reset_filters: () => dispatch(reset('event-filter-form'))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(EventFilterWrapper);