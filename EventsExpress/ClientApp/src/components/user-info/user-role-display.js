import React from 'react';
import IconButton from "@material-ui/core/IconButton";

export function UserRoleDisplay(props) {

    return (<>
            <td className="align-middle">{props.user.role.name}</td>

            <td className="align-middle">
                { (!props.isCurrentUser) 
                    ? <IconButton  className=""  size="small" onClick={props.callback}>
                        <i className="fas fa-edit"></i>
                     </IconButton> : null
                }
            </td>
        </>)
}