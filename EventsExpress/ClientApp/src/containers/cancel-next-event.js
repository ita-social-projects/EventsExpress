import React, { Component } from 'react';
import cancel_next_occurenceEvent from '../actions/cancel-next-occurenceEvent';
import { connect } from 'react-redux';
import Dropdown from 'react-bootstrap/Dropdown'
import { reset } from 'redux-form';
import OccurenceEventModal from '../components/occurenceEvent/occurenceEvent-modal'
import {
    setCancelNextOccurenceEventPending,
    setCancelNextOccurenceEventError,
    setCancelNextOccurenceEventSuccess
}
    from '../actions/cancel-next-occurenceEvent';
import { Redirect } from 'react-router-dom'

class CancelNextEventWrapper extends Component {
    constructor() {
        super()
        this.state = {
            redirect: false,
            show: false,
            submit: false
        };
        this.cancelHandler = this.cancelHandler.bind(this);
        this.submitHandler = this.submitHandler.bind(this);
    }

    componentDidUpdate = () => {
        if (!this.props.cancel_next_occurenceEvent_status.cancelNextOccurenceEventError &&
            this.props.cancel_next_occurenceEvent_status.isCancelNextOccurenceEventSuccess) {
            this.props.resetEvent();
            this.props.reset();
            this.setState({
                redirect: true
            })
        }
    }

    cancelHandler = () => {
        this.setState({
            redirect: false,
            show: false,
            submit: false
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
            submit: true
        });
        this.props.cancel_next_occurenceEvent(this.props.initialValues.id);
    }

    render() {

        return <>
            <Dropdown.Item onClick={this.handleClick}>Cancel once</Dropdown.Item>
            <OccurenceEventModal
                cancelHandler={this.cancelHandler}
                message="Are you sure to cancel the next event?"
                show={this.state.show}
                submitHandler={this.submitHandler} />
            {this.state.redirect &&
                <Redirect to='/occurenceEvents' />}
        </>
    }
}

const mapStateToProps = (state) => ({
    user_id: state.user.id,
    cancel_next_occurenceEvent_status: state.cancel_next_occurenceEvent,
    initialValues: state.event.data,
});

const mapDispatchToProps = (dispatch) => {
    return {
        cancel_next_occurenceEvent: (data) => dispatch(cancel_next_occurenceEvent(data)),
        resetEvent: () => dispatch(reset('event-form')),
        reset: () => {
            dispatch(setCancelNextOccurenceEventPending(true));
            dispatch(setCancelNextOccurenceEventSuccess(false));
            dispatch(setCancelNextOccurenceEventError(null));
        }
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(CancelNextEventWrapper);