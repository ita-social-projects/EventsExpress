import React, { Component } from 'react';
import Modal from 'react-bootstrap/Modal'
import Button from "@material-ui/core/Button";
import '../occurenceEvent/occurenceEvent.css'


export class OccurenceEventModal extends Component {
    constructor() {
        super()
        this.state = { show: true, setShow: false };
        this.handleClose = this.handleClose.bind(this);
        this.handleShow = this.handleShow.bind(this);
    }

    handleClose = () => this.setState(state => ({ show: false }));
    handleShow = () => this.setState({ show: true });

    render() {

        return (
            <>
                <Modal
                    className="custom-center"
                    show={this.state.show}
                    onHide={this.handleClose}
                    backdrop="static"
                    keyboard={false}
                >
                    <Modal.Header closeButton>
                        <Modal.Title>Comfirmation</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        Are you shure?
                    </Modal.Body>
                    <Modal.Footer>
                        <Button color="primary" variant="secondary" onClick={() => this.props.showHandler()}>
                            Cancel
                        </Button>
                        <Button color="primary" variant="primary">Submit</Button>
                    </Modal.Footer>
                </Modal>
            </>
        );
    }
}

export default OccurenceEventModal