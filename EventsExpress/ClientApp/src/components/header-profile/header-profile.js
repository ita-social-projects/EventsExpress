import React, {Component} from "react";
import IconButton from "@material-ui/core/IconButton";
import Create from "@material-ui/icons/Create";
import Notifications from "@material-ui/icons/Notifications";
import DirectionsRun from "@material-ui/icons/DirectionsRun";
import Avatar from '@material-ui/core/Avatar';
import ModalWind from '../modal-wind';
import { Link } from 'react-router-dom';
import RatingAverage from '../rating/rating-average'
import Badge from '@material-ui/core/Badge';

import './header-profile.css';

export default class HeaderProfile extends Component {


    render(){
 
        const { id, name, photoUrl, email, rating } = this.props.user;
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
                        <RatingAverage value={rating} direction='row' />
                        
                        <div>
                            <Link to={'/profile' }><IconButton
                                aria-label="Account of current user"
                                aria-controls="menu-appbar"
                                aria-haspopup="true"
                            >
                                <Create />
                            </IconButton></Link>
                            <Link to={'/notification_events' }>
                                <Badge badgeContent={this.props.notification} color="primary">
                            <IconButton
                                aria-label="Account of current user"
                                aria-controls="menu-appbar"
                                aria-haspopup="true"
                            >
                                <Notifications />
                            </IconButton>
                                </Badge></Link>
                            <Link to="/home/events?page=1">
                            <IconButton
                                className='menuButton'
                                aria-label="Edit"
                                aria-controls="menu-appbar"
                                aria-haspopup="true"
                                onClick={onClick}
                            >
                                <DirectionsRun />
                                </IconButton>
                            </Link>
                        </div>
                        
                            
                    </div>
                )}
            </div>
        </div>
    );
}
}