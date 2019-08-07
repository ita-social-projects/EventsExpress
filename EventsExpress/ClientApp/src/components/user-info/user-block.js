import React from 'react';
import IconButton from "@material-ui/core/IconButton";

export function UserBlock(props) {
    const { user, isCurrentUser, block, unblock } = props;

    return (isCurrentUser) ? <td> </td> :
        <td className="align-middle">
                 <div className="d-flex justify-content-center align-items-center">
                    {(user.isBlocked == true)
                        ? <IconButton  className="text-success" size="small" onClick={unblock}>
                            <i className="fas fa-lock" ></i>
                        </IconButton> 
                        : <IconButton className="text-danger" size="small" onClick={block} >
                            <i className="fas fa-unlock-alt" ></i>
                        </IconButton>
                    }
                </div>
        </td>

}