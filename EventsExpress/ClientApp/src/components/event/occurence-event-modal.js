import React from 'react';
import Button from "@material-ui/core/Button";
import {
    Alert,
    Modal,
    StyleSheet,
    Text,
    TouchableHighlight,
    View
} from "react-native";

export default function OccurenceEventModal(props) {
    return (
        <Modal
            show={show}
            onHide={handleClose}
            backdrop="static"
            keyboard={false}
            {...props}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Create Reccurent Event
            </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <p>
                    Click Submit to create the reccurent event. To edit the event, click Edit. 
                    Click Cancel Once to cancel the reccurent event. To cancel all reccurent events, click Cancel.
                </p>
            </Modal.Body>
            <Modal.Footer>
                <Button onClick={}>Confirm</Button>
                <Button onClick={}>Edit</Button>
                <Button onClick={}>Cancel Once</Button>
                <Button onClick={}>Cancel</Button>
            </Modal.Footer>
        </Modal>
    );
}

