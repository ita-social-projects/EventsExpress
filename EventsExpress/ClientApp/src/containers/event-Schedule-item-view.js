import React, { Component } from 'react';
import { connect } from 'react-redux';
import EventScheduleItemView from '../components/eventSchedule/eventSchedule-item-view';
import Spinner from '../components/spinner';
import getEventSchedule, {
    resetEventSchedule
} from '../actions/eventSchedule/eventSchedule-item-view-action';

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
                current_user={this.props.current_user}
            />
    }
}

const mapStateToProps = (state) => ({
    eventSchedule: state.eventSchedule,
    current_user: state.user
});

const mapDispatchToProps = (dispatch) => ({
    getEventSchedule: (id) => dispatch(getEventSchedule(id)),
    reset: () => dispatch(resetEventSchedule())
})

export default connect(mapStateToProps, mapDispatchToProps)(EventScheduleItemViewWrapper);
