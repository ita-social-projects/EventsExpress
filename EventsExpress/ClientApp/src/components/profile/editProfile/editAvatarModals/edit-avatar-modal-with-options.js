import React from "react";
import CustomAvatar from "../../../avatar/custom-avatar";
import Button from "@material-ui/core/Button";
import { makeStyles } from "@material-ui/core/styles";
import ChangeAvatarModal from "./change-avatar-modal";
import PhotoService from "../../../../services/PhotoService";
import DeleteAvatarModal from "./delete-avatar-modal-confirm";
import Dialog from "@material-ui/core/Dialog";
import DialogContent from "@material-ui/core/DialogContent";
import DialogTitle from "@material-ui/core/DialogTitle";
import IconButton from "@material-ui/core/IconButton";
import CloseIcon from "@material-ui/icons/Close";

const useStyles = makeStyles({
  button: {
    display: "inline-block",
    margin: "20px",
  },
  startWindow: {
    display: "grid",
    justifyItems: "center",
  },
  deleteButton: {
    display: "none",
  },
  closeButton: {
    position: "absolute",
    right: "8px",
    top: "8px",
  },
  title: {
    margin: 0,
    padding: "16px",
  },
});

const photoService = new PhotoService();

export default function EditAvatarModal(props) {
  const classes = useStyles();
  const [showChangeAvatarModal, setShowChangeAvatarModal] =
    React.useState(false);
  const [showDeleteAvatarModal, setShowDeleteAvatarModal] =
    React.useState(false);
  const [displayDeleteButton, setDisplayDeleteButton] = React.useState(false);

  const handleChangeButtonClick = () => {
    props.onHide();
    setShowChangeAvatarModal(true);
  };

  const handleChangeAvatarModalClose = () => {
    setShowChangeAvatarModal(false);
  };

  const handleReturnButtonInChangeModalClick = () => {
    handleChangeAvatarModalClose();
    props.Open();
  };

  const handleDeleteButtonClick = () => {
    props.onHide();
    setShowDeleteAvatarModal(true);
  };

  const handleDeleteAvatarModalClose = () => {
    setShowDeleteAvatarModal(false);
  };

  const handleReturnButtonInDeleteModalClick = () => {
    handleDeleteAvatarModalClose();
    props.Open();
  };

  React.useEffect(() => {
    photoService.getUserPhoto(props.id).then((image) => {
      if (image) {
        setDisplayDeleteButton(false);
      } else {
        setDisplayDeleteButton(true);
      }
    });
  }, []);

  const header = (displayDeleteButton ? "Add" : "Edit") + " Avatar";
  return (
    <React.Fragment>
      <Dialog
        fullWidth
        maxWidth="xs"
        open={props.show}
        onClose={props.onHide}
        aria-labelledby="dialog-with-options-title"
        scroll="body"
      >
        <DialogTitle id="dialog-with-options-title" className={classes.title}>
          {header}
          <IconButton
            aria-label="close"
            className={classes.closeButton}
            onClick={props.onHide}
          >
            <CloseIcon />
          </IconButton>
        </DialogTitle>
        <DialogContent>
          <div className={classes.startWindow}>
            <CustomAvatar
              width="200px"
              height="200px"
              name={props.name}
              userId={props.id}
              variant="square"
            />
            <div>
              <Button
                className={classes.button}
                onClick={handleChangeButtonClick}
                variant="contained"
                color="primary"
              >
                {displayDeleteButton ? "Add" : "Change"}
              </Button>
              <Button
                className={
                  displayDeleteButton ? classes.deleteButton : classes.button
                }
                onClick={handleDeleteButtonClick}
                variant="contained"
                color="secondary"
              >
                Delete
              </Button>
            </div>
          </div>
        </DialogContent>
      </Dialog>

      <ChangeAvatarModal
        show={showChangeAvatarModal}
        onHide={handleChangeAvatarModalClose}
        onReturn={handleReturnButtonInChangeModalClick}
        header={header}
      />

      <DeleteAvatarModal
        show={showDeleteAvatarModal}
        onHide={handleDeleteAvatarModalClose}
        onReturn={handleReturnButtonInDeleteModalClick}
        id={props.id}
      />
    </React.Fragment>
  );
}
