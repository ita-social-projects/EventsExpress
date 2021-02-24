import React from 'react';
import IconButton from "@material-ui/core/IconButton";
import Tooltip from '@material-ui/core/Tooltip';

export default function EventBlock(props) {
    const { block, unblock } = props;

    return props.eventItem.eventStatus.Blocked ?
        <Tooltip title="Blocked event">
            <IconButton className="text-success" size="middle" onClick={unblock}>
                <i className="fas fa-lock"></i>
            </IconButton>
        </Tooltip>


        : <Tooltip title="Unblocked event">
            <IconButton className="text-danger" size="middle" onClick={block}>
                <i className="fas fa-unlock"></i>
            </IconButton>
        </Tooltip>



}