import React, { Component } from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Slide from '@material-ui/core/Slide';

const Transition = React.forwardRef(function Transition(props, ref) {
  return <Slide direction="up" ref={ref} {...props} />;
});



export default class EventLeaveModal extends Component {
    state = { open: false };

    handleClickOpen = () => {
        this.setState({ open: true })
    };

    handleClose = () => {
        this.setState({ open: false })
    };
    
    render() {

        return (
            <div>
                <button onClick={this.handleClickOpen}
                    type="button"
                    className="btn btn-edit"
                    variant="contained"
                >
                    Leave
                </button>
                <Dialog
                    open={this.state.open}
                    TransitionComponent={Transition}
                    keepMounted
                    onClose={this.handleClose}
                    aria-labelledby="alert-dialog-slide-title"
                    aria-describedby="alert-dialog-slide-description"
                >
                    <DialogTitle id="alert-dialog-slide-title">{"Exiting from event"}</DialogTitle>
                    <DialogContent>
                        <DialogContentText id="alert-dialog-slide-description">
                        Are you sure that you want to leave this event. If you leave, your statement 
                        will be deleted. Press "Agree" if you want to leave event and "Disagree" if not.
                        </DialogContentText>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={this.handleClose} color="primary">
                        Disagree
                        </Button>
                        <Button onClick={this.props.submitLeave} color="secondary">
                        Agree
                        </Button>
                    </DialogActions>
                </Dialog>
            </div>
            );
    }
}