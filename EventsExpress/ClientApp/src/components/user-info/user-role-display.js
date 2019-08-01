import React from 'react';
import Fab from '@material-ui/core/Fab';

export function UserRoleDisplay(props) {

    return (<>
        <td className="align-middle">{props.role.name}</td>

            <td className="align-middle">
                <Fab size="small" onClick={props.callback} >
                    <i className="fas fa-edit"></i>
                </Fab>
            </td>
        </>)
}