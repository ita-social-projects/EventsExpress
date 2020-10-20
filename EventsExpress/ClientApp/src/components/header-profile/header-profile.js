
import React, { Component } from "react";
import { Link } from 'react-router-dom';
import IconButton from "@material-ui/core/IconButton";
import Badge from '@material-ui/core/Badge';
import Tooltip from '@material-ui/core/Tooltip';
import Zoom from '@material-ui/core/Zoom';
import ModalWind from '../modal-wind';
import CustomAvatar from '../avatar/custom-avatar';
import RatingAverage from '../rating/rating-average'
import { generateQuerySearch } from '../../components/helpers/helpers';
import './header-profile.css';

export default class HeaderProfile extends Component {


    render() {

        const { id, name, photoUrl, rating } = this.props.user;
        const { onClick } = this.props;

        return (
            <div className='header-profile-root'>
                <div className='d-inline-block'>
                    {!id && (
                        <ModalWind reset={this.props.reset} />
                    )}
                    {id && (
                        <div className="d-flex flex-column align-items-center">

                            <CustomAvatar size="big" photoUrl={photoUrl} name={this.props.user.name} />
                            <h4>{name}</h4>
                            <RatingAverage value={rating} direction='row' />

                            <div>
                                <Link to={'/profile'}>
                                    <Tooltip title="Edit your profile" placement="bottom" TransitionComponent={Zoom}>
                                        <IconButton>
                                            <i class="fa fa-cog" aria-hidden="true"></i>
                                        </IconButton>
                                    </Tooltip>
                                </Link>

                                <Link to={'/notification_events'}>
                                    <Tooltip title="Notifications" placement="bottom" TransitionComponent={Zoom}>
                                        <IconButton>
                                            <Badge badgeContent={this.props.notification} color="primary">
                                                <i class="fas fa-bell"></i>
                                            </Badge>
                                        </IconButton>
                                    </Tooltip>
                                </Link>

                                {/* <Link to="/home/events?page=1"> */}
                                <Link
                                    to={{
                                        pathname: "/home/events",
                                        search: generateQuerySearch(this.props.events.searchParams),
                                    }}
                                >
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
            </div >
        );
    }
}