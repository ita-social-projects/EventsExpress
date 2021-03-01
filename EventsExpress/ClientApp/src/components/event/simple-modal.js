import React, { Component } from "react";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import { DialogContent } from '@material-ui/core';

export default class SimpleModal extends Component {
    constructor(props) {
        super(props)

        this.state = {
            isOpen: false,
            id: null,
        }
    }

    onclick = () => {
        this.setState({isOpen: true});
    }

    onClose = () => {
        this.setState({isOpen: false, id: null});
    }

    onConfirm = () => {
        this.props.action()
        this.setState({ isOpen: false });
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
                                onClick={this.onConfirm}
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
