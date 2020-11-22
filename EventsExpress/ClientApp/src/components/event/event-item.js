import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import Moment from 'react-moment';
import 'moment-timezone';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import { Button, Menu, MenuItem } from '@material-ui/core'
import CardHeader from '@material-ui/core/CardHeader';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import { red } from '@material-ui/core/colors';
import Tooltip from '@material-ui/core/Tooltip';
import Badge from '@material-ui/core/Badge';
import SocialShare from '././share/ShareMenu'
import EventManagmentWrapper from '../../containers/event-managment';
import CustomAvatar from '../avatar/custom-avatar';

const useStyles = makeStyles(theme => ({
    card: {
        maxWidth: 345,
        maxHeight: 200,
        backgroundColor: theme.palette.primary.dark
    },
    media: {
        height: 0,
        paddingTop: '56.25%', // 16:9
    },
    expand: {
        transform: 'rotate(0deg)',
        marginLeft: 'auto',
        transition: theme.transitions.create('transform', {
            duration: theme.transitions.duration.shortest,
        }),
    },
    expandOpen: {
        transform: 'rotate(180deg)',
    },
    avatar: {
        backgroundColor: red[500],
    },
    button: {
    }
}));

export default class Event extends Component {
    constructor(props){
        super(props);
        this.state = {
            anchorEl: null
        }
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
            photoUrl,
            categories,
            countVisitor,
            isBlocked,
            owners
        } = this.props.item;
        const { city, country } = this.props.item;
        const INT32_MAX_VALUE = 2147483647;
        var owner = owners.find(x => x);
        const { anchorEl } = this.state;

        const PrintMenuItems = owners.map(x => (
            <MenuItem onClick={this.handleClose}>
                <div className="d-flex align-items-center border-bottom">
                    <div className="flex-grow-1">
                        <Link to={'/user/' + x.id} className="btn-custom">
                            <div className="d-flex align-items-center border-bottom">
                                <CustomAvatar 
                                    photourl={x.photourl}
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
        ))

        return (
            <div className={"col-12 col-sm-8 col-md-6 col-xl-4 mt-3"}>
                <Card
                    className={classes.card}
                    style={{ backgroundColor: (isBlocked) ? "gold" : "" }}
                >
                    <Menu
                        id="simple-menu"
                        anchorEl={anchorEl}
                        keepMounted
                        anchorOrigin ={{
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
                            <Button title={owner.username} className="btn-custom" onClick={this.handleClick}>
                                    <Badge overlap="circle" badgeContent={owners.length} color="primary"> 
                                        <CustomAvatar
                                            className={classes.avatar}
                                            photourl={owner.photourl}
                                            name={owner.username}
                                        />
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
                    />
                    <CardMedia
                        className={classes.media}
                        title={title}
                    >
                        <Link to={`/event/${id}/1`}>
                            <img src={photoUrl} className="w-100" />
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
                        <Typography variant="body2" color="textSecondary" component="p">
                            {description.substr(0, 128) + '...'}
                        </Typography>
                    </CardContent>
                    <CardActions disableSpacing>
                        <div className='w-100'>
                            <div className='mb-2'>
                                {`${city}, ${country}`}
                            </div>
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
                                    <IconButton className={classes.button} aria-label="view">
                                        <i className="fa fa-eye"></i>
                                    </IconButton>
                                </Link>
                                {(this.props.current_user !== null
                                    && this.props.current_user.role === "Admin")
                                    ? <EventManagmentWrapper eventItem={this.props.item} />
                                    : null
                                }
                                <SocialShare href={`${window.location.protocol}//${window.location.host}/event/${id}/1`} />
                            </div>
                        </div>
                    </CardActions>
                </Card>
            </div>
        );
    }
}
