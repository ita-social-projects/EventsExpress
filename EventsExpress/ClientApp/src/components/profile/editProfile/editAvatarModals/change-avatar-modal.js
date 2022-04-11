import React from "react";
import Modal from "react-bootstrap/Modal";
import ChangeAvatarWrapper from "../../../../containers/editProfileContainers/change-avatar";
import KeyboardBackspaceIcon from '@material-ui/icons/KeyboardBackspace';
import IconButton from '@material-ui/core/IconButton';



export default function ChangeAvatarModal(props) {
  return (
    <Modal
        show={props.show}
        onHide={props.onHide}
        size="lg"
        aria-labelledby="contained-modal-title-vcenter"
        centered
      >
        <Modal.Header closeButton>
          
          <Modal.Title id="contained-modal-title-vcenter">
          <IconButton 
           onClick={props.onReturn}
          >
            <KeyboardBackspaceIcon />
          </IconButton>
            Change Avatar
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <ChangeAvatarWrapper></ChangeAvatarWrapper>
        </Modal.Body>
      </Modal>
  )
}
