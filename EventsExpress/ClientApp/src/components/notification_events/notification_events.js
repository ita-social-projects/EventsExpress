import React, { Component } from 'react';
import { Redirect } from 'react-router';
import { connect } from 'react-redux';
import get_events from '../../actions/events-for-notification-action';
import EventList from '../event/events-for-profile';
import Spinner from '../spinner';
import BadRequest from '../Route guard/400';
import Unauthorized from '../Route guard/401';
import Forbidden from '../Route guard/403';

class NotificationEvents extends Component {
    componentWillMount = () => {
        this.props.get_events(this.props.notification.events);
    }

    render() {
        const { current_user, events, notification } = this.props;
        const { data, isPending, isError } = this.props.events;
        const { items } = this.props.events.data;
        const errorMessage = isError.ErrorCode == '403'
            ? <Forbidden />
            : isError.ErrorCode == '500'
                ? <Redirect from="*" to="/home/events" />
                : isError.ErrorCode == '401'
                    ? <Unauthorized />
                    : isError.ErrorCode == '400'
                        ? <BadRequest />
                        : null;

        const spinner = isPending ? <Spinner /> : null;
        const content = !errorMessage
            ? <EventList
                current_user={current_user}
                notification_events={this.props.notification.events}
                data_list={items} page={data.pageViewModel.pageNumber}
                totalPages={data.pageViewModel.totalPages}
                callback={this.props.get_events}
            />
            : null;

        return <>
            {spinner || content}
            {!spinner && items.length == 0 &&
                <p className="text-center h3">You don't have notifications</p>
            }
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

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(NotificationEvents);
