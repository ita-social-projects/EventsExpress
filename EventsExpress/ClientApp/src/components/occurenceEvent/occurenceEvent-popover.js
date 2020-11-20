import React, { Component } from 'react';
import Button from "@material-ui/core/Button";
import Typography from '@material-ui/core/Typography';
import Popover from '@material-ui/core/Popover';
import '../occurenceEvent/occurenceEvent.css'


export class OccurenceEventPopover extends Component {

    state = {
        anchorEl: false,
        isFocused: false,
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

    render() {

        return (
            <>
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
                >
                    <Typography style={{ maxWidth: "350px", padding: "15px" }}>
                        Click Create Without Editing to create the event without editing.
                        To create the event with editing you can choose second option.
                        Click Cancel Once to cancel the next event, to cancel all events click Cancel.</Typography>
                </Popover>
            </>
        );
    }
}

export default OccurenceEventPopover