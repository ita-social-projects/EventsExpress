import React from 'react';
import IconButton from "@material-ui/core/IconButton";

export default function EventBlock(props){
    const {eventItem,block,unblock}=props;
    console.log("props:");
    console.log(props);
    console.log(props.eventItem.isBlocked);
    return props.eventItem.isBlocked ?
        <IconButton className="text-success" size="small" onClick={unblock}>
            <i className="fas fa-lock"></i>
        </IconButton>

        :<IconButton className="text danger" size="small" onClick={block}>
            <i className="fas fa-unlock"></i>
        </IconButton>
             
         
         
}