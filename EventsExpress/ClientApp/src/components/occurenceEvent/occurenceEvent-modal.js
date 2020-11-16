import React, { Component } from 'react';
import Modal from 'react-bootstrap/Modal'
import Button from "@material-ui/core/Button";
import '../occurenceEvent/occurenceEvent.css'


export class OccurenceEventModal extends Component {
    constructor() {
        super()
    }

    render() {

        return (
            <>
                <Modal
                    className="custom-center"
                    show={this.props.show}
                    onHide={() => this.props.cancelHandler()}
                    backdrop="static"
                    keyboard={false}
                >
                    <Modal.Header closeButton>
                        <Modal.Title>Comfirmation</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        {this.props.message}
                    </Modal.Body>
                    <Modal.Footer>
                        <Button color="primary" variant="secondary" onClick={() => this.props.cancelHandler()}>
                            Cancel
                        </Button>
                        <Button color="primary" variant="primary" onClick={() => this.props.submitHandler()}>Submit</Button>
                    </Modal.Footer>
                </Modal>
            </>
        );
    }
}

export default OccurenceEventModal