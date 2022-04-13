import React from "react";
import Modal from "react-bootstrap/Modal";
import CustomAvatar from "../../../avatar/custom-avatar";
import Button from "@material-ui/core/Button";
import { makeStyles } from "@material-ui/core/styles";
import ChangeAvatarModal from "./change-avatar-modal";
import PhotoService from "../../../../services/PhotoService";
import { createBrowserHistory } from 'history';
import DeleteAvatarModal from "./delete-avatar-modal-confirm";

const useStyles = makeStyles({
  button: {
    display: "inline-block",
    margin: "20px",
  },
  startWindow: {
    display: "grid",
    justifyItems: "center",
  },
  deleteButton:{
    display:"none"
  }
});

const history = createBrowserHistory({ forceRefresh: true });

const photoService = new PhotoService();

export default function EditAvatarModal(props) {
  const classes = useStyles();
  const [showChangeAvatarModal, setShowChangeAvatarModal] = React.useState(false);
  const [showDeleteAvatarModal, setShowDeleteAvatarModal] = React.useState(false);
  const [displayDeleteButton, setDisplayDeleteButton] = React.useState(false);

  const handleChangeButtonClick = () =>{
    props.onHide();
    setShowChangeAvatarModal(true);
  }

  const handleChangeAvatarModalClose = () => {
    setShowChangeAvatarModal(false);
  }

  const handleReturnButtonInChangeModalClick= () =>{
    handleChangeAvatarModalClose();
    props.Open()
  }

  const handleDeleteButtonClick = () =>{
    props.onHide();
    setShowDeleteAvatarModal(true);
  }

  const handleDeleteAvatarModalClose = () => {
    setShowDeleteAvatarModal(false);
  }

  const handleReturnButtonInDeleteModalClick = () =>{
    handleDeleteAvatarModalClose();
    props.Open();
  }

  React.useEffect(() => {
     photoService.getUserPhoto(props.id).then((image) => {
      if (image) {
        setDisplayDeleteButton(false);
      }
      else{
        setDisplayDeleteButton(true);
      } 
    })
  });

 const header = (displayDeleteButton ? "Add" : "Edit") + " Avatar";

  return (
    <div>
      <Modal
        show = {props.show}
        size="lg"
        aria-labelledby="contained-modal-title-vcenter"
        centered 
        onHide = {props.onHide}
      >
        <Modal.Header closeButton>
          <Modal.Title id="contained-modal-title-vcenter">
            {header}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className={classes.startWindow}>
            <CustomAvatar width="200px" height="200px" name = {props.name} userId={props.id} />
            <div>
              <Button className={classes.button} 
              onClick={handleChangeButtonClick}
              variant="contained" color="primary">
                {displayDeleteButton ? "Add" : "Change"}
              </Button>
              <Button 
              className= {displayDeleteButton ? classes.deleteButton : classes.button}
              onClick={handleDeleteButtonClick}
              variant="contained" color="secondary"
              >
              Delete</Button>
            </div>
          </div>
        </Modal.Body>
      </Modal>

      <ChangeAvatarModal 
      show={showChangeAvatarModal}
      onHide={handleChangeAvatarModalClose}
      onReturn={handleReturnButtonInChangeModalClick}
      header = {header}
       />

      <DeleteAvatarModal
      show={showDeleteAvatarModal}
      onHide = {handleDeleteAvatarModalClose}
      onReturn = {handleReturnButtonInDeleteModalClick}
      id ={props.id}

      />
    </div>
  )
}
