import React, { Component } from "react";
import { connect } from 'react-redux';
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import { DialogContent } from '@material-ui/core';
import { renderErrorMessage } from '../helpers/helpers';
import { setEventCanelationModalStatus } from '../../actions/event-item-view';
import StatusHistory from '../helpers/EventStatusEnum';
import IconButton from "@material-ui/core/IconButton";
import Tooltip from '@material-ui/core/Tooltip';

class EventBlockUnblockModal extends Component {
    constructor(props) {
        super(props)

        this.state = {
            changeStatusReason: '',
        }
    }
    static getDerivedStateFromProps(props, state) {
        return { status: props.status };
    }

    handleChange = (event) => {
        this.setState({ changeStatusReason: event.target.value })
    }

    handleClickOpen = () => {
       // this.setState.setStatus(true);
        this.props.setStatus(true);
    }

    handleClose = () => {
        this.props.setStatus(false);
        this.setState({ changeStatusReason: '' })
    }

    submit = () => {
        this.props.submitCallback(this.state.changeStatusReason);
    }

   

    render() {
        return (
            <>
                {this.props.eventStatus.Blocked ?
                    <Tooltip title="Blocked event">
                        <IconButton className="text-success" size="middle" onClick={this.handleClickOpen}>
                            <i className="fas fa-lock"></i>
                        </IconButton>
                    </Tooltip>


                    : <Tooltip title="Unblocked event">
                        <IconButton className="text-danger" size="middle" onClick={this.handleClickOpen}>
                            <i className="fas fa-unlock"></i>
                        </IconButton>
                    </Tooltip>
                }
                
                <Dialog
                    open={this.state.status}
                    onClose={this.handleClose}
                >
                    <div className="eventCancel">
                        <DialogContent>
                                <div>
                                    <h4>Enter the reason</h4>
                                </div>
                            <div>
                                <input size="50" type='text' onChange={this.handleChange} />
                            </div>
                        </DialogContent>
                        <DialogActions>
                            <Button
                                fullWidth={true}
                                type="button"
                                color="primary"
                                onClick={this.handleClose}
                            >
                                Discard
                            </Button>
                            <Button
                                fullWidth={true}
                                type="button"
                                value="Login"
                                color="primary"
                                onClick={this.submit}
                            >
                                Confirm action
                            </Button>
                        </DialogActions>
                    </div>
                </Dialog>
            </>
        );
    }
}

const mapStateToProps = (state) => ({
    status: state.event.cancelationModalStatus
});

const mapDispatchToProps = (dispatch) => ({
    setStatus: (data) => dispatch(setEventCanelationModalStatus(data))
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EventBlockUnblockModal);
