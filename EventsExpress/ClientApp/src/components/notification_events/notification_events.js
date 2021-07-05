import React, { Component } from 'react';
import { connect } from 'react-redux';
import { eventsForNotification } from '../../actions/events/events-for-notification-action';
import EventList from '../event/events-for-profile';
import Spinner from '../spinner';

class NotificationEvents extends Component {
    componentWillMount = () => {
        this.props.get_events(this.props.notification.events);
    }

    render() {
        const { current_user } = this.props;
        const { data } = this.props.events;
        const { items } = this.props.events.data;

        return <Spinner showContent={data != undefined}>
            {items.length == 0 &&
                <p className="text-center h3">You don't have notifications</p>
            }
            <EventList
                current_user={current_user}
                notification_events={this.props.notification.events}
                data_list={items} page={data.pageViewModel.pageNumber}
                totalPages={data.pageViewModel.totalPages}
                callback={this.props.get_events}
            />
        </Spinner>
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
        get_events: (eventIds, page) => dispatch(eventsForNotification(eventIds, page))
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(NotificationEvents);
