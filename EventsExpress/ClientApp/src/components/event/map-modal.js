import React from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import LocationMap from './map/location-map';
import { reduxForm, Field } from 'redux-form';
import { renderTextField } from '../helpers/helpers';
import { DialogContentText } from '@material-ui/core';

let MapModal = props => {
    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    return (
        <div>
            <Button variant="outlined" color="primary" onClick={handleClickOpen}>
                Filter by location
            </Button>
            <Dialog fullWidth={true} open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
                <DialogTitle id="form-dialog-title">Filter by location</DialogTitle>
                <DialogContent>
                    <Field
                        name='radius'
                        component={renderTextField}
                        type="input"
                        label="Radius"
                    />
                    <Field
                        name='selectedPos'
                        component={LocationMap}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} color="primary">
                        Cancel
                    </Button>
                    <Button onClick={handleClose} color="primary">
                        Filter
                </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}

export default MapModal;