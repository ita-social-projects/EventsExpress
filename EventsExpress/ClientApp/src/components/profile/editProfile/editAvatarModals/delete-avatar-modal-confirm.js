import React from "react";
import Modal from "react-bootstrap/Modal";
import Button from "@material-ui/core/Button";
import KeyboardBackspaceIcon from '@material-ui/icons/KeyboardBackspace';
import IconButton from '@material-ui/core/IconButton';
import { connect } from 'react-redux';
import delete_avatar from "../../../../actions/redactProfile/avatar-delete-action";


// const photoService = new PhotoService();
// const history = createBrowserHistory({ forceRefresh: true });

 function DeleteAvatarModal(props) {
    const CancelButtonClick = () => {
        props.onHide();
    }
    const ConfirmButtonClick = () =>{
        props.delete_avatar(props.id);
        
    };

  return (
    <Modal
        show={props.show}
        onHide={props.onHide}
        centered
      >
        <Modal.Header closeButton>
          
          <Modal.Title id="contained-modal-title-vcenter">
          <IconButton 
           onClick={props.onReturn}
          >
            <KeyboardBackspaceIcon />
          </IconButton>
            Delete Avatar
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          Are you sure?
        </Modal.Body>
        <Modal.Footer>
          <Button variant="contained" color="secondary" onClick={CancelButtonClick}>Cancel</Button>
          <Button variant="contained" color="primary" onClick={ConfirmButtonClick}>Confirm</Button>
        </Modal.Footer>
      </Modal>
  )
}

const mapDispatchToProps = dispatch => {
  return {
      delete_avatar: (data) => dispatch(delete_avatar(data))
  };
}

export default connect(null, mapDispatchToProps)(DeleteAvatarModal);