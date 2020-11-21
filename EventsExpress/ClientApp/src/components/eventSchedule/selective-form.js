import React, { Component } from 'react';
import { connect } from 'react-redux';
import Dropdown from 'react-bootstrap/Dropdown'
import DropdownButton from 'react-bootstrap/DropdownButton'
import './eventSchedule.css'
import AddFromParentEventWrapper from '../../containers/add-event-from-parent'
import EditFromParentEventWrapper from '../../containers/edit-event-from-parent'
import CancelNextEventWrapper from '../../containers/cancel-next-event'
import CancelAllEventsWrapper from '../../containers/cancel-all-events'
import EventSchedulePopover from './eventSchedule-popover'
import EventScheduleModal from './eventSchedule-modal'
import './eventSchedule.css'


class SelectiveForm extends Component {
    constructor() {
        super()
        this.state = {
            edit: false,
            show: false,
            submit: false,
        };
        this.cancelHandler = this.cancelHandler.bind(this);
        this.submitHandler = this.submitHandler.bind(this);
    }

    cancelEdit = () => {
        this.setState({
            edit: false
        });
    }

    cancelHandler = () => {
        this.setState({
            show: false,
            submit: false
        });
    }

    onEdit = () => {
        this.setState({
            edit: true,
            show: true
        });
    }

    submitHandler = () => {
        this.setState({
            show: false,
            submit: true
        });
    }

    render() {

        return <>
            <div className="shadow-lg p-3 mb-5 bg-white rounded">
                <div className="row">
                    <div className="col-11 mb-3">
                        <DropdownButton title="Select Option For Event">
                            <Dropdown.Item as={AddFromParentEventWrapper}></Dropdown.Item>
                            <Dropdown.Item onClick={this.onEdit}>Create with editing</Dropdown.Item>
                            <Dropdown.Item as={CancelNextEventWrapper}></Dropdown.Item>
                            <Dropdown.Item as={CancelAllEventsWrapper}></Dropdown.Item>
                        </DropdownButton>
                    </div>
                    <EventSchedulePopover />
                </div>
                <EventScheduleModal
                    cancelHandler={() => this.cancelHandler()}
                    message="Are you sure to create the event with editing?"
                    show={this.state.show}
                    submitHandler={this.submitHandler} />
                {this.state.edit && this.state.submit &&
                    <EditFromParentEventWrapper />}
            </div>
        </>
    }
}


const mapStateToProps = (state) => ({
    user_id: state.user.id,
    initialValues: state.event.data,
});

export default connect(mapStateToProps)(SelectiveForm);