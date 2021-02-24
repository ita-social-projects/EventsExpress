import React from 'react';
import IconButton from "@material-ui/core/IconButton";
import Tooltip from '@material-ui/core/Tooltip';
import StatusHistory from '../helpers/EventStatusEnum';
import EventAvailableIcon from '@material-ui/icons/EventAvailable';
import EventBusyIcon from '@material-ui/icons/EventBusy';
import { grey } from '@material-ui/core/colors';

export default function EventActiveStatus(props) {
    switch (props.eventStatus) {
        case StatusHistory.Active:
            return (
                <Tooltip title="Active event">
                    <IconButton className="text-success" size="middle"  >
                        <EventAvailableIcon style={{ color: grey[600] }} />
                    </IconButton>
                </Tooltip>)
        case StatusHistory.Canceled:
            return (
                <Tooltip title="Canceled event">
                    <IconButton className="text-danger" size="middle" color="secondary" >
                        <EventBusyIcon style={{ color: grey[600] }} />
                    </IconButton>
                </Tooltip>)
        default:
            return (
                <Tooltip title="Passed event">
                    <IconButton className="text-danger" size="middle" color="primary" >
                        <EventBusyIcon style={{ color: grey[600] }} />
                    </IconButton>
                </Tooltip>)
    }
}
