import React, { Component } from 'react';
import { connect } from 'react-redux';
import Dropdown from 'react-bootstrap/Dropdown';
import EventScheduleModal from '../components/eventSchedule/eventSchedule-modal';
import cancel_all_eventSchedules from '../actions/eventSchedule/eventSchedule-cancel-all-action';

class CancelAllEventsWrapper extends Component {
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
        this.props.cancel_all_eventSchedules(this.props.eventId);
    }

    render() {
        return <>
            <Dropdown.Item onClick={this.handleClick}>Cancel</Dropdown.Item>
            <EventScheduleModal
                cancelHandler={this.cancelHandler}
                message="Are you sure you want to cancel all events?"
                show={this.state.show}
                submitHandler={this.submitHandler} />
        </>
    }
}

const mapStateToProps = (state) => ({
    cancel_all_eventSchedule_status: state.cancel_all_eventSchedules,
    eventId: state.event.data.id
});

const mapDispatchToProps = (dispatch) => {
    return {
        cancel_all_eventSchedules: (data) => dispatch(cancel_all_eventSchedules(data))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(CancelAllEventsWrapper);