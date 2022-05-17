import React from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogTitle from '@material-ui/core/DialogTitle';
import { reduxForm, Field} from 'redux-form';
import { useMediaQuery, useTheme } from '@material-ui/core';
import LocationByMap from '../../location/location_map';


const EditLocation = (props) => {
  const {isOpen, onClose, handleSubmit,submitting,pristine} = props;
  const [open, setOpen] = React.useState(isOpen);
  const theme = useTheme();
  const fullScreen = useMediaQuery(theme.breakpoints.down('xs'));

  const handleClose = () => {
    onClose();
    setOpen(false);
  };

    return (
      <div>
      <Dialog
        open={open}
        fullScreen={fullScreen}
        onClose={handleClose}
        fullWidth={true}
      >
        <DialogTitle id="alert-dialog-title">{"Edit your location"}</DialogTitle>
        <form onSubmit = {handleSubmit}>
            <Field name="location" component={LocationByMap}  label = "Location"/>
            <DialogActions>
              <Button onClick={handleClose} color="primary">
                Cancel
              </Button>
              <Button color="primary" type = 'submit' disabled={pristine || submitting}>
                Confirm
              </Button>
            </DialogActions>
          </form>
      </Dialog>
    </div>
  )
}


export default reduxForm({
  form: "EditLocation",
  enableReinitialize: true,
  touchOnChange: true
})(EditLocation);