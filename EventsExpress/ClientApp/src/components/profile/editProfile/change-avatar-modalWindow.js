import React from 'react';
import Modal from 'react-bootstrap/Modal'
import ChangeAvatarWrapper from '../../../containers/editProfileContainers/change-avatar';

export default function ChangeAvatarModal(props) {
    return (
      <Modal
        {...props}
        size="lg"
        aria-labelledby="contained-modal-title-vcenter"
        centered
      >
        <Modal.Header closeButton> 
          <Modal.Title id="contained-modal-title-vcenter">
            Update Avatar
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <ChangeAvatarWrapper></ChangeAvatarWrapper>
        </Modal.Body>
      </Modal>
    );
  }
  
