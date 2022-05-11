import React from "react";
import ChangeAvatarWrapper from "../../../../containers/editProfileContainers/change-avatar";
import KeyboardBackspaceIcon from "@material-ui/icons/KeyboardBackspace";
import IconButton from "@material-ui/core/IconButton";
import Dialog from "@material-ui/core/Dialog";
import DialogContent from "@material-ui/core/DialogContent";
import DialogTitle from "@material-ui/core/DialogTitle";
import CloseIcon from "@material-ui/icons/Close";
import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles({
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

export default function ChangeAvatarModal(props) {
  const classes = useStyles();
  return (
    <>
      <Dialog
        fullWidth
        maxWidth="sm"
        open={props.show}
        onClose={props.onHide}
        aria-labelledby="dialog-delete-confirm-title"
        scroll="body"
      >
        <DialogTitle id="dialog-delete-confirm-title" className={classes.title}>
          <IconButton onClick={props.onReturn}>
            <KeyboardBackspaceIcon />
          </IconButton>
          {props.header}
          <IconButton
            aria-label="close"
            className={classes.closeButton}
            onClick={props.onHide}
          >
            <CloseIcon />
          </IconButton>
        </DialogTitle>
        <DialogContent>
          <ChangeAvatarWrapper/>
        </DialogContent>
      </Dialog>
    </>
  );
}
