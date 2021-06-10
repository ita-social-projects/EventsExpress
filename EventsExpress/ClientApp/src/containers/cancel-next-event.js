import React, { Component } from 'react';
import { connect } from 'react-redux';
import Dropdown from 'react-bootstrap/Dropdown';
import EventScheduleModal from '../components/eventSchedule/eventSchedule-modal';
import cancel_next_eventSchedule from '../actions/eventSchedule/eventSchedule-cancel-next-action';

class CancelNextEventWrapper extends Component {
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
        this.props.cancel_next_eventSchedule(this.props.eventId);
    }

    render() {
        return <>
            <Dropdown.Item onClick={this.handleClick}>Cancel once</Dropdown.Item>
            <EventScheduleModal
                cancelHandler={this.cancelHandler}
                message="Are you sure you want to cancel the next event?"
                show={this.state.show}
                submitHandler={this.submitHandler} />
        </>
    }
}

const mapStateToProps = (state) => ({
    cancel_next_eventSchedule_status: state.cancel_next_eventSchedule,
    eventId: state.event.data.id
});

const mapDispatchToProps = (dispatch) => {
    return {
        cancel_next_eventSchedule: (data) => dispatch(cancel_next_eventSchedule(data))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(CancelNextEventWrapper);