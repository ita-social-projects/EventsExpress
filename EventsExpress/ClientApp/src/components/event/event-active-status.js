import React from 'react';
import IconButton from "@material-ui/core/IconButton";
import Tooltip from '@material-ui/core/Tooltip';
import eventStatusEnum from '../../constants/eventStatusEnum';
import EventChangeStatusModal from './event-change-status-modal';

export default function EventActiveStatus(props) {
    switch (props.eventStatus) {
        case eventStatusEnum.Active:
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
        case eventStatusEnum.Blocked:
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
        case eventStatusEnum.Canceled:
            return (
                <Tooltip title="Canceled event">
                    <IconButton className="text-danger" size="middle" color="secondary" >
                        <i class="far fa-calendar-times"></i>
                    </IconButton>
                </Tooltip>)
    }
}
