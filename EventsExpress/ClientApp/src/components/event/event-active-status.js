import React from 'react';
import IconButton from "@material-ui/core/IconButton";
import Tooltip from '@material-ui/core/Tooltip';
import eventStatusEnum from '../../constants/eventStatusEnum';
import SimpleModalWithDetails from '../helpers/simple-modal-with-details';

export default function EventActiveStatus(props) {
    switch (props.eventStatus) {
        case eventStatusEnum.Active:
            return (
                <SimpleModalWithDetails
                    key={props.eventId + props.eventStatus}
                    data="Are you sure?"
                    submitCallback={(reason) => props.onBlock(props.eventId, reason, props.eventStatus)}
                    button={<Tooltip title="Active event">
                        <IconButton className="text-success" size="middle">
                            <i className="fas fa-unlock" />
                        </IconButton>
                    </Tooltip>}
                />)
        case eventStatusEnum.Blocked:
            return (
                <SimpleModalWithDetails
                    key={props.eventId + props.eventStatus}
                    data="Are you sure?"
                    submitCallback={(reason) => props.onUnBlock(props.eventId, reason)}
                    button={<Tooltip title="Blocked event">
                        <IconButton className="text-danger" size="middle">
                            <i className="fas fa-lock" />
                        </IconButton>
                    </Tooltip>}
                />)
        case eventStatusEnum.Canceled:
            return (
                <Tooltip title="Canceled event">
                    <IconButton className="text-danger" size="middle" color="secondary" >
                        <i className="far fa-calendar-times" />
                    </IconButton>
                </Tooltip>)
    }
}
