import React from "react";
import Modal from "react-bootstrap/Modal";
import CustomAvatar from "../../../avatar/custom-avatar";
import Button from "@material-ui/core/Button";
import { makeStyles } from "@material-ui/core/styles";
import ChangeAvatarModal from "./change-avatar-modal";

const useStyles = makeStyles({
  button: {
    display: "inline-block",
    margin: "20px",
  },
  startWindow: {
    display: "grid",
    justifyItems: "center",
  },
});

export default function EditAvatarModal(props) {
  const classes = useStyles();
  const [showChangeAvatarModal, setShowChangeAvatarModal] = React.useState(false);

  const handleChangeButtonClick = () =>{
    props.onHide();
    setShowChangeAvatarModal(true);
  }

  const handleReturnButtonClick= () =>{
    setShowChangeAvatarModal(false)
    props.Open()
  }
  const handleChangeAvatarModalClose = () => {
    setShowChangeAvatarModal(false);
  }
  return (
    <div>
      <Modal
        show = {props.show}
        size="sm"
        aria-labelledby="contained-modal-title-vcenter"
        centered 
        onHide = {props.onHide}
      >
        <Modal.Header closeButton>
          <Modal.Title id="contained-modal-title-vcenter">
            Edit Avatar
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className={classes.startWindow}>
            <CustomAvatar width="150px" height="150px" name = {props.name} userId={props.id} />
            <div>
              <Button className={classes.button} 
              onClick={handleChangeButtonClick}
              >
                Change
              </Button>
              <Button className={classes.button}>Delete</Button>
            </div>
          </div>
        </Modal.Body>
      </Modal>

      <ChangeAvatarModal 
      show={showChangeAvatarModal}
      onHide={handleChangeAvatarModalClose}
      onReturn={handleReturnButtonClick}
       />
    </div>
  )
}
