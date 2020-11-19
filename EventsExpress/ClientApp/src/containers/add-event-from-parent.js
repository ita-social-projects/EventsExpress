import React, { Component } from 'react';
import add_copy_event from '../actions/add-copy-event';
import { connect } from 'react-redux';
import Dropdown from 'react-bootstrap/Dropdown'
import { reset } from 'redux-form';
import OccurenceEventModal from '../components/occurenceEvent/occurenceEvent-modal'
import {
    setCopyEventError,
    setCopyEventPending,
    setCopyEventSuccess
}
    from '../actions/add-copy-event';

class AddFromParentEventWrapper extends Component {
    constructor() {
        super()
        this.state = {
            show: false,
            submit: false
        };
        this.cancelHandler = this.cancelHandler.bind(this);
        this.submitHandler = this.submitHandler.bind(this);
    }

    componentDidUpdate = () => {

        if (!this.props.add_copy_event_status.copyEventError &&
            this.props.add_copy_event_status.isCopyEventSuccess) {
            this.props.resetEvent();
            this.props.reset();
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
        this.props.add_copy_event(this.props.initialValues.id);
    }

    render() {

        return <>
            <Dropdown.Item onClick={this.handleClick}>Create without editing</Dropdown.Item>
            <OccurenceEventModal
                cancelHandler={this.cancelHandler}
                message="Are you sure to create the event without editing?"
                show={this.state.show}
                submitHandler={this.submitHandler} />
        </>
    }
}

const mapStateToProps = (state) => ({
    user_id: state.user.id,
    add_copy_event_status: state.add_copy_event,
    initialValues: state.event.data,
});

const mapDispatchToProps = (dispatch) => {
    return {
        add_copy_event: (data) => dispatch(add_copy_event(data)),
        resetEvent: () => dispatch(reset('event-form')),
        reset: () => {
            dispatch(setCopyEventPending(true));
            dispatch(setCopyEventSuccess(false));
            dispatch(setCopyEventError(null));
        }
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(AddFromParentEventWrapper);