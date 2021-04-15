import React, { Component } from "react";
import { Link } from 'react-router-dom';
import IconButton from "@material-ui/core/IconButton";
import Badge from '@material-ui/core/Badge';
import Tooltip from '@material-ui/core/Tooltip';
import Zoom from '@material-ui/core/Zoom';
import eventHelper from '../helpers/eventHelper';
import ModalWind from '../modal-wind';
import CustomAvatar from '../avatar/custom-avatar';
import RatingAverage from '../rating/rating-average'
import './header-profile.css';
import AuthComponent from "../../security/authComponent";
import { Roles } from '../../constants/userRoles';

export default class HeaderProfile extends Component {
    handleClick = () => {
        this.props.onSubmit(null);

    }

    render() {
        const { id, name, photoUrl, rating, role } = this.props.user;
        const { onClick } = this.props;

        return (
            <div className='header-profile-root'>
                <div className='d-inline-block'>
                    {!id && (
                        <ModalWind reset={this.props.reset} />
                    )}
                    <AuthComponent>
                        <div className="d-flex flex-column align-items-center">
                            <CustomAvatar size="big" photoUrl={photoUrl} name={this.props.user.name} />
                            <h4 className="user-name">{name}</h4>
                            <RatingAverage value={rating} direction='row' />
                            <div>
                                <Link to={'/profile'}>
                                    <Tooltip title="Edit your profile" placement="bottom" TransitionComponent={Zoom}>
                                        <IconButton>
                                            <i className="fa fa-cog" aria-hidden="true"></i>
                                        </IconButton>
                                    </Tooltip>
                                </Link>
                                <AuthComponent roleMatch={Roles.User}>
                                    <Link to={'/notification_events'}>
                                        <Tooltip title="Notifications" placement="bottom" TransitionComponent={Zoom}>
                                            <IconButton>
                                                <Badge badgeContent={this.props.notification} color="primary">
                                                    <i className="fas fa-bell"></i>
                                                </Badge>
                                            </IconButton>
                                        </Tooltip>
                                    </Link>
                                </AuthComponent>
                                <Link
                                    to={{
                                        pathname: "/home/events",
                                        search: eventHelper.getQueryStringByEventFilter(this.props.filter),
                                    }}
                                >
                                    <Tooltip title="Sign out" placement="bottom" TransitionComponent={Zoom}>
                                        <IconButton onClick={onClick}>
                                            <i className="fas fa-sign-out-alt"></i>
                                        </IconButton>
                                    </Tooltip>
                                </Link>
                            </div>
                            <AuthComponent roleMatch={Roles.User}>
                                <button className="btn btn-outline-secondary" onClick={this.handleClick}>
                                    <i className="fas fa-plus mr-1"></i>
                                        add event
                                </button>
                            </AuthComponent>
                        </div>
                    </AuthComponent>
                </div>
            </div >
        );
    }
}
