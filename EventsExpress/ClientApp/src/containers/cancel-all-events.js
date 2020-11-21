import React, { Component } from 'react';
import cancel_all_eventSchedules from '../actions/cancel-all-eventSchedules';
import { connect } from 'react-redux';
import Dropdown from 'react-bootstrap/Dropdown'
import { reset } from 'redux-form';
import EventScheduleModal from '../components/eventSchedule/eventSchedule-modal'
import {
    setCancelAllEventSchedulesPending,
    setCancelAllEventSchedulesError,
    setCancelAllEventSchedulesSuccess
}
    from '../actions/cancel-all-eventSchedules';

class CancelAllEventsWrapper extends Component {
    constructor() {
        super()
        this.state = {
            show: false,
        };
        this.cancelHandler = this.cancelHandler.bind(this);
        this.submitHandler = this.submitHandler.bind(this);
    }

    componentDidUpdate = () => {
        console.log(this.props);
        if (!this.props.cancel_all_eventSchedule_status.cancelEventSchedulesError &&
            this.props.cancel_all_eventSchedule_status.isCancelEventSchedulesSuccess) {
            this.props.resetEvent();
            this.props.reset();
        }
    }

    cancelHandler = () => {
        this.setState({
            redirect: false,
            show: false,
        });
    }

    handleClick = () => {
        this.setState({
            show: true
        });
    }

    submitHandler = () => {
        this.setState({
            show: false,
        });
        this.props.cancel_all_eventSchedules(this.props.initialValues.id);
    }

    render() {

        return <>
            <Dropdown.Item onClick={this.handleClick}>Cancel</Dropdown.Item>
            <EventScheduleModal
                cancelHandler={this.cancelHandler}
                message="Are you sure to cancel all events?"
                show={this.state.show}
                submitHandler={this.submitHandler} />
        </>
    }
}

const mapStateToProps = (state) => ({
    user_id: state.user.id,
    cancel_all_eventSchedule_status: state.cancel_all_eventSchedules,
    initialValues: state.event.data,
});

const mapDispatchToProps = (dispatch) => {
    return {
        cancel_all_eventSchedules: (data) => dispatch(cancel_all_eventSchedules(data)),
        resetEvent: () => dispatch(reset('event-form')),
        reset: () => {
            dispatch(setCancelAllEventSchedulesPending(true));
            dispatch(setCancelAllEventSchedulesSuccess(false));
            dispatch(setCancelAllEventSchedulesError(null));
        }
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(CancelAllEventsWrapper);