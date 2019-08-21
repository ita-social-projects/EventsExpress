import React, {Component} from "react";
import IconButton from "@material-ui/core/IconButton";
import Create from "@material-ui/icons/Create";
import Notifications from "@material-ui/icons/Notifications";
import DirectionsRun from "@material-ui/icons/DirectionsRun";
import Avatar from '@material-ui/core/Avatar';
import ModalWind from '../modal-wind';
import { Link } from 'react-router-dom';

import './header-profile.css';

export default class HeaderProfile extends Component {


    render(){
 
        const { id, name, photoUrl, email } = this.props.user;
        const { onClick } = this.props;
    
    return (
        <div className='root'>
            <div>
                {!id && (
                    <ModalWind  reset={this.props.reset} />
                )}
                {id && (
                    <div className="d-flex flex-column align-items-center">
                        {photoUrl
                            ? <Avatar
                                src={photoUrl}
                                className='bigAvatar'
                            />
                            : <Avatar className='bigAvatar'>
                                <h1 className="display-1 text-light">
                                    {email.charAt(0).toUpperCase()}
                                </h1>
                            </Avatar>}
                        
                        
                        <h4>{name}</h4>
                        
                        <div>
                            <Link to={'/profile' }><IconButton
                                aria-label="Account of current user"
                                aria-controls="menu-appbar"
                                aria-haspopup="true"
                            >
                                <Create />
                            </IconButton></Link>
                            <IconButton
                                aria-label="Account of current user"
                                aria-controls="menu-appbar"
                                aria-haspopup="true"
                            >
                                <Notifications />
                            </IconButton>
                            <IconButton
                                className='menuButton'
                                aria-label="Edit"
                                aria-controls="menu-appbar"
                                aria-haspopup="true"
                                onClick={onClick}
                            >
                                <DirectionsRun />
                            </IconButton>
                        </div>
                        
                            
                    </div>
                )}
            </div>
        </div>
    );
}
}