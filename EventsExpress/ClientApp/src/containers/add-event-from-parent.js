import React, { Component } from 'react';
import { connect } from 'react-redux';
import Dropdown from 'react-bootstrap/Dropdown'
import EventScheduleModal from '../components/eventSchedule/eventSchedule-modal'
import add_copy_event from '../actions/event/event-copy-without-edit-action';

class AddFromParentEventWrapper extends Component {
    constructor() {
        super()
        this.state = {
            show: false
        };
    }

    cancelHandler = () => {
        this.setState({
            show: false
        });
    }

    handleClick = () => {
        this.setState({
            show: true
        });
    }

    submitHandler = () => {
        this.setState({
            show: false
        });
        this.props.add_copy_event(this.props.eventId);
    }

    render() {
        return <>
            <Dropdown.Item onClick={this.handleClick}>Create without editing</Dropdown.Item>
            <EventScheduleModal
                cancelHandler={this.cancelHandler}
                message="Are you sure you want to create the event without editing?"
                show={this.state.show}
                submitHandler={this.submitHandler} />
        </>
    }
}

const mapStateToProps = (state) => ({
    add_copy_event_status: state.add_copy_event,
    eventId: state.event.data.id
});

const mapDispatchToProps = (dispatch) => {
    return {
        add_copy_event: (data) => dispatch(add_copy_event(data))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(AddFromParentEventWrapper);