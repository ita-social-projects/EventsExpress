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
import Typography from '@material-ui/core/Typography';
import { red } from '@material-ui/core/colors';
import Tooltip from '@material-ui/core/Tooltip';
import Badge from '@material-ui/core/Badge';
import CustomAvatar from '../avatar/custom-avatar';
import './event-item.css';

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
    constructor(props) {
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
            photoUrl,
            isBlocked,
            owners
        } = this.props.item;
        const { anchorEl } = this.state;

        const PrintMenuItems = owners.map(x => (
            <MenuItem onClick={this.handleClose}>
                <div className="d-flex align-items-center border-bottom">
                    <div className="flex-grow-1">
                        <Link to={'/user/' + x.id} className="btn-custom">
                            <div className="d-flex align-items-center border-bottom">
                                <CustomAvatar
                                    photoUrl={x.photoUrl}
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
                <Link to={`/editEvent/${id}/`}>
                <Card
                    className={classes.card}
                    style={{ backgroundColor: (isBlocked) ? "gold" : "" }}
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
                                            photoUrl={owners[0].photoUrl}
                                            name={owners[0].username}
                                        />
                                    </Badge>
                                </Button>

                            }

                            title={title}
                            subheader={<Moment format="D MMM YYYY" withTitle>{dateFrom}</Moment>}
                            classes={{ title: 'title' }}
                        />
                        <CardMedia
                            className={classes.media}
                            title={title}
                        >                                                    
                            {photoUrl &&
                                <img src={photoUrl} className="w-100" alt="Event" /> 
                            }
                            {photoUrl === null &&
                                <i class="far fa-images fa-10x " ></i>   
                            }
                        </CardMedia>
                    <CardContent>
                        {description &&
                            <Tooltip title={description.substr(0, 570) + (description.length > 570 ? '...' : '')} classes={{ tooltip: 'description-tooltip' }} >
                                <Typography variant="body2" color="textSecondary" className="description" component="p">
                                    {description.substr(0, 128)}
                                </Typography>
                            </Tooltip>
                        }
                    </CardContent>
                    </Card>
                </Link>
            </div>
        );
    }
}
