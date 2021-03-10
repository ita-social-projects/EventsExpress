import React, { Component } from 'react';
import { connect } from 'react-redux';
import { block_event, unblock_event, change_event_status } from '../actions/event-item-view';
import EventBlock from '../components/event/event-block';
import EventBlockUnblockModal from '../components/event/event-block-unblock-modal';
import StatusHistory from '../components/helpers/EventStatusEnum';

class EventManagmentWrapper extends Component {

    block = () => {
        this.props.block()
    }

    unblock = () => {
        this.props.unblock()
    }

    render() {
        const { eventStatus } = this.props.eventItem;
        console.log(this.props.eventItem);
        let canBlocked = eventStatus !== StatusHistory.Blocked;
        let canUnblocked = eventStatus === StatusHistory.Blocked;
        return (
            <div className="container-fluid mt-1">
                <div className="button-block">
                    {canBlocked && <EventBlockUnblockModal
                        submitCallback={this.props.onCancel}
                         cancelationStatus={this.props.event.cancelation}
                        eventStatus={StatusHistory.Blocked}
                    />}
                    {(canUnblocked) && <EventBlockUnblockModal
                        submitCallback={this.props.onCancel}
                         cancelationStatus={this.props.event.cancelation}
                        eventStatus={!StatusHistory.Blocked}
                    />}
                </div>

                <div className={(this.props.eventItem.eventStatus.Blocked == true) ? "bg-warning" : ""}>
                    <EventBlock
                        key={this.props.eventItem.eventStatus.Blocked}
                        eventItem={this.props.eventItem}
                        block={this.block}
                        unblock={this.unblock}
                    />
                </div>
            </div>
        )
    }
}

const mapStateToProps = (state) => ({
});

const mapDispatchToProps = (dispatch, props) => {
    return {
        block: () => dispatch(change_event_status(props.eventItem.eventId, props.eventItem.reason, props.eventItem.eventStatus)),
        unblock: () => dispatch(change_event_status(props.eventItem.eventId, props.eventItem.reason, props.eventItem.eventStatus)),
        // block: () => dispatch(block_event(props.eventItem.id)),
        // unblock: () => dispatch(unblock_event(props.eventItem.id))
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(EventManagmentWrapper)