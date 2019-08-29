import React, { Component } from "react";
import Button from "@material-ui/core/Button";
import Module from '../helpers';
import Dialog from "@material-ui/core/Dialog";
import { makeStyles } from "@material-ui/core/styles";
import RecoverPasswordContainer from "../../containers/editProfileContainers/recoverPasswordContainer";




const useStyles = makeStyles({
    root: {
        flexGrow: 1,
        maxWidth: true,
        fullWidth: true
    }
});



function Modalwind2(props) {
    console.log(props)
    const [open, setOpen] = React.useState(false);

    function handleClickOpen() {
        setOpen(true);
    }

    function handleClose() {
        setOpen(false);
       // props.reset();
    }


    return (
        <div>
            <Button  color="primary" onClick={handleClickOpen}>
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
        </div>
    )
}
export default Modalwind2