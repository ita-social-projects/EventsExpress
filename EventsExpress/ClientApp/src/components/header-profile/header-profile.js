import React, {Component} from "react";
import { makeStyles } from "@material-ui/core/styles";
import IconButton from "@material-ui/core/IconButton";
import Create from "@material-ui/icons/Create";
import Notifications from "@material-ui/icons/Notifications";
import DirectionsRun from "@material-ui/icons/DirectionsRun";
import AccountCircle from "@material-ui/icons/AccountCircle";
import Switch from "@material-ui/core/Switch";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import FormGroup from "@material-ui/core/FormGroup";
import MenuItem from "@material-ui/core/MenuItem";
import Menu from "@material-ui/core/Menu";
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import ModalWind from '../modal-wind';

import './header-profile.css';

export default class HeaderProfile extends Component {


    render(){
 
        const { id, name } = this.props.user;
        const { onClick} = this.props;
    
    return (
        <div className='root'>
            <div>
                {!id && (
                    <ModalWind  reset={this.props.reset} />
                )}
                {id && (
                    <div className="d-flex flex-column align-items-center">
                        <Avatar
                            alt="Тут аватар"
                            src="/dima/dima.png"
                        
                            className='bigAvatar'
                        />
                        <h4>{name}</h4>
                        
                        <div>
                            <IconButton
                                aria-label="Account of current user"
                                aria-controls="menu-appbar"
                                aria-haspopup="true"
                            >
                                <Create />
                            </IconButton>
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