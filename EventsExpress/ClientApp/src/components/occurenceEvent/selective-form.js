import React, { Component } from 'react';
import { connect } from 'react-redux';
import Dropdown from 'react-bootstrap/Dropdown'
import DropdownButton from 'react-bootstrap/DropdownButton'
import '../occurenceEvent/occurenceEvent.css'
import Typography from '@material-ui/core/Typography';
import Button from "@material-ui/core/Button";
import Popover from '@material-ui/core/Popover';
import AddFromParentEventWrapper from '../../containers/add-event-from-parent'
import EditFromParentEventWrapper from '../../containers/edit-from-parent-event'
import CancelNextEventWrapper from '../../containers/cancel-next-event'
import CancelAllEventsWrapper from '../../containers/cancel-all-events'
import '../occurenceEvent/occurenceEvent.css'


class SelectiveForm extends Component {
    constructor() {
        super()
        this.state = {
            edit: false,
            show: false,
            submit: false,
            anchorEl: false,
            isFocused: false,
        };
        this.cancelHandler = this.cancelHandler.bind(this);
        this.submitHandler = this.submitHandler.bind(this);
    }

    cancelHandler = () => {
        this.setState({
            show: false,
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

    onEdit = () => {
        this.setState({
            edit: true,
            show: true
        });
    }

    onFocusChange = () => {
        this.setState({ isFocused: true });
    }

    submitHandler = () => {
        this.setState({
            show: false,
            submit: true
        });
    }

    render() {

        console.log(this.state);
        return <>
            <div className="shadow-lg p-3 mb-5 bg-white rounded">
                <div className="row">
                    <div className="col-11 mb-3">
                        <DropdownButton title="Select Option For Event">
                            <Dropdown.Item  as={AddFromParentEventWrapper}></Dropdown.Item>
                            <Dropdown.Item  onClick={this.onEdit}>Create with editing</Dropdown.Item>
                            <Dropdown.Item  as={CancelNextEventWrapper}></Dropdown.Item>
                            <Dropdown.Item  as={CancelAllEventsWrapper}></Dropdown.Item>
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
                {this.state.edit &&
                <EditFromParentEventWrapper 
                show={this.state.show}
                submitHandler={this.submitHandler}
                cancelHandler={this.cancelHandler}
                submit={this.state.submit}/>}
            </div>
        </>
    }
}


const mapStateToProps = (state) => ({
    user_id: state.user.id,
    initialValues: state.event.data,
});

export default connect(mapStateToProps)(SelectiveForm);