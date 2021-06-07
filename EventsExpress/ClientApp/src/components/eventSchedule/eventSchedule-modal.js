import React, { Component } from 'react';
import Modal from 'react-bootstrap/Modal'
import Button from "@material-ui/core/Button";
import './eventSchedule.css'

export default class EventScheduleModal extends Component {
    
    render() {
        return (
            <>
                <Modal
                    className="custom-center"
                    show={this.props.show}
                    onHide={this.props.cancelHandler}
                    keyboard={false}
                >
                    <Modal.Header closeButton>
                        <Modal.Title>Confirmation</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        {this.props.message}
                    </Modal.Body>
                    <Modal.Footer>
                        <Button color="primary" variant="outlined" onClick={this.props.cancelHandler}>
                            Cancel
                        </Button>
                        <Button color="primary" variant="contained" onClick={this.props.submitHandler}>
                            Submit
                        </Button>
                    </Modal.Footer>
                </Modal>
            </>
        );
    }
}