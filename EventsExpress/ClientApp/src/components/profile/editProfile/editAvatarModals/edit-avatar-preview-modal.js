import React from "react";
import CustomAvatar from "../../../avatar/custom-avatar";
import Button from "@material-ui/core/Button";
import { makeStyles } from "@material-ui/core/styles";
import Dialog from "@material-ui/core/Dialog";
import DialogContent from "@material-ui/core/DialogContent";
import DialogTitle from "@material-ui/core/DialogTitle";
import IconButton from "@material-ui/core/IconButton";
import CloseIcon from "@material-ui/icons/Close";
import { PhotoService } from "../../../../services";

const photoService = new PhotoService();

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

export default function EditAvatarModal(props) {
  const classes = useStyles();
  const [displayDeleteButton, setDisplayDeleteButton] = React.useState(false);
  const { open, onClose, name, id, onChangeButtonClick, onDeleteButtonClick } =
    props;
  const header = (displayDeleteButton ? "Add" : "Edit") + " Avatar";

  React.useEffect(() => {
    photoService.getUserPhoto(id).then((image) => {
      if (image) {
        setDisplayDeleteButton(false);
      } else {
        setDisplayDeleteButton(true);
      }
    });
  });
  return (
    <React.Fragment>
      <Dialog
        fullWidth
        maxWidth="xs"
        open={open}
        onClose={onClose}
        aria-labelledby="dialog-with-options-title"
        scroll="body"
      >
        <DialogTitle id="dialog-with-options-title" className={classes.title}>
          {header}
          <IconButton
            aria-label="close"
            className={classes.closeButton}
            onClick={onClose}
          >
            <CloseIcon />
          </IconButton>
        </DialogTitle>
        <DialogContent>
          <div className={classes.startWindow}>
            <CustomAvatar
              width="200px"
              height="200px"
              name={name}
              userId={id}
              variant="square"
            />
            <div>
              <Button
                className={classes.button}
                onClick={onChangeButtonClick}
                variant="contained"
                color="primary"
              >
                {displayDeleteButton ? "Add" : "Change"}
              </Button>
              <Button
                className={
                  displayDeleteButton ? classes.deleteButton : classes.button
                }
                onClick={onDeleteButtonClick}
                variant="contained"
                color="secondary"
              >
                Delete
              </Button>
            </div>
          </div>
        </DialogContent>
      </Dialog>
    </React.Fragment>
  );
}
