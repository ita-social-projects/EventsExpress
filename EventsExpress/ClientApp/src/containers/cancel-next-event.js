import React, { Component } from 'react';
import { connect } from 'react-redux';
import Dropdown from 'react-bootstrap/Dropdown';
import EventScheduleModal from '../components/eventSchedule/eventSchedule-modal';
import cancel_next_eventSchedule from '../actions/eventSchedule-cancel-next-action';

class CancelNextEventWrapper extends Component {
    constructor() {
        super()
        this.state = {
            show: false,
        };
        this.cancelHandler = this.cancelHandler.bind(this);
        this.submitHandler = this.submitHandler.bind(this);
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
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(CancelNextEventWrapper);