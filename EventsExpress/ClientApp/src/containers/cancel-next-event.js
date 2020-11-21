import React, { Component } from 'react';
import cancel_next_eventSchedule from '../actions/cancel-next-eventSchedule';
import { connect } from 'react-redux';
import Dropdown from 'react-bootstrap/Dropdown'
import { reset } from 'redux-form';
import EventScheduleModal from '../components/eventSchedule/eventSchedule-modal'
import {
    setCancelNextEventSchedulePending,
    setCancelNextEventScheduleError,
    setCancelNextEventScheduleSuccess
}
    from '../actions/cancel-next-eventSchedule';

class CancelNextEventWrapper extends Component {
    constructor() {
        super()
        this.state = {
            show: false,
        };
        this.cancelHandler = this.cancelHandler.bind(this);
        this.submitHandler = this.submitHandler.bind(this);
    }

    componentDidUpdate = () => {
        if (!this.props.cancel_next_eventSchedule_status.cancelNextEventScheduleError &&
            this.props.cancel_next_eventSchedule_status.isCancelNextEventScheduleSuccess) {
            this.props.resetEvent();
            this.props.reset();
        }
    }

    cancelHandler = () => {
        this.setState({
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
        this.props.cancel_next_eventSchedule(this.props.initialValues.id);
    }

    render() {

        return <>
            <Dropdown.Item onClick={this.handleClick}>Cancel once</Dropdown.Item>
            <EventScheduleModal
                cancelHandler={this.cancelHandler}
                message="Are you sure to cancel the next event?"
                show={this.state.show}
                submitHandler={this.submitHandler} />
        </>
    }
}

const mapStateToProps = (state) => ({
    user_id: state.user.id,
    cancel_next_eventSchedule_status: state.cancel_next_eventSchedule,
    initialValues: state.event.data,
});

const mapDispatchToProps = (dispatch) => {
    return {
        cancel_next_eventSchedule: (data) => dispatch(cancel_next_eventSchedule(data)),
        resetEvent: () => dispatch(reset('event-form')),
        reset: () => {
            dispatch(setCancelNextEventSchedulePending(true));
            dispatch(setCancelNextEventScheduleSuccess(false));
            dispatch(setCancelNextEventScheduleError(null));
        }
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(CancelNextEventWrapper);