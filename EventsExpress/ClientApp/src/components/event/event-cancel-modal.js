import React, { Component } from "react";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import { DialogContent } from '@material-ui/core';
import { renderErrorMessage } from '../helpers/helpers';
import { connect } from 'react-redux';
import { setEventCanelationModalStatus } from '../../actions/event-item-view';

class EventCancelModal extends Component {
    constructor(props) {
        super(props)

        this.state = {
            cancelationReason: '',
        }
    }

    handleChange = (event) => {
        this.setState({ cancelationReason: event.target.value })
    }

    handleClickOpen = () => {
        this.props.setStatus(true);
    }

    handleClose = () => {
        this.props.setStatus(false);
        this.setState({ cancelationReason: '' })
    }

    submit = () => {
        this.props.submitCallback(this.state.cancelationReason);
    }

    render() {
        return (
            <>
                <button onClick={this.handleClickOpen} className="btn btn-join">Cancel</button>
                <Dialog
                    open={this.props.status}
                    onClose={this.handleClose}
                >
                    <div className="eventCancel">
                        <DialogContent>
                            <div>
                                <h4>Enter the reason of cancelation</h4>
                            </div>
                            <div>
                                <input size="50" type='text' onChange={this.handleChange} />
                            </div>
                            {this.props.cancelationStatus.errorMessage &&
                                renderErrorMessage(this.props.cancelationStatus.errorMessage, 'Reason')
                                // <div style={{ color: 'red' }}>
                                //     {JSON.parse(this.props.cancelationStatus.errorMessage)["errors"]["Reason"].map(() =>
                                //         <div>
                                //             {item}
                                //         </div>
                                //     )}
                                // </div>
                            }
                        </DialogContent>
                        <DialogActions>
                            <Button fullWidth={true} type="button" color="primary" onClick={this.handleClose}>
                                discard
                            </Button>
                            <Button fullWidth={true} type="button" value="Login" color="primary" onClick={this.submit}>
                                confirm cancelation
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

export default connect(mapStateToProps, mapDispatchToProps)(EventCancelModal)