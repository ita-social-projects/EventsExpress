import React, {Component} from "react";
import { Link } from 'react-router-dom';
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
import CustomAvatar from '../avatar/custom-avatar';
import RatingAverage from '../rating/rating-average'

import './header-profile.css';

export default class HeaderProfile extends Component {


    render(){
 
        const { id, name, photoUrl, rating } = this.props.user;
        const { onClick } = this.props;
    
    return (
        <div className='header-profile-root'>
            <div className='d-inline-block'>
                {!id && (
                    <ModalWind  reset={this.props.reset} />
                )}
                {id && (
                    <div className="d-flex flex-column align-items-center">

                        <CustomAvatar size="big" photoUrl={photoUrl} name={this.props.user.name} />                        
                        <h4>{name}</h4>
                        <RatingAverage value={rating} direction='row' />
                        
                        <div>
                            <Link to={'/profile' }>
                                <Tooltip title="Edit your profile" placement="bottom" TransitionComponent={Zoom}>
                                    <IconButton>
                                        <i class="fa fa-cog" aria-hidden="true"></i>
                                    </IconButton>
                                </Tooltip>
                            </Link>
                            
                            <Link to={'/notification_events' }>                            
                                <Tooltip title="Notifications" placement="bottom" TransitionComponent={Zoom}>
                                    <IconButton>
                                        <Badge badgeContent={this.props.notification} color="primary">
                                            <i class="fas fa-bell"></i>
                                        </Badge>
                                    </IconButton>
                                </Tooltip>
                            </Link>

                            <Link to="/home/events?page=1">
                                <Tooltip title="Sign out" placement="bottom" TransitionComponent={Zoom}>
                                    <IconButton onClick={onClick}>
                                        <i class="fas fa-sign-out-alt"></i>
                                    </IconButton>
                                </Tooltip>
                            </Link>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
}
}