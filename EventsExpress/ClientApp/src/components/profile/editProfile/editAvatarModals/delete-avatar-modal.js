import React from "react";
import Button from "@material-ui/core/Button";
import KeyboardBackspaceIcon from "@material-ui/icons/KeyboardBackspace";
import IconButton from "@material-ui/core/IconButton";
import Dialog from "@material-ui/core/Dialog";
import DialogContent from "@material-ui/core/DialogContent";
import DialogTitle from "@material-ui/core/DialogTitle";
import DialogContentText from "@material-ui/core/DialogContentText";
import DialogActions from "@material-ui/core/DialogActions";
import { makeStyles } from "@material-ui/core/styles";
import CloseIcon from "@material-ui/icons/Close";

const useStyles = makeStyles({
  text: {
    fontSize: "17px",
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

function DeleteAvatarModal(props) {
  const classes = useStyles();

  const { open, onClose, onReturn, onSubmit } = props;
  return (
    <>
      <Dialog
        fullWidth
        maxWidth="xs"
        open={open}
        onClose={onClose}
        aria-labelledby="dialog-delete-confirm-title"
        scroll="body"
      >
        <DialogTitle id="dialog-delete-confirm-title" className={classes.title}>
          <IconButton onClick={onReturn}>
            <KeyboardBackspaceIcon />
          </IconButton>
          Delete Avatar
          <IconButton
            aria-label="close"
            className={classes.closeButton}
            onClick={onClose}
          >
            <CloseIcon />
          </IconButton>
        </DialogTitle>
        <DialogContent>
          <DialogContentText className={classes.text}>
            Are you sure?
          </DialogContentText>
          <DialogActions>
            <Button variant="contained" color="secondary" onClick={onClose}>
              Cancel
            </Button>
            <Button variant="contained" color="primary" onClick={onSubmit}>
              Confirm
            </Button>
          </DialogActions>
        </DialogContent>
      </Dialog>
    </>
  );
}

export default DeleteAvatarModal;
