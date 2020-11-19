import React, { Component } from 'react';
import { connect } from 'react-redux';
import Dropdown from 'react-bootstrap/Dropdown'
import { reset } from 'redux-form';
import OccurenceEventModal from '../components/occurenceEvent/occurenceEvent-modal'
import { Redirect } from 'react-router-dom'
import EditFromParentEvent from './edit-event-from-parent'

class EditFromParentEventWrapper extends Component {

    render() {

        return <>
            <OccurenceEventModal
                cancelHandler={() => this.props.cancelHandler()}
                message="Are you sure to create the event with editing?"
                show={this.props.show}
                submitHandler={() => this.props.submitHandler()} />
            {this.props.submit &&
            <EditFromParentEvent/>}
        </>
    }
}

const mapStateToProps = (state) => ({
    user_id: state.user.id,
    initialValues: state.event.data,
});

export default connect(mapStateToProps)(EditFromParentEventWrapper);