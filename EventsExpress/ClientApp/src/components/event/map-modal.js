import React, { Component } from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import { renderLocationMapWithCircle } from '../helpers/form-helpers';
import { Field } from 'redux-form';
import './slider.css';
import DisplayMap from '../event/map/display-map';

class MapModal extends Component {
    constructor(props) {
        super(props);
        this.state = {
            open: false
        }
    }

    handleClickOpen = () => {
        this.setState({ open: true });
    };

    handleClose = () => {
        const startValue = this.props.initialize({
            radius: 8,
            selectedPos: { latitude: null, longitude: null }
        })
        if (this.props.values.selectedPos.latitude != null)
            return startValue;
        else
            return startValue && this.setState({ open: false })  
    };

    handleFilter = () => {
        this.setState({ open: false });
    }

    render() {
        return (
            <div>
                <Button variant="outlined" fullWidth={true} color="primary" onClick={this.handleClickOpen}>
                    Filter by location
                </Button>
                <Dialog fullWidth={true} open={this.state.open} onClose={this.handleClose} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Filter by location</DialogTitle>
                    <DialogContent>
                        {this.props.values && this.props.values.radius &&
                            <div>
                                <div class="slidecontainer">
                                    <label>Radius is {this.props.values.radius} km</label>
                                    <Field name="radius" component="input"
                                        type="range"
                                        min="1" max="10000" value={this.props.values.radius}
                                        onChange={this.onRadiusChange}
                                        step="1"
                                        className="radius-slider"
                                    />
                                </div>
                            </div>
                        }
                        <div>
                            {
                                this.props.values &&
                                this.props.values.selectedPos != undefined &&
                                this.props.values.selectedPos.latitude != undefined &&
                                this.props.values.selectedPos.longitude != undefined &&
                                <div>
                                    <p>Current position on the Map is:</p>
                                    <p>latitude: {this.props.values.selectedPos.latitude}</p>
                                    <p>longitude: {this.props.values.selectedPos.longitude}</p>
                                    <DisplayMap location={{ ...this.props.values.selectedPos }}/>
                                </div>
                            }
                            {
                                this.props.values &&
                                this.props.values.selectedPos.latitude == null &&
                                this.props.values.selectedPos.longitude == null &&
                                <div>
                                    <p>Choose position on the Map!</p>
                                </div>
                            }
                            <Field
                                name='selectedPos'
                                component={renderLocationMapWithCircle}
                                radius={this.props.values.radius}
                            />
                        </div>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={this.handleClose} color="primary">
                            Cancel
                        </Button>
                        <Button onClick={this.handleFilter} color="primary">
                            Apply
                        </Button>
                    </DialogActions>
                </Dialog>
            </div>
        );
    }
}

export default MapModal;