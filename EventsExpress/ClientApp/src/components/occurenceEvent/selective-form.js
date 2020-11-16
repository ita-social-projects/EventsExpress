import React, { Component } from 'react';
import { connect } from 'react-redux';
import OccurenceEventModal from '../occurenceEvent/occurenceEvent-modal'
import Dropdown from 'react-bootstrap/Dropdown'
import DropdownButton from 'react-bootstrap/DropdownButton'
import '../occurenceEvent/occurenceEvent.css'
import Typography from '@material-ui/core/Typography';
import Button from "@material-ui/core/Button";
import Popover from '@material-ui/core/Popover';
import EditFromParentEventWrapper from '../../containers/edit-event-from-parent'
import '../occurenceEvent/occurenceEvent.css'


export class SelectiveForm extends Component {
    constructor() {
        super()
        this.state = {
            isCreateOption: false,
            isEditOption: false,
            isCancelOnceOption: false,
            isCancelOption: false,
            anchorEl: false,
            show: false,
            submit: false,
            isFocused: false
        };
        this.cancelHandler = this.cancelHandler.bind(this);
        this.submitHandler = this.submitHandler.bind(this);
    }


    onClickConfirm = (e) => {
        this.CreateOption() ||
            this.EditOption() ||
            this.CancelOnceOption() ||
            this.CancelOption()
    }

    CreateOption = () => {
        this.setState({
            isCreateOption: true,
            isEditOption: false,
            isCancelOption: false,
            isCancelOnceOption: false,
            show: true,
            submit: false
        });
    }

    cancelHandler = () => {
        this.resetForm();
    }

    submitHandler = () => {
        this.setState({
            submit: true,
            show: false
        });
    }

    EditOption = () => {
        this.setState(state => ({
            isEditOption: true,
            isCreateOption: false,
            isCancelOption: false,
            isCancelOnceOption: false,
            show: true,
            submit: false
        }));
    }

    CancelOnceOption = () => {
        this.setState({
            isEditOption: false,
            isCreateOption: false,
            isCancelOnceOption: true,
            isCancelOption: false,
            show: true,
            submit: false
        });
    }

    CancelOption = () => {
        this.setState({
            isEditOption: false,
            isCreateOption: false,
            isCancelOnceOption: false,
            isCancelOption: true,
            show: true,
            submit: false
        });
    }

    handlePopover = (event) => {
        this.setState({
            anchorEl: true
        });
    }

    handlePopoverClose = () => {
        this.setState({
            anchorEl: false
        });
    }

    onFocusChange = () => {
        this.setState({ isFocused: true });
    }

    resetForm = () => {
        this.setState({
            imagefile: [],
            isCreateOption: false,
            isEditOption: false,
            isCancelOnceOption: false,
            isCancelOption: false,
            submit: false
        });
    }

    render() {
        console.log("render", this.props);
        return (
            <div className="shadow-lg p-3 mb-5 bg-white rounded">
                <div className="row">
                    <div className="col-11 mb-3">
                        <DropdownButton title="Select Option For Event">
                            <Dropdown.Item eventKey="0" onClick={this.CreateOption}>Create without editing</Dropdown.Item>
                            <Dropdown.Item eventKey="1" onClick={this.EditOption}>Create with editing</Dropdown.Item>
                            <Dropdown.Item eventKey="2" onClick={this.CancelOnceOption}>Cancel once</Dropdown.Item>
                            <Dropdown.Item eventKey="3" onClick={this.CancelOption}>Cancel</Dropdown.Item>
                        </DropdownButton>
                    </div>
                    <Button
                        onFocus={this.onFocusChange}
                        style={(this.state.isFocused) ?
                            { minWidth: "2px", outlineStyle: "none" } :
                            { minWidth: "2px" }} onClick={this.handlePopover}>
                        <i class="fas fa-info-circle"></i>
                    </Button>
                    <Popover
                        open={this.state.anchorEl}
                        anchorEl={this.state.anchorEl}
                        onClose={this.handlePopoverClose}
                        anchorOrigin={{
                            vertical: 'bottom',
                            horizontal: 'center',
                        }}
                        transformOrigin={{
                            vertical: 'top',
                            horizontal: 'center',
                        }}
                    >
                        <Typography style={{ maxWidth: "350px", padding: "15px" }}>Click Create Without Editing to create the event without editing.
                            To create the event with editing you can choose second option. Click Cancel Once to cancel the next event, to cancel all events click Cancel.</Typography>
                    </Popover>
                </div>
                {this.state.isCreateOption &&
                    <OccurenceEventModal
                        cancelHandler={this.cancelHandler}
                        message={"Create the event without editing?"} show={this.state.show}
                        submitHandler={this.submitHandler} />}
                {this.state.isEditOption &&
                    <OccurenceEventModal
                        cancelHandler={this.cancelHandler}
                        message={"Create the event with editing?"} show={this.state.show}
                        submitHandler={this.submitHandler} />}
                {this.state.isEditOption && this.state.submit &&
                    <EditFromParentEventWrapper />}
                {this.state.isCancelOnceOption &&
                    <OccurenceEventModal
                        cancelHandler={this.cancelHandler}
                        message={"Cancel the next event?"} show={this.state.show}
                        submitHandler={this.submitHandler} />}
                {this.state.isCancelOption &&
                    <OccurenceEventModal
                        cancelHandler={this.cancelHandler}
                        message={"Cancel all events?"} show={this.state.show}
                        submitHandler={this.submitHandler} />}
            </div>
        );
    }
}


export default SelectiveForm;