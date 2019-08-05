import React from 'react';
import Fab from '@material-ui/core/Fab';

export function UserRoleDisplay(props) {

    return (<>
        <td className="align-middle">{props.user.role.name}</td>

            <td className="align-middle">
                { (props.user.id !== props.currentUser.id) 
                    ? <Fab size="small" onClick={props.callback} >
                        <i className="fas fa-edit"></i>
                    </Fab> : null
                }
            </td>
        </>)
}