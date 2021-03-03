import React from 'react';
import IconButton from "@material-ui/core/IconButton";
import Tooltip from '@material-ui/core/Tooltip';
import StatusHistory from '../helpers/EventStatusEnum';
import EventChangeStatusModal from './event-change-status-modal';

export default function EventActiveStatus(props) {
    switch (props.eventStatus) {
        case StatusHistory.Active:
            return (
                <EventChangeStatusModal
                    key={props.eventId + props.eventStatus}
                    submitCallback={(reason) => props.onBlock(props.eventId, reason, props.eventStatus)}
                    button={<Tooltip title="Active event">
                        <IconButton className="text-success" size="middle">
                            <i className="fas fa-unlock"></i>
                        </IconButton>
                    </Tooltip>}
                />)
        case StatusHistory.Blocked:
            return (
                <EventChangeStatusModal
                    key={props.eventId + props.eventStatus}
                    submitCallback={(reason) => props.onUnBlock(props.eventId, reason)}
                    button={<Tooltip title="Blocked event">
                        <IconButton className="text-danger" size="middle">
                            <i className="fas fa-lock"></i>
                        </IconButton>
                    </Tooltip>}
                />)
        case StatusHistory.Canceled:
            return (
                <Tooltip title="Canceled event">
                    <IconButton className="text-danger" size="middle" color="secondary" >
                        <i class="far fa-calendar-times"></i>
                    </IconButton>
                </Tooltip>)
    }
}
