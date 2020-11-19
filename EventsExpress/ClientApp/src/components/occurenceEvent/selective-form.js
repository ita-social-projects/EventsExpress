import React, { Component } from 'react';
import { connect } from 'react-redux';
import OccurenceEventModal from '../occurenceEvent/occurenceEvent-modal'
import Dropdown from 'react-bootstrap/Dropdown'
import DropdownButton from 'react-bootstrap/DropdownButton'
import '../occurenceEvent/occurenceEvent.css'
import Typography from '@material-ui/core/Typography';
import Button from "@material-ui/core/Button";
import Popover from '@material-ui/core/Popover';
import add_copy_event from '../../actions/add-copy-event';
import EditFromParentEventWrapper from '../../containers/edit-event-from-parent'
import cancel_all_occurenceEvent from '../../actions/cancel-all-occurenceEvents';
import cancel_next_occurence_event from '../../actions/cancel-next-occurenceEvent';
import '../occurenceEvent/occurenceEvent.css'



class SelectiveForm extends Component {
    constructor() {
        super()
        this.state = {
            edit: false,
            anchorEl: false,
            show: false,
            submit: false,
            isFocused: false
        };
        this.cancelHandler = this.cancelHandler.bind(this);
        this.submitHandler = this.submitHandler.bind(this);
    }

    cancelHandler = () => {
        this.resetForm();
    }

    CancelOnceOption = () => {
        this.setState({
            submitHandler: this.cancelOnceSubmitHandler,
            message: "You you sure to cancel the next event?",
            show: true
        });
    }

    CancelOption = () => {
        this.setState({
            submitHandler: this.cancelSubmitHandler,
            message: "You you sure to cancel all events?",
            show: true
        });
    }

    cancelOnceSubmitHandler = () => {
        this.setState({
            show: false
        });
        this.props.cancel_next_occurence_event(this.props.initialValues.id);
    }

    cancelSubmitHandler = () => {
        this.setState({
            show: false
        });
        this.props.cancel_all_occurenceEvent(this.props.initialValues.id);
    }

    CreateOption = () => {
        this.setState({
            submitHandler: this.createSubmitHandler,
            message: "You you sure to create event without editing?",
            show: true
        });
    }

    createSubmitHandler = () => {
        this.setState({
            show: false
        });
        this.props.add_copy_event(this.props.initialValues.id);
    }

    EditOption = () => {
        this.setState({
            submitHandler: this.editSubmitHandler,
            message: "You you sure to create event with editing?",
            show: true
        });
    }

    editSubmitHandler = () => {
        this.setState({
            show: false,
            edit: true
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
            edit: false,
            show: false,
            submit: false
        });
    }

    submitHandler = () => {
        this.setState({
            submit: true,
            show: false
        });
    }

    render() {

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
                <OccurenceEventModal
                    cancelHandler={this.cancelHandler}
                    message={this.state.message} show={this.state.show}
                    submitHandler={this.state.submitHandler} />
                {this.state.edit && <EditFromParentEventWrapper />}
            </div>
        );
    }
}


const mapStateToProps = (state) => ({
    user_id: state.user.id,
    initialValues: state.event.data,
});

const mapDispatchToProps = (dispatch) => {
    return {
        add_copy_event: (eventId) => dispatch(add_copy_event(eventId)),
        cancel_all_occurenceEvent: (eventId) => dispatch(cancel_all_occurenceEvent(eventId)),
        cancel_next_occurence_event: (eventId) => dispatch(cancel_next_occurence_event(eventId))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(SelectiveForm);