import React from "react";
import Button from "@material-ui/core/Button";
import KeyboardBackspaceIcon from "@material-ui/icons/KeyboardBackspace";
import IconButton from "@material-ui/core/IconButton";
import { connect } from "react-redux";
import { delete_avatar } from "../../../../actions/redactProfile/avatar-change-action";
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
  const CancelButtonClick = () => {
    props.onHide();
  };
  const ConfirmButtonClick = () => {
    props.delete_avatar(props.id);
  };

  return (
    <>
      <Dialog
        fullWidth
        maxWidth="xs"
        open={props.show}
        onClose={props.onHide}
        aria-labelledby="dialog-delete-confirm-title"
        scroll="body"
      >
        <DialogTitle id="dialog-delete-confirm-title" className={classes.title}>
          <IconButton onClick={props.onReturn}>
            <KeyboardBackspaceIcon />
          </IconButton>
          Delete Avatar
          <IconButton
            aria-label="close"
            className={classes.closeButton}
            onClick={props.onHide}
          >
            <CloseIcon />
          </IconButton>
        </DialogTitle>
        <DialogContent>
          <DialogContentText className={classes.text}>
            Are you sure?
          </DialogContentText>
          <DialogActions>
            <Button
              variant="contained"
              color="secondary"
              onClick={CancelButtonClick}
            >
              Cancel
            </Button>
            <Button
              variant="contained"
              color="primary"
              onClick={ConfirmButtonClick}
            >
              Confirm
            </Button>
          </DialogActions>
        </DialogContent>
      </Dialog>
    </>
  );
}

const mapDispatchToProps = (dispatch) => {
  return {
    delete_avatar: (data) => dispatch(delete_avatar(data)),
  };
};

export default connect(null, mapDispatchToProps)(DeleteAvatarModal);
