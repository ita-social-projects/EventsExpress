import React, { Component } from 'react';
import get_events from '../../actions/notification_events';
import EventList from '../event/events-for-profile';
import Spinner from '../spinner';

import BadRequest from '../Route guard/400';
import Unauthorized from '../Route guard/401';
import Forbidden from '../Route guard/403';

import { Redirect } from 'react-router';

import { connect } from 'react-redux';

class NotificationEvents extends Component{

    componentWillMount = () => {
        this.props.get_events(this.props.notification.events);
    }

    componentWillUnmount = () => {

    }

    render(){
        const { current_user, events, notification} = this.props;
        const { data, isPending, isError } = this.props.events;
        const { items } = this.props.events.data;
        const errorMessage = isError.ErrorCode == '403' 
            ? <Forbidden /> 
            : isError.ErrorCode == '500' 
                ? <Redirect from="*" to="/home/events?page=1"/>
                : isError.ErrorCode == '401' 
                    ? <Unauthorized /> 
                    : isError.ErrorCode == '400' 
                        ? <BadRequest /> 
                        : null;

        const spinner = isPending ? <Spinner /> : null;
        const content = !errorMessage ? <EventList current_user={current_user} notification_events={this.props.notification.events} data_list={items} page={data.pageViewModel.pageNumber} totalPages={data.pageViewModel.totalPages} callback={this.props.get_events} /> : null;
       
        return <>        
            {spinner}
            {content}
        </>
    }
}

const mapStateToProps = (state) => {
    return {
        events: state.events,
        current_user: state.user,
        notification: state.notification
    }    
};

const mapDispatchToProps = (dispatch) => { 
    return {
        get_events: (eventIds, page) => dispatch(get_events(eventIds, page))
    } 
};

    export default connect(mapStateToProps, mapDispatchToProps)(NotificationEvents);