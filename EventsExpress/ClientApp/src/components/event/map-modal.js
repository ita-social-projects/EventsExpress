import React from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import LocationMap from './map/location-map';
import { makeStyles } from "@material-ui/core/styles";
import { Field } from 'redux-form';
import Typography from "@material-ui/core/Typography";
import Slider from "@material-ui/core/Slider";

function RadiusSlider(props) {
    const useStyles = makeStyles((theme) => ({
        root: {
            width: "auto"
        },
        margin: {
            height: theme.spacing(3)
        }
    }));

    const classes = useStyles();

    const marks = [
        {
            value: 0,
            label: "0"
        },

        {
            value: 2000,
            label: "2000"
        }
    ];

    const valuetext = (value) => value;

    const onChange = (event, value) => {
        props.onRadiusChange(value);
    }

    return (
        <div className={classes.root}>
            <Typography id="discrete-slider-custom" gutterBottom>
                Radius
      </Typography>
            <Slider
                defaultValue={20}
                getAriaValueText={valuetext}
                aria-labelledby="discrete-slider-custom"
                valueLabelDisplay="auto"
                marks={marks}
                scale={(x) => Math.exp(x-100)}
                min={0}
                max={2000}
                onChange={onChange}
            />
        </div>
    );
}

const MapModal = props => {
    const [open, setOpen] = React.useState(false);
    const [selectedPos, setSelectedPos] = React.useState(null)
    const [radius, setRadius] = React.useState(10);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const onClickCallBack = (coords) => {        
        setSelectedPos([coords.lat, coords.lng]);
    }

    const onRadiusChange = (value) => {
        setRadius(value);
    }

    const pos = () => {
        if (selectedPos != null) {
            return (
                <div>
                    <div>{selectedPos[0]}</div>
                    <div>{selectedPos[1]}</div>
                </div>
                );
        }
    }

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
                        component={RadiusSlider}
                        onRadiusChange={onRadiusChange}
                    />
                    <Field
                        name='selectedPos'
                        component={LocationMap}
                        onClickCallBack={onClickCallBack}
                        circle
                        radius={radius}
                    />
                    {pos()}
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