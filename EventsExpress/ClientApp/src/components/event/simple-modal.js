import React, { Component } from "react";
import { connect } from 'react-redux';
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import { DialogContent } from '@material-ui/core';
import { setEventCanelationModalStatus } from '../../actions/event-item-view';

export default class SimpleModal extends Component {
    constructor(props) {
        super(props)

        this.state = {
            isOpen: false,
            Id: null,
        }
    }

    onclick = () => {
        this.setState({isOpen: true});
    }

    onClose = () => {
        this.setState({isOpen: false, Id: null});
    }

    render() {
        return (
            <>
                <div onClick={this.onclick}>{this.props.button}</div>
                <Dialog
                    open={this.state.isOpen}
                    onClose={this.onClose}
                >
                    <div className="eventCancel">
                        <DialogContent>
                            <div>
                                {this.props.data}
                            </div>
                        </DialogContent>
                        <DialogActions>
                            <Button
                                fullWidth={true}
                                type="button"
                                color="primary"
                                onClick={this.onClose}
                            >
                                discard
                            </Button>
                            <Button
                                fullWidth={true}
                                type="button"
                                value="Login"
                                color="primary"
                                onClick={() => this.props.action(this.props.Id)}
                            >
                                confirm
                            </Button>
                        </DialogActions>
                    </div>
                </Dialog>
            </>
        );
    }
}
