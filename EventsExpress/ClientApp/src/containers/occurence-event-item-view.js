import React, { Component } from 'react';
import { connect } from 'react-redux';
import OccurenceEventItemView from '../components/occurenceEvent/occurenceEvent-item-view';
import Spinner from '../components/spinner';
import getOccurenceEvent from '../actions/occurenceEvent-item-view';
import get_event from '../actions/event-item-view';
import { resetOccurenceEvent } from '../actions/occurenceEvent-item-view';
import { resetEvent } from '../actions/event-item-view';

class OccurenceEventItemViewWrapper extends Component {
    componentWillMount() {
        const { id } = this.props.match.params;
        this.props.getOccurenceEvent(id);
    }

    componentWillUnmount() {
        this.props.reset();
    }

    render() {
        const { isPending } = this.props.occurenceEvent;
        return isPending
            ? <Spinner />
            : <OccurenceEventItemView
                occurenceEvent={this.props.occurenceEvent}
                match={this.props.match}
                event={this.props.event}
                current_user={this.props.current_user}
            />
    }
}

const mapStateToProps = (state) => ({
    event: state.event,
    occurenceEvent: state.occurenceEvent,
    current_user: state.user
});

const mapDispatchToProps = (dispatch) => ({
    get_event: (id) => dispatch(get_event(id)),
    getOccurenceEvent: (id) => dispatch(getOccurenceEvent(id)),
    reset: () => dispatch(resetOccurenceEvent())
})


export default connect(mapStateToProps, mapDispatchToProps)(OccurenceEventItemViewWrapper);