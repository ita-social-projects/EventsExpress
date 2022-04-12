import React, { Component } from 'react';
import Dropdown from 'react-bootstrap/Dropdown'
import DropdownButton from 'react-bootstrap/DropdownButton'
import './eventSchedule.css'
import AddFromParentEventWrapper from '../../containers/add-event-from-parent'
import EditFromParentEventWrapper from '../../containers/edit-event-from-parent'
import CancelNextEventWrapper from '../../containers/cancel-next-event'
import CancelAllEventsWrapper from '../../containers/cancel-all-events'
import EventSchedulePopover from './eventSchedule-popover'
import EventScheduleModal from './eventSchedule-modal'

export default class SelectiveForm extends Component {
    constructor() {
        super()
        this.state = {
            show: false,
            submit: false
        };
    }

    cancelHandler = () => {
        this.setState({
            show: false,
            submit: false
        });
    }

    onEdit = () => {
        this.setState({
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
                    <div className="dropdown">
                    <div className="col-11">
                        <DropdownButton title="Select Option For Event">
                            <Dropdown.Item as={AddFromParentEventWrapper}></Dropdown.Item>
                            <Dropdown.Item onClick={this.onEdit}>Create with editing</Dropdown.Item>
                            <Dropdown.Item as={CancelNextEventWrapper}></Dropdown.Item>
                            <Dropdown.Item as={CancelAllEventsWrapper}></Dropdown.Item>
                        </DropdownButton>
                    </div>
                    </div>
                    <EventSchedulePopover />
                </div>
                <EventScheduleModal
                    cancelHandler={this.cancelHandler}
                    message="Are you sure you want to create the event with editing?"
                    show={this.state.show}
                    submitHandler={this.submitHandler} />
                {this.state.submit &&
                    <div className="mt-3">
                        <EditFromParentEventWrapper onCancelEditing={this.cancelHandler}/>
                    </div>
                }
            </div>
        </>
    }
}