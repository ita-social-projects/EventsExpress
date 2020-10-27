import React from 'react';
import IconButton from "@material-ui/core/IconButton";

export default function EventBlock(props){
    const {block,unblock}=props;
    
    return props.eventItem.isBlocked ?
        <IconButton className="text-success" size="middle" onClick={unblock}>
            <i className="fas fa-lock"></i>
        </IconButton>

        :<IconButton className="text-danger" size="middle" onClick={block}>
            <i className="fas fa-unlock"></i>
        </IconButton>
             
         
         
}