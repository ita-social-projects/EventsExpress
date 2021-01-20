import React from "react";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import RecoverPasswordContainer from "../../containers/editProfileContainers/recoverPasswordContainer";

function Modalwind2(props) {
    const [open, setOpen] = React.useState(false);

    function handleClickOpen() {
        setOpen(true);
    }

    function handleClose() {
        setOpen(false);
    }

    return (
        <>
            <Button fullWidth color="primary" onClick={handleClickOpen}>
                Forgot password
            </Button>
            <Dialog
                open={open}
                onClose={handleClose}
                aria-labelledby="form-dialog-title"
                maxWidth='md'
            >
                <RecoverPasswordContainer handleClose={handleClose} />
                <Button fullWidth onClick={handleClose} color="primary">
                    Close
                </Button>
            </Dialog>
        </>
    )
}

export default Modalwind2;
