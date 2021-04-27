import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import Moment from 'react-moment';

import 'moment-timezone';
import Card from '@material-ui/core/Card';
import { Button, Menu, MenuItem } from '@material-ui/core';
import CardHeader from '@material-ui/core/CardHeader';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import Tooltip from '@material-ui/core/Tooltip';
import Badge from '@material-ui/core/Badge';

import SocialShareMenu from './share/SocialShareMenu';
import EventActiveStatus from './event-active-status';
import CustomAvatar from '../avatar/custom-avatar';
import DisplayLocation from './map/display-location';
import eventStatusEnum from '../../constants/eventStatusEnum';
import { useStyle } from '../event/CardStyle';

import PhotoService from '../../services/PhotoService';
import EventsExpressService from '../../services/EventsExpressService';
import { connect } from 'react-redux';
import AuthComponent from '../../security/authComponent';
import { eventImage } from '../../constants/eventImage';

const useStyles = useStyle;
const photoService = new PhotoService();

export default class EventCard extends Component {
    constructor(props) {
        super(props);

        this.state = {
            anchorEl: null
        }
    }

    componentDidMount() {
        photoService.getPreviewEventPhoto(this.props.item.id);
    }

    renderCategories = (arr) => {
        return arr.map((x) => (<div key={x.id}>#{x.name}</div>)
        );
    }

    handleClick = (event) => {
        this.setState({ anchorEl: event.currentTarget });
    }

    handleClose = () => {
        this.setState({ anchorEl: null });
    }

    render() {
        const classes = useStyles;
        const {
            id,
            title,
            dateFrom,
            description,
            isPublic,
            maxParticipants,
            eventStatus,
            photoUrl,
            categories,
            countVisitor,
            owners
        } = this.props.item;
        const INT32_MAX_VALUE = null;
        const { anchorEl } = this.state;

        const PrintMenuItems = owners.map(x => (
            <MenuItem onClick={this.handleClose}>
                <div className="d-flex align-items-center border-bottom">
                    <div className="flex-grow-1">
                        <Link to={'/user/' + x.id} className="btn-custom">
                            <div className="d-flex align-items-center border-bottom">
                                <CustomAvatar
                                    userId={owners[0].id}
                                    name={x.username}
                                />
                                <div>
                                    <h5 className="pl-2">{x.username}</h5>
                                </div>
                            </div>
                        </Link>
                    </div>
                </div>
            </MenuItem>
        ));

        return (
            <div className={"col-12 col-sm-8 col-md-6 col-xl-4 mt-3"}>
                <Card
                    className={classes.cardCanceled}
                    style={{
                        backgroundColor: (eventStatus === eventStatusEnum.Blocked) ? "gold" : "",
                        opacity: (eventStatus === eventStatusEnum.Canceled) ? 0.5 : 1

                    }}
                >
                    <Menu
                        id="simple-menu"
                        anchorEl={anchorEl}
                        keepMounted
                        anchorOrigin={{
                            vertical: "bottom",
                            horisontal: "left"
                        }}
                        open={Boolean(anchorEl)}
                        onClose={this.handleClose}
                    >

                        {
                            PrintMenuItems
                        }
                    </Menu>
                    <CardHeader
                        avatar={
                            <Button title={owners[0].username} className="btn-custom" onClick={this.handleClick}>
                                <Badge overlap="circle" badgeContent={owners.length} color="primary">
                                    <CustomAvatar
                                        className={classes.avatar}
                                        userId={owners[0].id}
                                        name={owners[0].username}/>
                                </Badge>
                            </Button>
                        }

                        action={
                            <Tooltip title="Visitors">
                                <IconButton>
                                    <Badge badgeContent={countVisitor} color="primary">
                                        <i className="fa fa-users"></i>
                                    </Badge>
                                </IconButton>
                            </Tooltip>
                        }
                        title={title}
                        subheader={<Moment format="D MMM YYYY" withTitle>{dateFrom}</Moment>}
                        classes={{ title: 'title' }}
                    />
                    <CardMedia
                        className={classes.media}
                        title={title}>
                        <Link to={`/event/${id}/1`} id="LinkToEvent">
                            <img src={eventImage}
                                id="eventPreviewPhotoImg" alt="Event"
                                className="w-100" />
                        </Link>
                    </CardMedia>
                    {(maxParticipants < INT32_MAX_VALUE) &&
                        <CardContent>
                            <Typography
                                variant="body2"
                                color="textSecondary"
                                component="p"
                            >
                                {countVisitor}/{maxParticipants} Participants
                            </Typography>
                        </CardContent>
                    }
                    <CardContent>
                        {description &&
                            <Tooltip title={description.substr(0, 570) + (description.length > 570 ? '...' : '')} classes={{ tooltip: 'description-tooltip' }} >
                                <Typography variant="body2" color="textSecondary" className="description" component="p">
                                    {description.substr(0, 128)}
                                </Typography>
                            </Tooltip>
                        }
                    </CardContent>
                    <CardActions disableSpacing>
                        <div className='w-100'>
                            {this.props.item.location &&
                                <DisplayLocation
                                    location={this.props.item.location}
                                />
                            }
                            <br />
                            <div className="float-left">
                                {this.renderCategories(categories.slice(0, 2))}
                            </div>
                            <div className='d-flex flex-row align-items-center justify-content-center float-right'>
                                {!isPublic &&
                                    <Tooltip title="Private event">
                                        <IconButton>
                                            <Badge color="primary">
                                                <i className="fa fa-key"></i>
                                            </Badge>
                                        </IconButton>
                                    </Tooltip>
                                }
                                <Link to={`/event/${id}/1`}>
                                    <Tooltip title="View">
                                        <IconButton aria-label="view">
                                            <i className="fa fa-eye"></i>
                                        </IconButton>
                                    </Tooltip>
                                </Link>
                                <AuthComponent rolesMatch={['Admin']}>
                                    <EventActiveStatus
                                        key={this.props.item.id + this.props.item.eventStatus}
                                        eventStatus={this.props.item.eventStatus}
                                        eventId={this.props.item.id}
                                        onBlock={this.props.onBlock}
                                        onUnBlock={this.props.onUnBlock} />
                                </AuthComponent>
                                <SocialShareMenu href={`${window.location.protocol}//${window.location.host}/event/${id}/1`} />
                            </div>
                        </div>
                    </CardActions>
                </Card>
            </div>
        );
    }
}