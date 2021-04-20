import React from 'react';
import IconButton from "@material-ui/core/IconButton";

export function UserRoleDisplay(props) {

    let roles = props.user.roles.map(r => <div>{r.name}</div>)

    return (<>
            <td className="align-middle">{roles}</td>

            <td className="align-middle">
                { (!props.isCurrentUser) 
                    ? <IconButton  className="text-info"  size="small" onClick={props.callback}>
                        <i className="fas fa-edit"></i>
                     </IconButton> : null
                }
            </td>
        </>)
}