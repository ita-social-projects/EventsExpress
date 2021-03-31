import React, { Component } from 'react';
import { connect } from 'react-redux';
import EventScheduleItemView from '../components/eventSchedule/eventSchedule-item-view';
import Spinner from '../components/spinner';
import getEventSchedule, {
    resetEventSchedule
} from '../actions/eventSchedule/eventSchedule-item-view-action';
import get_event from '../actions/event/event-item-view-action';

class EventScheduleItemViewWrapper extends Component {
    componentWillMount() {
        const { id } = this.props.match.params;
        this.props.getEventSchedule(id);
    }

    componentWillUnmount() {
        this.props.reset();
    }

    render() {
        const { isPending } = this.props.eventSchedule;
        return isPending
            ? <Spinner />
            : <EventScheduleItemView
                eventSchedule={this.props.eventSchedule}
                match={this.props.match}
                event={this.props.event}
                current_user={this.props.current_user}
            />
    }
}

const mapStateToProps = (state) => ({
    event: state.event,
    eventSchedule: state.eventSchedule,
    current_user: state.user
});

const mapDispatchToProps = (dispatch) => ({
    get_event: (id) => dispatch(get_event(id)),
    getEventSchedule: (id) => dispatch(getEventSchedule(id)),
    reset: () => dispatch(resetEventSchedule())
})

export default connect(mapStateToProps, mapDispatchToProps)(EventScheduleItemViewWrapper);
